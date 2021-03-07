using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public List<EnemyDataObject> EnemyDataObjects = new List<EnemyDataObject>();
}

[System.Serializable]
public class EnemyDataObject
{
    public string EnemyID;
    public float EnemyMemoryTimer;
    public GameObject EnemyPrefab;
}
