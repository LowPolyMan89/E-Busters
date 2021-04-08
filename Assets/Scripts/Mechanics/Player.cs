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

    public Terminal ClosesTerminal;
    public ObjectAction ClosesActionObject;

    public Weapon CurrentWeapon;
    public bool isWeaponReady = false;
    public PlayerStats PlayerStats { get => playerStats; }
    public Inventory Inventory { get => inventory;}
    public Transform ItemDropPoint { get => itemDropPoint; }
    public Animator Animator { get => animator; }
    public float PlayerHP { get => playerHP; }
    public Item ItemInActiveSlot { get => itemInActiveSlot; set => itemInActiveSlot = value; }

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
        return this;
    }


    public Weapon AddWeaponToPlayer(Weapon weapon)
    {

        weapon.gameObject.transform.SetParent(weaponSlot);
        weapon.gameObject.transform.localPosition = Vector3.zero;
        weapon.transform.rotation = weaponSlot.rotation;
        weapon.transform.localEulerAngles = new Vector3(weapon.transform.rotation.x, weapon.transform.rotation.y, 90);
        weapon.CreateWeapon();
        weapon.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        weapon.gameObject.GetComponent<BoxCollider>().enabled = false;
        isWeaponReady = true;
        return weapon;
    }

    private void OnDestroy()
    {
        DataProvider.Instance.Events.OnAddWeaponToSlot -= AddWeaponToPlayer;
        DataProvider.Instance.Events.OnPlayerHpChange -= SetPlayerHP;
    }

}
