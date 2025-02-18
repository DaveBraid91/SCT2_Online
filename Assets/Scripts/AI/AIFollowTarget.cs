using System;
using UnityEngine;

public class AIFollowTarget : AIBase
{
    private void Update()
    {
        cmpAgent.SetDestination(m_enemyBehaviour.target.position);
    }
}
