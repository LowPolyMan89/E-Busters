using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    private bool isCanActivate;
    public DataProvider dataProvider;
    public Animator animator;
    public bool isActive = true;
    [SerializeField] private Renderer material;
    [SerializeField] private bool isOpen = false;

    private void Start()
    {
        dataProvider = DataProvider.Instance;
        CheckActive();
        if(dataProvider)
            dataProvider.Events.OnOpenTerminalEvent += Open;

        StartCoroutine(SlowUpdate());
    }

    private IEnumerator SlowUpdate()
    {
        yield return new WaitForSeconds(0.4f);
        CheckActive();
        StartCoroutine(SlowUpdate());
    }

    public Terminal Open(Terminal terminal)
    {
        if(terminal != this)
        {
            return null;
        }

        if(isActive)
        {
            OpenTerminalPanel(this);
            return this;
        }

        return null;
            
    }

    private void CheckActive()
    {
        if(!material)
        {
            return;
        }

        if(isActive)
        {
           material.materials[0].SetColor("_EmissionColor", Color.green);
        }
        else
        {
            material.materials[0].SetColor("_EmissionColor", Color.red);
        }
    }

    public virtual void UseTerminal()
    {

    }

    private void OpenTerminalPanel(Terminal terminal)
    {
        if(terminal != this)
        {
            return;
        }

        if(!isOpen)
        {
            dataProvider.BattleUI.OpenTerminalWindow(terminal);
        }
        else
        {
            dataProvider.BattleUI.CloseTerminalWindow(terminal);
        }

        isOpen = !isOpen;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!isActive)
            {
                return;
            }
            dataProvider.Player.ClosesTerminal = this;
            if(animator)
            {
                animator.SetBool("Open", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isActive)
            {
                return;
            }

            dataProvider.Player.ClosesTerminal = null;
            dataProvider.BattleUI.CloseTerminalWindow(this);
            if(animator)
            {
                animator.SetBool("Open", false);
            }
        }
    }

    private void OnDestroy()
    {

    }
}
