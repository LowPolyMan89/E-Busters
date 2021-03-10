﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    private DataProvider dataProvider;

    [SerializeField] private Image bulletCountSprite;
    [SerializeField] private Image noizeCountSprite;
    [SerializeField] private Image powerCountSprite;
    [SerializeField] private Text ammoCountText;
    public TerminalPanel TerminalPanel;
    public DoorDataPanel DoorDataPanelPrefab;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Transform DoorDataPanelTransform;
    [SerializeField] private List<InventoryItemSlot> inventoryItemSlots = new List<InventoryItemSlot>();

    private void Start()
    {
        dataProvider = DataProvider.Instance;
        dataProvider.Events.OnUiUpdate += UpdateUI;
        dataProvider.Events.OnAddItemToSlot += AddItemToSlot;
        Invoke("UpdateUI", 0.5f);
    }

    public void CreateInventoryItemSlot(InventoryItemSlot inventoryItemSlot)
    {
        inventoryItemSlots.Add(inventoryItemSlot);
    }

    public Item AddItemToSlot(Item item)
    {
        foreach(var i in inventoryItemSlots)
        {
            if(!i.ItemInSlot)
            {
                i.ItemInSlot = item;
                Sprite sprite = emptySprite;
                foreach(var b in dataProvider.ItemsData.ItemsDatas)
                {
                    if(b.ID == item.ID)
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

    public void OpenTerminalWindow(Terminal terminal)
    {
        if (dataProvider.ClosesTerminal == null)
        {
            TerminalPanel.gameObject.SetActive(false);
            return;
        }

        if (!TerminalPanel.gameObject.activeSelf)
        {
            TerminalPanel.gameObject.SetActive(true);
            terminal.UseTerminal();
        }
        else
        {
            TerminalPanel.gameObject.SetActive(false);
        }
             
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
        bulletCountSprite.fillAmount = (float)(dataProvider.CurrentWeapon.AmmoCount / dataProvider.CurrentWeapon.weaponData.AmmoCount);
        noizeCountSprite.fillAmount = dataProvider.CurrentMapData.Noize / 100f;
        powerCountSprite.fillAmount = dataProvider.CurrentMapData.Power / dataProvider.LevelConfig.LevelPower;
        ammoCountText.text = dataProvider.CurrentWeapon.AmmoStorage.ToString("");
    }

    private void OnDisable()
    {
        dataProvider.Events.OnUiUpdate -= UpdateUI;
        dataProvider.Events.OnAddItemToSlot -= AddItemToSlot;
    }
}
