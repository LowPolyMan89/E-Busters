using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{
    [SerializeField] private WeaponData Data;
    private bool isFireRate = false;
    [SerializeField] private float angle;
    private void Start()
    {
        base.weaponData = Data;

        CreateBullet();

    }

    public override void PickUpItems()
    {
        base.PickUpItems();
    }

    public void CreateBullet()
    {
        angle = Data.Angle;

        var shape = BulletVfx.shape;
        shape.angle = angle;


        BulletVfx.maxParticles = (int)Data.AmmoInShoot;
        BulletVfx.startSpeed = Data.Range / 0.1f;
    }

    public override void Shoot()
    {
        if (base.isReload || isFireRate)
        {
            return;
        }
        else
        {
            StartCoroutine(ShootRate());
        }
    }

    private IEnumerator ShootRate()
    {
        isFireRate = true;
        base.StartVfx();
        base.RemoveAmmo(Data.AmmoInShoot);
        yield return new WaitForSeconds(Data.FireRate);
        isFireRate = false;
    }
}
