using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : BaseHealth
{
    private static readonly int Die1 = Animator.StringToHash("die");
    private NavMeshAgent _cmpAgent;

    protected override void Awake()
    {
        base.Awake();
        _cmpAgent = GetComponent<NavMeshAgent>();
    }


    protected override void Die()
    {
        _cmpAgent.enabled = false;
        cmpAnimator.SetTrigger(Die1);
        Destroy(this.gameObject, 10);
    }
}
