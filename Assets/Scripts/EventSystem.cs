using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public event Func<float, DamegableObject, DamegableObject> OnBulletHitEvent;
    public event Action OnUiUpdate;
    public event Action OnBulletCountChange;

    public DamegableObject BulletHitEvent(float damage, DamegableObject damegableObject)
    {
        if(OnBulletHitEvent != null)
        {
            OnBulletHitEvent(damage, damegableObject);
        }

        return damegableObject;
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
