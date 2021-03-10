using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _reactionRadius;
    private NavMeshAgent agent;
    [SerializeField] private LayerMask layerMask;
    public EnemyDataObject enemyData;
    [SerializeField] private float enemyMemoryTimer;
    [SerializeField] private Weapon enemyWeaponRange;
    [SerializeField] private Weapon enemyWeaponClose;

    public float EnemyMemoryTimer { get => enemyMemoryTimer;}
    public Weapon EnemyWeaponRange { get => enemyWeaponRange; }
    public Weapon EnemyWeaponClose { get => enemyWeaponClose; }

    public virtual void Attack()
    {

    }

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Update()
    {
        if(enemyMemoryTimer > 0 )
        {
            enemyMemoryTimer -= Time.deltaTime;
        }
        if(enemyMemoryTimer <= 0)
        {
            enemyMemoryTimer = 0;
        }

        if(Vector3.Distance(transform.position, _player.transform.position) <= _reactionRadius)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, _player.transform.position - transform.position, out hit, 50f, layerMask))
            {
                if(hit.transform.tag == "Player")
                {
                    enemyMemoryTimer = enemyData.EnemyMemoryTimer;
                    agent.isStopped = false;
                    agent.SetDestination(_player.transform.position);
                    Attack();

                }
                else if(enemyMemoryTimer > 0)
                {
                    agent.SetDestination(_player.transform.position);
                }
                else
                {
                    agent.isStopped = true;
                }

            }
        
        }
         
    }

}
