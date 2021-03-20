using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [ContextMenu("Act")]
    public void Activate()
    {
        animator.SetBool("Activate", !animator.GetBool("Activate"));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
