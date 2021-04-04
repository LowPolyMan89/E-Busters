using AmplifyShaderEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{

    [SerializeField] private float noize;
    [SerializeField] private float power;
    [SerializeField] private bool isPowerStop;
    [SerializeField] private List<Door> allDoors = new List<Door>();
    [SerializeField] private List<GameObject> energyStatusGroup = new List<GameObject>(3);
    [SerializeField] private EnergyStatus energyStatus = EnergyStatus.Normal;
    [SerializeField] private bool isRecharging = false;

    internal void OpenAllDoors()
    {
        print("Warning! All Doors Open!!");

        foreach (var d in allDoors)
        {
            if(!d.IsEmergencyStatus)
                d.EmergencyOpen();
        }
    }

    internal void NormalAllDoors()
    {
        foreach (var d in allDoors)
        {
            if (d.IsEmergencyStatus)
                d.NormalStatus();
        }
    }


    private float dangerStatusValue;

    public float Noize { get => noize; }
    public float Power { get => power; }
    public bool IsPowerStop { get => isPowerStop; }
    public List<Door> AllDoors { get => allDoors; }

    private DataProvider dataProvider;

    private void Start()
    {
        dataProvider = DataProvider.Instance;
        power = dataProvider.LevelConfig.LevelPower;
        dangerStatusValue = dataProvider.LevelConfig.DangerStatusValue;
        dataProvider.Events.OnMapNoizeChange += ChangeNoize;
        dataProvider.Events.OnMapPowerChange += ChangePower;
        allDoors.AddRange(GameObject.FindObjectsOfType<Door>());
        StartCoroutine(PowerChanger());
    }

    private IEnumerator PowerChanger()
    {
        yield return new WaitForSeconds(1f);
        if (!isPowerStop)
        {
            if (!isRecharging)
            {
                float value = dataProvider.LevelConfig.PowerPerSecond;
                dataProvider.Events.PowerChangeEvent(-value, -value);
            }
            CheckEnergyStatus();
        }
        StartCoroutine(PowerChanger());
    }

    private void CheckEnergyStatus()
    {
        if(energyStatusGroup.Count < 1)
        {
            return;
        }

        if(isRecharging)
        {
            float value = dataProvider.LevelConfig.RechargingSpeed;
            dataProvider.Events.PowerChangeEvent(value, value);

            print("Power level: " + power);
            if(power > dangerStatusValue)
            {
                isRecharging = false;
            }

            return;
        }

        if(power >= dangerStatusValue)
        {
            energyStatus = EnergyStatus.Normal;
            energyStatusGroup[0].SetActive(true);
            energyStatusGroup[1].SetActive(false);
            energyStatusGroup[2].SetActive(false);
            print("Normal energy status!");
            NormalAllDoors();

        }
        if(power < dangerStatusValue && power != 0)
        {
            energyStatus = EnergyStatus.Danger;
            energyStatusGroup[0].SetActive(false);
            energyStatusGroup[1].SetActive(true);
            energyStatusGroup[2].SetActive(false);
            print("Danger energy status!");
            OpenAllDoors();
        }
        if(power <= 0)
        {
            isRecharging = true;
            energyStatus = EnergyStatus.None;
            energyStatusGroup[0].SetActive(false);
            energyStatusGroup[1].SetActive(false);
            energyStatusGroup[2].SetActive(true);
            print("Critical energy status! Start recharging!");
        }
    }



    public float ChangeNoize(float value)
    {
        if(energyStatus == EnergyStatus.Normal)
        {
            return value;
        }

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

    public float ChangePower(float value)
    {
        power += value;

        if (power >= 100f)
        {
            power = 0f;
        }

        if (power <= 0)
        {
            power = 0f;
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
        dataProvider.Events.OnMapPowerChange -= ChangePower;
    }


}

public enum EnergyStatus
{
    Normal, Danger, None
}
