using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public event Func<float, DamegableObject, DamegableObject> OnBulletHitEvent;
    public event Func<ObjectAction, ObjectAction> OnDoorOpenEvent;
    public event Action OnUiUpdate;
    public event Action OnBulletCountChange;
    public event Func<float, float> OnMapNoizeChange;

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

}
