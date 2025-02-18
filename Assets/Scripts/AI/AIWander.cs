using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIWander : AIBase
{
    [SerializeField] private float wanderRadius;
    private Vector3 initialPosition;
    private Vector3 randomEndPoint;

    protected override void Start()
    {
        base.Start();

        initialPosition = transform.position;
        SetNewRandomPoint();
    }

    private void Update()
    {
        if (!cmpAgent.enabled) return;

        if (cmpAgent.remainingDistance < breakingDistance && !cmpAgent.pathPending)
        {
            SetNewRandomPoint();
        }
    }

    private void SetNewRandomPoint()
    {
        randomEndPoint = initialPosition + Random.insideUnitSphere * wanderRadius;

        randomEndPoint.y = 0;

        cmpAgent.SetDestination(randomEndPoint);
    }

    private void OnDrawGizmos()
    {
        if (cmpAgent != null)
        {
            Gizmos.DrawWireSphere(randomEndPoint, 0.5f);
            Gizmos.DrawWireSphere(cmpAgent.destination, 0.5f);
        }
    }
}
