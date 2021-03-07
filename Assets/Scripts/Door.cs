using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ObjectAction
{
    [SerializeField] private GameObject door;
    [SerializeField] private string doorId;
    [SerializeField] private GameObject redColors;
    [SerializeField] private GameObject greenColors;
    [SerializeField] private bool isEmergencyStatus = false;
    public string DoorId { get => doorId;}
    public bool IsEmergencyStatus { get => isEmergencyStatus; }

    private void Start()
    {
        DataProvider.Instance.Events.OnDoorOpenEvent += OpenDoor;
        Activate(base.IsActive);
    }
    public override void Action()
    {
        Animator anim = door.GetComponent<Animator>();
        print("Door open: " + this.GetInstanceID());
        anim.SetBool("Open", !anim.GetBool("Open"));
        DataProvider.Instance.Events.DoorOpenEvent(this, this);
    }

    public void EmergencyOpen()
    {
        isEmergencyStatus = true;
        Animator anim = door.GetComponent<Animator>();
        anim.SetBool("Open", true);
        DataProvider.Instance.Events.DoorOpenEvent(this, this);
        base.SetActive(false);
        Activate(false);
    }

    public void NormalStatus()
    {
        isEmergencyStatus = false;
        Animator anim = door.GetComponent<Animator>();
        anim.SetBool("Open", false);
        DataProvider.Instance.Events.DoorOpenEvent(this, this);
        base.SetActive(true);
        Activate(true);
    }

    public void Activate(bool v)
    {
        if(v == true)
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
}
