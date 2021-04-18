using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform itemDropPoint;
    [SerializeField] private Weapon[] playerWeapons = new Weapon[2];
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private Animator animator;
    [SerializeField] private float playerHP = 100f;
    [SerializeField] private Item itemInActiveSlot;
    [SerializeField] private Transform lookPoint;

    public Terminal ClosesTerminal;
    public ObjectAction ClosesActionObject;
    public string PlayerAreaName = "";
    public Weapon CurrentWeapon;
    public bool isWeaponReady = false;
    public PlayerStats PlayerStats { get => playerStats; }
    public Inventory Inventory { get => inventory;}
    public Transform ItemDropPoint { get => itemDropPoint; }
    public Animator Animator { get => animator; }
    public float PlayerHP { get => playerHP; }
    public Item ItemInActiveSlot { get => itemInActiveSlot; set => itemInActiveSlot = value; }
    public Transform WeaponSlot { get => weaponSlot; set => weaponSlot = value; }
    public Transform LookPoint { get => lookPoint; set => lookPoint = value; }

    [SerializeField] private Inventory inventory;

    private void Start()
    {
        DataProvider.Instance.Events.OnAddWeaponToSlot += AddWeaponToPlayer;
        DataProvider.Instance.Events.OnPlayerHpChange += SetPlayerHP;
        animator = gameObject.GetComponent<Animator>();
    }

    public Player SetPlayerHP(float value)
    {
        playerHP += value;
        if (playerHP > 100) playerHP = 100;
        return this;
    }

    public Item EquipItemToPlyaer(Item item)
    {
        item.gameObject.SetActive(true);
        DataProvider.Instance.Player.itemInActiveSlot = item;
        item.gameObject.transform.SetParent(weaponSlot);
        item.gameObject.transform.localPosition = Vector3.zero;
        item.transform.rotation = weaponSlot.rotation;
        item.transform.localEulerAngles = new Vector3(item.transform.rotation.x, item.transform.rotation.y, 0);
        item.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        item.SwitchColliders(false);
        return item;
    }

    public Item RemoveItemFromPlyaer(Item item)
    {
        item.gameObject.SetActive(true);
        DataProvider.Instance.Player.itemInActiveSlot = null;
        item.gameObject.transform.SetParent(null);
        item.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        item.SwitchColliders(true);
        return item;
    }

    public Weapon AddWeaponToPlayer(Weapon weapon)
    {
        weapon.gameObject.SetActive(true);
        DataProvider.Instance.Player.CurrentWeapon = weapon;
        weapon.gameObject.transform.SetParent(weaponSlot);
        weapon.gameObject.transform.localPosition = Vector3.zero;
        weapon.transform.rotation = weaponSlot.rotation;
        weapon.transform.localEulerAngles = new Vector3(weapon.transform.rotation.x, weapon.transform.rotation.y, 0);
        weapon.CreateWeapon();
        weapon.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        weapon.SwitchColliders(false);
        isWeaponReady = true;
        return weapon;
    }

    public Weapon RemovePlayerWeapon(Weapon weapon)
    {
        weapon.gameObject.SetActive(true);
        DataProvider.Instance.Player.CurrentWeapon = null;
        weapon.gameObject.transform.SetParent(null);
        weapon.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        weapon.SwitchColliders(true);
        isWeaponReady = false;
        return weapon;
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerAreaName = other.name;
    }

    private void OnDestroy()
    {
        DataProvider.Instance.Events.OnAddWeaponToSlot -= AddWeaponToPlayer;
        DataProvider.Instance.Events.OnPlayerHpChange -= SetPlayerHP;
    }

}
