using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{ 
    public Item ItemInSlot;
    public Image ItemImage;
    public int SlotID;
    private DataProvider dataProvider;
    private Vector3 oldPos;

    private void Start()
    {
        dataProvider = DataProvider.Instance;
        oldPos = ItemImage.transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!ItemInSlot)
        {
            return;
        }

        ItemImage.transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!ItemInSlot)
        {
            return;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!ItemInSlot)
        {
            return;
        }

        if (eventData.hovered.Count > 0)
        {

            if (eventData.hovered[0].name.Contains("ItemSlot"))
            {
                InventoryItemSlot targetslot = eventData.hovered[0].GetComponent<InventoryItemSlot>();

                if (targetslot.ItemInSlot)
                {

                }
                else
                {
                    print("Change item position:  " + ItemInSlot.name);

                    targetslot.ItemInSlot = ItemInSlot;
                    targetslot.ItemImage.sprite = ItemImage.sprite;
                    ItemInSlot = null;
                    ItemImage.sprite = dataProvider.BattleUI.emptySprite;
                    ItemImage.transform.localPosition = oldPos;
                }

            }
            else
            {
                ItemImage.transform.localPosition = oldPos;
            }
        }
        else
        {
            dataProvider.Player.Inventory.RemovItemFromList(ItemInSlot);
            ItemInSlot.gameObject.transform.position = dataProvider.Player.ItemDropPoint.position;
            ItemInSlot.gameObject.SetActive(true);
            ItemInSlot = null;
            ItemImage.sprite = dataProvider.BattleUI.emptySprite;
            ItemImage.transform.localPosition = oldPos;

        }

    }
}
