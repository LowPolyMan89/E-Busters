using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public float MoveSpeed;
    public float SprintSpeed;
    public float AimSpeed;
    public float NoizePerSprint;
}
