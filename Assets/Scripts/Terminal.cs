using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    private bool isCanActivate;
    public DataProvider dataProvider;


    private void Start()
    {
        dataProvider = DataProvider.Instance;
        if(dataProvider)
            dataProvider.Events.OnInteractiveAction += Open;
    }

    public void Open()
    {
        OpenTerminalPanel(this);
    }

    public virtual void UseTerminal()
    {

    }

    private void OpenTerminalPanel(Terminal terminal)
    {
        dataProvider.BattleUI.OpenTerminalWindow(terminal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           dataProvider.ClosesTerminal = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            dataProvider.ClosesTerminal = null;
            dataProvider.BattleUI.OpenTerminalWindow(this);
        }
    }

    private void OnDestroy()
    {

    }
}
