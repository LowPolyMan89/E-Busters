using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float reloadTime;
    [SerializeField] private float ammoCount;
    [SerializeField] private float ammoStorage;
    [SerializeField] private VfxStarter vfxStarter;
    public ParticleSystem BulletVfx;
    public bool isReload = false;

    private DataProvider _DataProvider;

    [HideInInspector] public WeaponData weaponData;


    public float ReloadTime { get => reloadTime;}
    public float AmmoCount { get => ammoCount;}
    public float AmmoStorage { get => ammoStorage; }

    public void CreateWeapon()
    {
        _DataProvider = DataProvider.Instance;
        reloadTime = weaponData.ReloadTime;
        ammoCount = weaponData.AmmoCount;
        ammoStorage = weaponData.AmmoStorage;

        _DataProvider.CurrentWeapon = this;   
        
    }

    public void StartVfx()
    {
        if(ammoCount > 0)
        {
            vfxStarter.Activate();
        }
    }

    public void RemoveAmmo(float value)
    {
        ammoCount -= value;
        if(ammoCount < 0)
        {
            ammoCount = 0;
        }

        _DataProvider.Events.UiUpdate();

    }

    public virtual void Shoot()
    { }

    public virtual void Reload(float time)
    {
        if(ammoStorage <= 0)
        {
            print("Ammo empty!");
            return;
        }

        if(!isReload && ammoCount < weaponData.AmmoCount)
        {
            print("Start reload");
            StartCoroutine(Reloading(weaponData.ReloadTime));
        }
            
        else
        {
            print("Ammo full");
        }
    }

    private IEnumerator Reloading(float time)
    {
        isReload = true;
        yield return new WaitForSeconds(time);


        if(weaponData.AmmoCount - ammoCount <= ammoStorage)
        {
            print("Add ammo from storage");
            ammoStorage -= weaponData.AmmoCount - ammoCount;
            ammoCount = weaponData.AmmoCount;
            
        }
        else
        {
            print("Add last ammo from storage");
            ammoCount += ammoStorage;
            ammoStorage = 0;
        }

        isReload = false;
        _DataProvider.Events.UiUpdate();
    }


}
