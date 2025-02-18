using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AIBase : MonoBehaviour
{
    protected EnemySimpleBehaviour m_enemyBehaviour;
    protected NavMeshAgent cmpAgent;
    [SerializeField] protected float breakingDistance = 0.1f;
    
    protected virtual void Start()
    {
        m_enemyBehaviour = GetComponent<EnemySimpleBehaviour>();
        cmpAgent = GetComponent<NavMeshAgent>();
        cmpAgent.stoppingDistance = breakingDistance;
    }
}
