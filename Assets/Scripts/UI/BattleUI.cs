﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    private DataProvider dataProvider;
    [SerializeField] private Image reloadImage;
    [SerializeField] private Image noizeCountSprite;
    [SerializeField] private Image powerCountSprite;
    [SerializeField] private Image playerHPCountSprite;
    [SerializeField] private Text ammoCountText;
    [SerializeField] private Image activeItemImage;
    public LootBoxPanel LootBoxPanel;
    public TerminalPanel TerminalPanel;
    public DoorDataPanel DoorDataPanelPrefab;
    public Sprite emptySprite;
    [SerializeField] private Transform DoorDataPanelTransform;
    [SerializeField] private InventoryItemSlot activeSlot;
    [SerializeField] private List<InventoryItemSlot> inventoryItemSlots = new List<InventoryItemSlot>();
    [SerializeField] private List<InventoryItemSlot> lootBoxItemSlots = new List<InventoryItemSlot>();
    [SerializeField] private Transform canvas;
    [SerializeField] private List<ItemInfoPanel> itemInfoPanels = new List<ItemInfoPanel>();
    [SerializeField] private Vector3 itemInfoPanelOffset;
    [SerializeField] private Transform questPanel;
    [SerializeField] private GameObject questPrefab;
    private float reloadTime = 0f;
    private void Start()
    {
        dataProvider = DataProvider.Instance;
        dataProvider.Events.OnUiUpdate += UpdateUI;
        dataProvider.Events.OnAddItemToSlot += AddItemToSlot;
        Invoke("UpdateUI", 0.5f);
    }

    public GameObject CreateQuestPanel(string name, string description)
    {
        GameObject quest = Instantiate(questPrefab);
        quest.transform.SetParent(questPanel);
        QuestUI questUI = quest.GetComponent<QuestUI>();
        questUI.Name.text = description;
        return quest;
    }

    public void RemoveActiveSlotItem()
    {

        activeSlot.ItemInSlot = null;
        activeSlot.ItemImage.sprite = emptySprite;

    } 

    private void Update()
    {
        int activecount = 0;
        int currentitem = 0;
        activecount = dataProvider.Player.Inventory.NearestItem.Count;

        foreach (var i in itemInfoPanels)
        {
            if (activecount == 0)
            {
                i.gameObject.SetActive(false);
            }
            else
            {
                itemInfoPanels[currentitem].gameObject.SetActive(true);
                Vector3 itempos = dataProvider.Player.Inventory.NearestItem[currentitem].transform.position;
                Vector3 offsetpos = new Vector3(itempos.x + itemInfoPanelOffset.x, itempos.y + itemInfoPanelOffset.y, itempos.z + itemInfoPanelOffset.z);
                itemInfoPanels[currentitem].transform.position = Camera.main.WorldToScreenPoint(offsetpos);
                Sprite sprite = null;

                foreach (var b in dataProvider.ItemsData.ItemsDatas)
                {
                    if (b.ID == dataProvider.Player.Inventory.NearestItem[currentitem].ID)
                    {
                        sprite = b.Sprite;
                        break;
                    }
                }

                itemInfoPanels[currentitem].image.sprite = sprite;
            }

            if (currentitem + 1 < activecount)
                currentitem++;
        }

        if (!dataProvider.Player.CurrentWeapon)
        {
            return;
        }

        if (dataProvider.Player.CurrentWeapon.isReload)
        {
            reloadTime += Time.deltaTime;
            reloadImage.fillAmount = reloadTime / dataProvider.Player.CurrentWeapon.ReloadTime;
        }
        else
        {
            reloadTime = 0f;
            reloadImage.fillAmount = 0;

        }
    }

    public void CreateInventoryItemSlot(InventoryItemSlot inventoryItemSlot)
    {
        inventoryItemSlots.Add(inventoryItemSlot);
    }

    public void CreateItemInfoPanel(Item item)
    {

    }

    public void CloseLootBoxPanel()
    {
        LootBoxPanel.gameObject.SetActive(false);
    }

    public void OpenLootBoxPanel(List<Item> items, bool value)
    {
        int indx = 0;

        if (!LootBoxPanel.gameObject.activeSelf)
        {

            LootBoxPanel.gameObject.SetActive(true);

            if (items != null)
            {
                if (items.Count > 0)
                {
                    foreach (var i in items)
                    {
                        if (i != null)
                        {
                            AddItemToLootBoxSlot(i);
                        }
                        else
                        {
                            lootBoxItemSlots[indx].ItemImage.sprite = emptySprite;
                        }

                        indx++;
                    }

                }
            }


        }
        else
        {

            LootBoxPanel.gameObject.SetActive(false);
        }
    }

    public Item AddItemToLootBoxSlot(Item item)
    {
        if (!item)
        {
            return null;
        }

        foreach (var i in lootBoxItemSlots)
        {
            if (!i.ItemInSlot)
            {
                i.ItemInSlot = item;
                Sprite sprite = emptySprite;
                foreach (var b in dataProvider.ItemsData.ItemsDatas)
                {
                    if (b.ID == item.ID)
                    {
                        sprite = b.Sprite;
                    }
                }
                i.ItemImage.sprite = sprite;
                return item;
            }
        }

        return item;
    }

    public Item AddItemToSlot(Item item)
    {
        foreach (var i in inventoryItemSlots)
        {
            if (!i.ItemInSlot)
            {
                i.ItemInSlot = item;
                Sprite sprite = emptySprite;
                foreach (var b in dataProvider.ItemsData.ItemsDatas)
                {
                    if (b.ID == item.ID)
                    {
                        sprite = b.Sprite;
                    }
                }
                i.ItemImage.sprite = sprite;
                return item;
            }
        }

        return item;
    }

    public void CloseTerminalWindow(Terminal terminal)
    {
        TerminalPanel.gameObject.SetActive(false);
    }

    public void OpenTerminalWindow(Terminal terminal)
    {

        TerminalPanel.gameObject.SetActive(true);
        terminal.UseTerminal();
    } 

    public void ClearDoorPanel()
    {
        for(int i = 0; i < DoorDataPanelTransform.childCount; i++)
        {
           Destroy(DoorDataPanelTransform.GetChild(i).gameObject);
        }
    }

    public void CreateDoorDataPanelObject(Door door, string doorId, bool doorStatus)
    {
        DoorDataPanel doorobj = Instantiate(DoorDataPanelPrefab);
        doorobj.transform.SetParent(DoorDataPanelTransform);
        doorobj.transform.localScale = Vector3.one;

        doorobj.Door = door;
        doorobj.DoorId.text = doorId;
        doorobj.SetStatus(doorStatus);

    }

    public void UpdateUI()
    {
        noizeCountSprite.fillAmount = dataProvider.CurrentMapData.Noize / 100f;
        powerCountSprite.fillAmount = dataProvider.CurrentMapData.Power / dataProvider.LevelConfig.LevelPower;
        playerHPCountSprite.fillAmount = dataProvider.Player.PlayerHP / 100f;
        if (!dataProvider.Player.CurrentWeapon)
        {
            ammoCountText.text = "00";
            return;
        }
        ammoCountText.text = (dataProvider.Player.CurrentWeapon.AmmoCount.ToString("") + " / "  + dataProvider.Player.CurrentWeapon.AmmoStorage.ToString(""));
    }

    private void OnDisable()
    {
        dataProvider.Events.OnUiUpdate -= UpdateUI;
        dataProvider.Events.OnAddItemToSlot -= AddItemToSlot;
    }


}
