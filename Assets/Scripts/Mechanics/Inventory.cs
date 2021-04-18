using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> NearestItem = new List<Item>();
    [SerializeField] private List<Item> items = new List<Item>();
    private DataProvider dataProvider;

    private void Start()
    {
        dataProvider = DataProvider.Instance;
    }

    public void EquipItem(Item item)
    {
        switch(item.Type)
        {
            case Item.ItemType.Weapon:
                dataProvider.Player.AddWeaponToPlayer((Weapon)item);
                break;
            case Item.ItemType.Usable:
                dataProvider.Player.EquipItemToPlyaer(item);
                break;
        }
    }

    public void UnEquipItem(Item item)
    {
        switch (item.Type)
        {
            case Item.ItemType.Weapon:
                dataProvider.Player.RemovePlayerWeapon((Weapon)item);
                break;
            case Item.ItemType.Usable:
                dataProvider.Player.RemoveItemFromPlyaer(item);
                break;
        }
    }

    public void PickUpItems()
    {
        if(NearestItem.Count > 0)
        {
            NearestItem[0].gameObject.SetActive(false);
            AddItemToList(NearestItem[0]);
            RemovNearestItem(NearestItem[0]);        
        }
    }

    public void AddItemToList(Item item)
    {
        if (items.Contains(item))
        {
            return;
        }
        else
        {
            items.Add(item);
            dataProvider.Events.AddItemToSlot(item, item);
        }

    }

    public bool CheckNearItemInList(Item item)
    {
        return NearestItem.Contains(item);
    }

    internal Item GetItem(string id)
    {
        Item item = null;

        foreach(var i in items)
        {
            if(i.ID == id)
            {
                item = i;
                return item;
            }
        }

        return item;
    }

    public bool CheckItemInList(Item item)
    {
        return items.Contains(item);
    }

    public void RemovItemFromList(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }

    public void AddNearItemToList(Item item)
    {
        if (NearestItem.Contains(item))
        {
            return;
        }
        else
        {
            NearestItem.Add(item);
        }

    }

    public void RemovNearestItem(Item item)
    {
        if (NearestItem.Contains(item))
        {
            NearestItem.Remove(item);
        }
    }
}
