using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootBox : ObjectAction
{
   [SerializeField] private Item[] items = new Item[4];

    public Item[] GetItems()
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
        if (DataProvider.Instance.ClosesActionObject != this)
        {
            return;
        }

        if (!DataProvider.Instance.Player.Inventory.GetItem(RequiredItemId) && RequiredItemId != "")
        {
            return;
        }

        DataProvider.Instance.BattleUI.OpenLootBoxPanel(items.ToList<Item>(), true);
    }

}
