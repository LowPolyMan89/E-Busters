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
