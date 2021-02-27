using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAction : MonoBehaviour
{
    public ActionTypes ActionType;
    public ActionTriggerType TriggerType;
    [SerializeField] private GameObject door;
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private bool isActive;


    private void Start()
    {
        switch (ActionType)
        {
            case ActionTypes.DoorOpen:
                DataProvider.Instance.Events.OnDoorOpenEvent += OpenDoor;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(TriggerType != ActionTriggerType.TriggerEnter || !isActive)
        {
            return;
        }

        if(other.tag == "Player")
        {
            Action();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (TriggerType != ActionTriggerType.TriggerEnter || !isActive)
        {
            return;
        }

        if (other.tag == "Player")
        {
            Action();
        }
    }

    public void Action()
    {
        switch (ActionType)
        {
            case ActionTypes.DoorOpen:
                Animator anim = door.GetComponent<Animator>();
                anim.SetBool("Open", !anim.GetBool("Open"));
                DataProvider.Instance.Events.DoorOpenEvent(this, this);
                break;
            default:
                break;
        }
    }

    public ObjectAction OpenDoor(ObjectAction action)
    {
        return this;
    }
}
