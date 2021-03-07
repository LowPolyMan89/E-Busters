using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAction : MonoBehaviour
{
    public ActionTypes ActionType;
    public ActionTriggerType TriggerType;
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private bool isActive;

    public bool IsActive { get => isActive; }

    private void Start()
    {

    }

    public virtual void SetActive(bool value)
    {
        isActive = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(TriggerType != ActionTriggerType.TriggerEnter || !isActive)
        {
            return;
        }
        
        if(other.tag == "Player" || other.tag == "Enemy")
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

        if (other.tag == "Player" || other.tag == "Enemy")
        {
            Action();
        }
    }

    public virtual void Action()
    {

    }


}
