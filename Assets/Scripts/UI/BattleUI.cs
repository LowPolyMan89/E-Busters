using System.Collections;
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
    [SerializeField] private Text ammoCountText;
    public TerminalPanel TerminalPanel;
    public DoorDataPanel DoorDataPanelPrefab;
    public Sprite emptySprite;
    [SerializeField] private Transform DoorDataPanelTransform;
    [SerializeField] private List<InventoryItemSlot> inventoryItemSlots = new List<InventoryItemSlot>();
    [SerializeField] private Transform canvas;
    [SerializeField] private List<ItemInfoPanel> itemInfoPanels = new List<ItemInfoPanel>();
    [SerializeField] private Vector3 itemInfoPanelOffset;
    private float reloadTime = 0f;
    private void Start()
    {
        dataProvider = DataProvider.Instance;
        dataProvider.Events.OnUiUpdate += UpdateUI;
        dataProvider.Events.OnAddItemToSlot += AddItemToSlot;
        Invoke("UpdateUI", 0.5f);
    }

    private void Update()
    {
        int activecount = 0;
        int currentitem = 0;
        activecount = dataProvider.Player.Inventory.NearestItem.Count;

        foreach(var i in itemInfoPanels)
        {
            if(activecount == 0)
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

            if(currentitem + 1 < activecount )
                currentitem++;
        }

        if(dataProvider.CurrentWeapon.isReload)
        {
            reloadTime += Time.deltaTime;
            reloadImage.fillAmount = reloadTime / dataProvider.CurrentWeapon.ReloadTime;
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
        noizeCountSprite.fillAmount = dataProvider.CurrentMapData.Noize / 100f;
        powerCountSprite.fillAmount = dataProvider.CurrentMapData.Power / dataProvider.LevelConfig.LevelPower;
        ammoCountText.text = (dataProvider.CurrentWeapon.AmmoCount.ToString("") + " / "  + dataProvider.CurrentWeapon.AmmoStorage.ToString(""));
    }

    private void OnDisable()
    {
        dataProvider.Events.OnUiUpdate -= UpdateUI;
        dataProvider.Events.OnAddItemToSlot -= AddItemToSlot;
    }


}
