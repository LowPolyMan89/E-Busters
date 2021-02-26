using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    private static DataProvider instance;
    public static DataProvider Instance { get => instance;}

    public Weapon CurrentWeapon;
    public EventSystem Events;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
