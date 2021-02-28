using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    private static DataProvider instance;
    public static DataProvider Instance { get => instance;}
    public List<EnemySpawner> EnemySpawners;
    public Weapon CurrentWeapon;
    public EventSystem Events;
    public MapData CurrentMapData;
    public Player Player;
    public EnemyData EnemyData;
    public LevelConfig LevelConfig;

    private void Awake()
    {
        Player = GameObject.FindObjectOfType<Player>();
        EnemySpawners.AddRange(GameObject.FindObjectsOfType<EnemySpawner>());

        if (instance == null)
        {
            instance = this;
        }
    }

}
