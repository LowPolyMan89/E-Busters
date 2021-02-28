using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    public PlayerStats PlayerStats { get => playerStats; }
}
