using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum EnemyStates
{
    Wandering,
    FollowingTarget,
    FollowingPath
}

[Serializable, RequireComponent(typeof(SphereCollider))]
public class EnemySimpleBehaviour : MonoBehaviour
{
    
    
    [Header("Current state")] 
    [SerializeField] private EnemyStates state;
     
    [SerializeField] private NavMeshAgent cmpAgent;
    [field:SerializeField] public Transform target { get; private set; }
    [SerializeField] private SphereCollider detectionCollider;

    [SerializeField] private float detectionDistance;
    

    [SerializeField] private AIBase[] aiStates;
    

    private void Start()
    {
        cmpAgent = GetComponent<NavMeshAgent>();
        aiStates = GetComponents<AIBase>();
        detectionCollider = GetComponent<SphereCollider>();
        detectionCollider.radius = detectionDistance;
    }

    private void Update()
    {
        if (state == EnemyStates.FollowingPath)
        {
            UpdateFollowPath();
        }
        else if(state == EnemyStates.Wandering)
        {
            UpdateWandering();
        }
        else if (state == EnemyStates.FollowingTarget)
        {
            UpdateFollowTarget();
        }
    }

    private void UpdateFollowPath()
    {
        if (!PlayerIsOnRange(detectionDistance)) return;
        
        ChangeState(EnemyStates.FollowingTarget);
    }
    
    private void UpdateWandering()
    {
        if (!PlayerIsOnRange(detectionDistance)) return;
        
        ChangeState(EnemyStates.FollowingTarget);
    }
    
    private void UpdateFollowTarget()
    {
        if (PlayerIsOnRange(detectionDistance)) return;
        
        var dice = Random.Range(0, 100);
        ChangeState(dice >= 50 ? EnemyStates.FollowingPath : EnemyStates.Wandering);
    }

    private void ChangeState(EnemyStates newState)
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
} 
