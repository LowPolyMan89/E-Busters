using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public event Func<float, DamegableObject, DamegableObject> OnBulletHitEvent;
    public event Func<DoorTerminal, DoorTerminal> OnDoorTerminalOpen;
    public event Func<ObjectAction, ObjectAction> OnDoorOpenEvent;
    public event Action OnUiUpdate;
    public event Action OnBulletCountChange;
    public event Func<float, float> OnMapNoizeChange;
    public event Func<float, float> OnMapPowerChange;
    public event Func<EnemySpawner, EnemySpawner> OnSpawnEnemyEvent;
    public event Action OnInteractiveAction;
    public event Func<Item, Item> OnAddItemToSlot;


    public EnemySpawner SpawnEnemy(EnemySpawner outValu, EnemySpawner spawner)
    {
        if (OnSpawnEnemyEvent != null)
        {
            print("Event: SpawnEnemy = " + spawner.SpawnEnemyID);
            OnSpawnEnemyEvent(spawner);
        }

        return spawner;
    }

    public Item AddItemToSlot(Item outValu, Item inValue)
    {
        if (OnAddItemToSlot != null)
        {
            OnAddItemToSlot(inValue);
        }

        return inValue;
    }

    public DoorTerminal DoorTerminalOpen(DoorTerminal outValu, DoorTerminal terminal)
    {
        if (OnDoorTerminalOpen != null)
        {
            OnDoorTerminalOpen(terminal);
        }

        return terminal;
    }

    public ObjectAction DoorOpenEvent(ObjectAction inValue, ObjectAction outValue)
    {
        if(OnDoorOpenEvent != null)
        {
            OnDoorOpenEvent(outValue);
        }

        return outValue;
    }

    public DamegableObject BulletHitEvent(float damage, DamegableObject damegableObject)
    {
        if (OnBulletHitEvent != null)
        {
            OnBulletHitEvent(damage, damegableObject);
        }

        return damegableObject;
    }

    public float NoizeChangeEvent(float inValue, float outValue)
    {

        if (OnBulletHitEvent != null)
        {
            OnMapNoizeChange(outValue);
            UiUpdate();
        }

        return outValue;
    }

    public float PowerChangeEvent(float inValue, float outValue)
    {

        if (OnMapPowerChange != null)
        {
            OnMapPowerChange(outValue);
            UiUpdate();
        }

        return outValue;
    }

    public void BulletCountChange() //call on bullet count change
    {
        if (OnBulletCountChange != null)
        {
            OnBulletCountChange();
        }
    }

    public void UiUpdate() 
    {
        if (OnUiUpdate != null)
        {
            OnUiUpdate();
        }
    }

    public void EnteractiveAction()
    {
        if (OnInteractiveAction != null)
        {
            OnInteractiveAction();
        }
    }

}
