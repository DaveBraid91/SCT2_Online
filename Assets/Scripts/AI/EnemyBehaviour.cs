using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum EnemyState
{
    Wandering,
    FollowingTarget,
    FollowingPath,
    Attacking
}

[Serializable, RequireComponent(typeof(Animator), typeof(SphereCollider))]
public class EnemyBehaviour : MonoBehaviour
{
    private readonly int strongAttack = Animator.StringToHash("strongAttack");
    private readonly int attack = Animator.StringToHash("attack");
    
    [Header("Current state")] 
    [SerializeField] private EnemyState state;
     
    [SerializeField] private NavMeshAgent cmpAgent;
    [field:SerializeField] public Transform target { get; private set; }
    [SerializeField] private SphereCollider detectionCollider;

    [SerializeField] private float detectionDistance;
    [SerializeField] private float attackDistance;

    [SerializeField] private float timeBetweenAttacks = 2.5f;
    [SerializeField] private float strongAttackChance = 0.3f;

    private Animator cmpAnimator;

    private float timeForNextAttack;

    [SerializeField] private AIBase[] aiStates;
    

    private void Start()
    {
        cmpAnimator = GetComponent<Animator>();
        cmpAgent = GetComponent<NavMeshAgent>();
        aiStates = GetComponents<AIBase>();
        detectionCollider = GetComponent<SphereCollider>();
        detectionCollider.radius = detectionDistance;
    }

    private void Update()
    {
        if (state == EnemyState.FollowingPath)
        {
            UpdateFollowPath();
        }
        else if(state == EnemyState.Wandering)
        {
            UpdateWandering();
        }
        else if (state == EnemyState.FollowingTarget)
        {
            UpdateFollowTarget();
        }
        else if (state == EnemyState.Attacking)
        {
            UpdateAttacking();
        }
    }

    private void UpdateFollowPath()
    {
        if (!PlayerIsOnRange(detectionDistance)) return;
        
        ChangeState(EnemyState.FollowingTarget);
    }
    
    private void UpdateWandering()
    {
        if (!PlayerIsOnRange(detectionDistance)) return;
        
        ChangeState(EnemyState.FollowingTarget);
    }
    
    private void UpdateFollowTarget()
    {
        if (PlayerIsOnRange(detectionDistance) && !PlayerIsOnRange(attackDistance)) return;

        if (PlayerIsOnRange(attackDistance))
        {
            ChangeState(EnemyState.Attacking);
        }
        else
        {
            ChangeState(EnemyState.FollowingPath);
        }
        
    }
    
    private void UpdateAttacking()
    {
        if (!PlayerIsOnRange(attackDistance))
        {
            ChangeState(EnemyState.FollowingTarget);
        }
        else
        {
            transform.LookAt(target.position);
            timeForNextAttack -= Time.deltaTime;
            if (timeForNextAttack <= 0)
            {
                if (Random.Range(0.0f, 1.0f) < strongAttackChance)
                {
                    cmpAnimator.SetTrigger(strongAttack);
                }
                else
                {
                    cmpAnimator.SetTrigger(attack);
                }

                timeForNextAttack = timeBetweenAttacks;
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        state = newState;
        
        for (int i = 0; i < aiStates.Length; i++)
        {
            aiStates[i].enabled = i == (int)state;
        }
    }

    private bool PlayerIsOnRange(float range)
    {
        var sqrDistance = (target.position - transform.position).sqrMagnitude;
        return sqrDistance <= Mathf.Pow(range, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ITargeteable>() == null) return;
        
        target = other.transform;
    }

    private void HitPlayer(float damage)
    {
        target.GetComponent<IDamagable>()?.ApplyDamage(damage);
    }
} 
