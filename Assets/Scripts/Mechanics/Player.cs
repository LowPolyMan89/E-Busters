using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform itemDropPoint;
    public PlayerStats PlayerStats { get => playerStats; }
    public Inventory Inventory { get => inventory;}
    public Transform ItemDropPoint { get => itemDropPoint; }

    [SerializeField] private Inventory inventory;
    
    


}
