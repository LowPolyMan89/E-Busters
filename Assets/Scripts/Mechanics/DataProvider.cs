using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    private static DataProvider instance;
    public static DataProvider Instance { get => instance;}
    public List<EnemySpawner> EnemySpawners;
    public EventSystem Events;
    public MapData CurrentMapData;
    public ItemsData ItemsData;
    public Player Player;
    public EnemyData EnemyData;
    public LevelConfig LevelConfig;
    public Terminal ClosesTerminal;
    public ObjectAction ClosesActionObject;
    public BattleUI BattleUI;

    private void Awake()
    {
        Player = GameObject.FindObjectOfType<Player>();
        EnemySpawners.AddRange(GameObject.FindObjectsOfType<EnemySpawner>());
        BattleUI = GameObject.FindObjectOfType<BattleUI>();
        if (instance == null)
        {
            instance = this;
        }
    }

}
