using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private List<Collider> colliders = new List<Collider>();

    public void SwitchColliders(bool value)
    {
        foreach (var v in colliders)
        {
            v.enabled = value;
        }
    }
    public enum ItemType
    {
        Weapon, Usable
    }

    public ItemType Type;
    public bool Equiped = false;
    public string ID;

    public virtual void Use()
    {

    }

    private void Start()
    {
      //  DataProvider.Instance.Events.OnInteractiveAction += PickUpItems;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Equiped)
        {
            return;
        }

        if(other.tag == "Player")
        {
            print(gameObject.name);
            DataProvider.Instance.Player.Inventory.AddNearItemToList(this);
        }
    }

    public virtual void PickUpItems()
    {
        if(DataProvider.Instance.Player.Inventory.CheckNearItemInList(this))
        {
            DataProvider.Instance.Player.Inventory.AddItemToList(this);
            DataProvider.Instance.Player.Inventory.RemovNearestItem(this);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Equiped)
        {
            return;
        }

        if (other.tag == "Player")
        {
            // print(gameObject.name);
            DataProvider.Instance.Player.Inventory.RemovNearestItem(this);
        }
    }

    private void OnDestroy()
    {
       // DataProvider.Instance.Events.OnInteractiveAction -= PickUpItems;
    }
}
