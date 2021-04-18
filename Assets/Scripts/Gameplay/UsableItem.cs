using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : Item
{
    [SerializeField] private int useCount;

    public int UseCount { get => useCount; }

    public virtual void UseItem()
    {
        useCount--;
        if(useCount <= 0)
        {
            DataProvider.Instance.Player.Inventory.UnEquipItem(this);
            DataProvider.Instance.Player.Inventory.RemovItemFromList(this);
            DataProvider.Instance.Player.Inventory.RemovNearestItem(this);
            DataProvider.Instance.BattleUI.RemoveActiveSlotItem();
            Destroy(this.gameObject);
        }
    }
}
