using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _reactionRadius;
    [SerializeField] private ParticleSystem particleSystem_piss;
    private NavMeshAgent agent;


    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        particleSystem_piss.Stop();
    }
    
    private void Update()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) <= _reactionRadius)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, _player.transform.position - transform.position, out hit, 200f))
            {
                if(hit.transform.tag == "Player")
                {
                    particleSystem_piss.Play();
                    agent.SetDestination(_player.transform.position);
                }
                else
                {
                    particleSystem_piss.Stop();
                }
                     

            }
            else
            {
                particleSystem_piss.Stop();
            }

        
        }
        else
        {
            particleSystem_piss.Stop();
        }
         
    }

}
