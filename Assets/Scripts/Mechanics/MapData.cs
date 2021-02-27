using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    
    [SerializeField] private float noize;

    public float Noize { get => noize;}

    private void Start()
    {
        DataProvider.Instance.Events.OnMapNoizeChange += ChangeNoize;
    }

    public float ChangeNoize(float value)
    {
        noize += value;

        if (noize >= 100f)
        {
            noize = 100f;
        }

        if(noize <= 0)
        {
            noize = 0f;
        }

        
        return value;
    }

    private void OnDisable()
    {
        DataProvider.Instance.Events.OnMapNoizeChange -= ChangeNoize;
    }
}
