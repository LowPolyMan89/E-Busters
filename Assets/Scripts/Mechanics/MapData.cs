using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{

    [SerializeField] private float noize;

    public float Noize { get => noize; }

    private DataProvider dataProvider;

    private void Start()
    {
        dataProvider = DataProvider.Instance;
        dataProvider.Events.OnMapNoizeChange += ChangeNoize;

    }

    public float ChangeNoize(float value)
    {
        noize += value;

        if (noize >= 100f)
        {
            SpawnNewEnemy();
            noize = 0f;
        }

        if (noize <= 0)
        {
            noize = 0f;
        }


        return value;
    }

    private void SpawnNewEnemy()
    {
        EnemySpawner enemySpawner = SelectSpawnerFromDist(dataProvider.LevelConfig.EnemyTriggerRadius, dataProvider.Player.transform.position);

        if(enemySpawner)
            dataProvider.Events.SpawnEnemy(enemySpawner, enemySpawner);
    }

    private EnemySpawner SelectSpawnerFromDist(float radius, Vector3 origin)
    {
        EnemySpawner enemySpawner = null;

        foreach(var spawner in dataProvider.EnemySpawners)
        {
            if(spawner.SpawnCapacity > 0)
            {
                if(Vector3.Distance(origin, spawner.transform.position) <= radius)
                {
                    enemySpawner = spawner;
                    break;
                }
            }
        }

        return enemySpawner;
    }

    private void OnDestroy()
    {
        DataProvider.Instance.Events.OnMapNoizeChange -= ChangeNoize;
    }
}
