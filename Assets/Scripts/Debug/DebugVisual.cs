using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVisual : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] private bool isDrawPlayerDirection = false;
    [SerializeField] private bool isDrawWeaponDirection = false;
    [SerializeField] private bool isDrawWeaponPointDirection = false;

    private Player player;
    private DataProvider dataProvider;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        dataProvider = DataProvider.Instance;
    }

    private void Update()
    {
        if (isDrawPlayerDirection)
            DrawPlayerDirection();
        if (isDrawWeaponDirection)
            DrawWeaponDirection();
        if (isDrawWeaponPointDirection)
            DrawWeaponPointDirection();
    }

    private void DrawPlayerDirection()
    {
        if (!player)
            return;

        Debug.DrawRay(player.LookPoint.position, player.LookPoint.forward * 20f, Color.blue);
    }

    private void DrawWeaponDirection()
    {
        if (!player)
            return;
        if (!player.CurrentWeapon)
            return;

        Transform firepoint = player.CurrentWeapon.FirePoint;

        Debug.DrawRay(firepoint.position, firepoint.forward, Color.red);

    }

    private void DrawWeaponPointDirection()
    {
        if (!player)
            return;

        Transform firepoint = player.WeaponSlot;

        Debug.DrawRay(firepoint.position, firepoint.forward, Color.green);

    }

#endif
}
