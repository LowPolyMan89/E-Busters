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
    public bool isLootBox = false;
    public LootBox LootBox;

    private void Start()
    {
        dataProvider = DataProvider.Instance;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!ItemInSlot)
        {
            return;
        }

        if(isLootBox)
        {
            LootBox lootBox = (LootBox)dataProvider.Player.ClosesActionObject;

            lootBox.RemoveItem(ItemInSlot, SlotID);
        }

        ItemImage.transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!ItemInSlot)
        {
            return;
        }
        else
        {
            oldPos = ItemImage.transform.localPosition;
        }
 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!ItemInSlot)
        {
            return;
        }

        LootBox lootBox = null;

        if (eventData.hovered.Count > 0)
        {
          //  print(eventData.hovered[0]);

            bool check = false;
            int indx = 0;
            

            foreach (var c in eventData.hovered)
            {
                if(c.gameObject.GetComponent<InventoryItemSlot>())
                {
                    check = true;
                    break;
                }
                indx++;
            }

            if (check)
            {
                InventoryItemSlot targetslot = eventData.hovered[indx].GetComponent<InventoryItemSlot>();

                if (targetslot.ItemInSlot)
                {

                }
                else
                {
                    if (targetslot.isLootBox)
                    {
                        print("Move to LootBox");
                        dataProvider.Player.Inventory.RemovItemFromList(ItemInSlot);

                        if (dataProvider.Player.ClosesActionObject)
                        {
                            lootBox = (LootBox)dataProvider.Player.ClosesActionObject;

                            lootBox.AddItem(ItemInSlot, targetslot.SlotID);
                        }

                    }
                    else
                    {
                        print("Pick froom LootBox");
                        dataProvider.Player.Inventory.AddItemToList(ItemInSlot);
                    }

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
            if(isLootBox)
            {
                lootBox = (LootBox)dataProvider.Player.ClosesActionObject;

                if(lootBox)
                    lootBox.RemoveItem(ItemInSlot);
            }
            else
            {
                dataProvider.Player.Inventory.RemovItemFromList(ItemInSlot);
            }
         
            ItemInSlot.gameObject.transform.position = dataProvider.Player.ItemDropPoint.position;
            ItemInSlot.gameObject.SetActive(true);
            ItemInSlot = null;
            ItemImage.sprite = dataProvider.BattleUI.emptySprite;
            ItemImage.transform.localPosition = oldPos;

        }

    }
}
