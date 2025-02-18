using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowPath : AIBase
{
    [SerializeField] private Transform[] wayPoints;
    private int _currentPathIndex = 0;

    protected override void Start()
    {
        base.Start();

        GoToNextPoint();
    }

    private void Update()
    {
        if (cmpAgent.remainingDistance < breakingDistance && !cmpAgent.pathPending)
        {
            GoToNextPoint();
        }
    }

    private void GoToNextPoint()
    {
        if(_currentPathIndex >= wayPoints.Length) _currentPathIndex = 0;
        cmpAgent.SetDestination(wayPoints[_currentPathIndex++].position);
    }
}
