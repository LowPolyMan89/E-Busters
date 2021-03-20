using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
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
    [SerializeField] private InventoryItemSlot inventoryItemSlotSelected = null;
    [SerializeField] private bool isDrag;

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

    private void Update()
    {
        if(inventoryItemSlotSelected != null && Input.GetMouseButton(0) && inventoryItemSlotSelected.ItemInSlot != null)
        {
            isDrag = true;
        }
        else
        {
            isDrag = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.hovered[0].name.Contains("ItemSlot"))
        {
            inventoryItemSlotSelected = eventData.hovered[0].GetComponent<InventoryItemSlot>();
        }
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.hovered[0].name.Contains("ItemSlot"))
        {
            inventoryItemSlotSelected = null;
        }
    }
}
