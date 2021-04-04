using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void Start()
    {
        Invoke("StartTutorial", 1f);
    }

    private void StartTutorial()
    {
        SetPlayerHP();
    }
   
    public void SetPlayerHP()
    {
        DataProvider.Instance.Events.PlayerChangeHPEvent(-50, DataProvider.Instance.Player);
    }
}
