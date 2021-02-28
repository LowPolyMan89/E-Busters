using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private DataProvider dataProvider;
    [SerializeField] private string spawnEnemyID;
    [SerializeField] private int spawnCapacity;

    public int SpawnCapacity { get => spawnCapacity; }
    public string SpawnEnemyID { get => spawnEnemyID; }

    private void Start()
    {
        dataProvider = DataProvider.Instance;
        dataProvider.Events.OnSpawnEnemyEvent += SpawnEnemy;
    }

    public EnemySpawner SpawnEnemy(EnemySpawner spawner)
    {
        if(spawner != this || spawnCapacity <= 0)
        {
            return null;
        }
        EnemyDataObject enemyDataObject = dataProvider.EnemyData.EnemyDataObjects.Find(x => (x.EnemyID == spawnEnemyID));
        GameObject newEnemy = Instantiate(enemyDataObject.EnemyPrefab, transform.position, Quaternion.identity);
        newEnemy.GetComponent<AI>().enemyData = enemyDataObject;
        spawnCapacity--;
        return this;
    }

    private void OnDestroy()
    {
        dataProvider.Events.OnSpawnEnemyEvent -= SpawnEnemy;
    }
}
