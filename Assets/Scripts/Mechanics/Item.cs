using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private DataProvider dataProvider;
    public string ID;

    private void Start()
    {
        dataProvider = DataProvider.Instance;
        dataProvider.Events.OnInteractiveAction += PickUpItems;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print(gameObject.name);
            dataProvider.Player.Inventory.AddNearItemToList(this);
        }
    }

    public void PickUpItems()
    {
        if(dataProvider.Player.Inventory.CheckNearItemInList(this))
        {
            dataProvider.Player.Inventory.AddItemToList(this);
            dataProvider.Player.Inventory.RemovNearestItem(this);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print(gameObject.name);
            dataProvider.Player.Inventory.RemovNearestItem(this);
        }
    }

    private void OnDestroy()
    {
        dataProvider.Events.OnInteractiveAction -= PickUpItems;
    }
}
