using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AINavMesh : MonoBehaviour
{
    [SerializeField] public bool patrol = true;

    [SerializeField] public Transform destination;
    [SerializeField] public Transform[] patrolTransforms;
    public int patrolIndex = 0;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        UpdatePatrolDestination();
    }
    // Update is called once per frame
    void Update()
    {
        //_agent.destination = destination.position;


        if (patrol)
        {
            if (Vector3.Distance(transform.position, destination.position) < 1)
            {
                IteratePatrolIndex();
                UpdatePatrolDestination();
            }
        }
    }

    void UpdatePatrolDestination()
    {
        destination = patrolTransforms[patrolIndex];
        _agent.SetDestination(destination.position);
    }
    void IteratePatrolIndex()
    {
        patrolIndex++;
        if (patrolIndex == patrolTransforms.Length)
        {
            patrolIndex = 0;
        }
    }
}
