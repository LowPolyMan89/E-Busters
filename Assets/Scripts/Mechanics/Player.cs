using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform itemDropPoint;
    [SerializeField] private Weapon[] playerWeapons = new Weapon[2];
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private Weapon closesWeapon;
    public Weapon CurrentWeapon;
    public PlayerStats PlayerStats { get => playerStats; }
    public Inventory Inventory { get => inventory;}
    public Transform ItemDropPoint { get => itemDropPoint; }
    public Weapon ClosesWeapon { get => closesWeapon; }

    [SerializeField] private Inventory inventory;

    private void Start()
    {
        DataProvider.Instance.Events.OnAddWeaponToSlot += AddWeaponToPlayer;
        DataProvider.Instance.Events.OnInteractiveAction += PickUpWeapon;
    }

    public void AddClosesWeapon(Weapon weapon)
    {
        closesWeapon = weapon;
    }

    public void PickUpWeapon()
    {
        AddWeaponToPlayer(closesWeapon);
    }

    public Weapon AddWeaponToPlayer(Weapon weapon)
    {
        if (!closesWeapon)
            return null;

        weapon.gameObject.transform.SetParent(weaponSlot);
        weapon.gameObject.transform.localPosition = Vector3.zero;
        weapon.transform.rotation = weaponSlot.rotation;
        weapon.CreateWeapon();
        weapon.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        weapon.gameObject.GetComponent<BoxCollider>().enabled = false;
        return weapon;
    }
    


}
