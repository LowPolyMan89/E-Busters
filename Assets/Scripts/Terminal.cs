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

    private void Start()
    {
        dataProvider = DataProvider.Instance;
        CheckActive();
        if(dataProvider)
            dataProvider.Events.OnInteractiveAction += Open;

        StartCoroutine(SlowUpdate());
    }

    private IEnumerator SlowUpdate()
    {
        yield return new WaitForSeconds(0.4f);
        CheckActive();
        StartCoroutine(SlowUpdate());
    }

    public void Open()
    {
        if(isActive)
            OpenTerminalPanel(this);
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
        dataProvider.BattleUI.OpenTerminalWindow(terminal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!isActive)
            {
                return;
            }
            dataProvider.ClosesTerminal = this;
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

            dataProvider.ClosesTerminal = null;
            dataProvider.BattleUI.OpenTerminalWindow(this);
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
