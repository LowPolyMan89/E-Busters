﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject
{
    public float EnemyTriggerRadius;
    public float LevelPower;
    public float PowerPerSecond;
    public float DangerStatusValue;
    public float RechargingSpeed;
}
