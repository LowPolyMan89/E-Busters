using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicDoor : ObjectAction
{
    [SerializeField] private GameObject door;
    [SerializeField] private string doorId;
    [SerializeField] private GameObject redColors;
    [SerializeField] private GameObject greenColors;
    [SerializeField] private bool isEmergencyStatus = false;
    public string DoorId { get => doorId; }
    public bool IsEmergencyStatus { get => isEmergencyStatus; }

    private void Start()
    {
        DataProvider.Instance.Events.OnDoorOpenEvent += OpenDoor;
        DataProvider.Instance.Events.OnInteractiveAction += Action;
        Activate(base.IsActive);
    }
    public override void Action()
    {
        if(DataProvider.Instance.Player.ClosesActionObject != this)
        {
            return;
        }
    

        if (!DataProvider.Instance.Player.Inventory.GetItem(RequiredItemId) && RequiredItemId != "")
        {
            return;
        }

        Animator anim = door.GetComponent<Animator>();
        print("Door open: " + this.GetInstanceID());
        anim.SetBool("Open", !anim.GetBool("Open"));
        DataProvider.Instance.Events.DoorOpenEvent(this, this);
    }

    public void EmergencyOpen()
    {

    }

    public void NormalStatus()
    {

    }

    public void Activate(bool v)
    {
        if (v == true)
        {
            redColors.SetActive(false);
            greenColors.SetActive(true);
        }
        else
        {
            redColors.SetActive(true);
            greenColors.SetActive(false);
        }
        base.SetActive(v);
    }

    public ObjectAction OpenDoor(ObjectAction action)
    {
        return this;
    }

    private void OnDestroy()
    {
        DataProvider.Instance.Events.OnInteractiveAction -= Action;
    }
}
