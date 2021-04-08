using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootBox : ObjectAction
{
   [SerializeField] private List<Item> items = new List<Item>(4);

    public List<Item> GetItems()
    {
        return items;
    }
    public void AddItem(Item item, int slotId)
    {
        items[slotId] = item;
    }

    private void Start()
    {
        DataProvider.Instance.Events.OnInteractiveAction += Action;
    }
    public override void Action()
    {
        if (DataProvider.Instance.Player.ClosesActionObject != this)
        {
            return;
        }

        if (!DataProvider.Instance.Player.Inventory.GetItem(RequiredItemId) && RequiredItemId != "")
        {
            return;
        }

        DataProvider.Instance.BattleUI.OpenLootBoxPanel(items, true);
    }

    internal void RemoveItem(Item itemInSlot, int slotId)
    {
        items[slotId] = null;
    }
    internal void RemoveItem(Item itemInSlot)
    {
        if(items.Contains(itemInSlot))
             items.Remove(itemInSlot);
    }
}
