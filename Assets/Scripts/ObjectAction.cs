﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAction : MonoBehaviour
{
    public ActionTypes ActionType;
    public ActionTriggerType TriggerType;
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private bool isActive;
    [SerializeField] private string requiredItemId;
    [SerializeField] private DataProvider dataProvider;

    public bool IsActive { get => isActive; }
    public string RequiredItemId { get => requiredItemId;}


    public virtual void SetActive(bool value)
    {
        isActive = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        dataProvider = DataProvider.Instance;

        if (TriggerType == ActionTriggerType.PressE && isActive)
        {
            dataProvider.Player.ClosesActionObject = this;

            return;
        }

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
        dataProvider.Player.ClosesActionObject = null;

        if (TriggerType == ActionTriggerType.PressE && isActive)
        {

            if(ActionType == ActionTypes.LootBoxOpen)
            {
                dataProvider.BattleUI.CloseLootBoxPanel();
            }

            return;
        }

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
