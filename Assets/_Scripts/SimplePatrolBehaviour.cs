using UnityEngine;
using UnityEngine.AI;

public class SimplePatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    private NavMeshAgent _cmpAgent;
    private int _currentPathIndex = 0;

    protected void Start()
    {
        _cmpAgent = GetComponent<NavMeshAgent>();
        GoToNextPoint();
    }

    private void Update()
    {
        if (_cmpAgent.remainingDistance < _cmpAgent.stoppingDistance && !_cmpAgent.pathPending)
        {
            GoToNextPoint();
        }
    }

    private void GoToNextPoint()
    {
        if(_currentPathIndex >= wayPoints.Length) _currentPathIndex = 0;
        _cmpAgent.SetDestination(wayPoints[_currentPathIndex++].position);
    }
}
