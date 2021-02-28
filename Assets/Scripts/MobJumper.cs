using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobJumper : AI
{
    public override void Attack()
    {
        base.EnemyWeaponRange.Shoot();
    }
}
