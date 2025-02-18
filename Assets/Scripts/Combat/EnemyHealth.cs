using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : BaseHealth
{
    NavMeshAgent cmpAgent;

    protected override void Awake()
    {
        base.Awake();
        cmpAgent = GetComponent<NavMeshAgent>();
    }


    protected override void Die()
    {
        cmpAgent.enabled = false;
        cmpAnimator.SetTrigger("die");
        Destroy(this.gameObject, 10);
    }
}
