using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegableObject : MonoBehaviour
{

    [SerializeField] private StatsData statsData;
    [SerializeField] private float objectHitPoint;
    public float ObjectHitPoint { get => objectHitPoint; }

    private void Start()
    {
        if(!statsData)
        {
            objectHitPoint = 0;
        }
        else
        {
            objectHitPoint = statsData.HitPoint;
        }

        DataProvider.Instance.Events.OnBulletHitEvent += TakeDamage;
    }

    public DamegableObject TakeDamage(float value, DamegableObject damegableObject)
    {
        if(damegableObject == this)
        {

            objectHitPoint -= value;

            if(objectHitPoint <= 0)
            {
                Dead();
            }
        }
        return this;
    }

    private void Dead()
    {
        Destroy(this.gameObject);
    }


    private void OnDisable()
    {
        DataProvider.Instance.Events.OnBulletHitEvent -= TakeDamage;
    }
}
