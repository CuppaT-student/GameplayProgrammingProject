using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAINavMesh3D : MonoBehaviour
{
    // Movement Specific
    [SerializeField] public Transform destination;


    // Patrol Specific
    [Header("Patrol")]
    [SerializeField] public bool patrol = true;
    /*    [SerializeField] public bool randomPatrolOrder = true;
        [SerializeField] public bool gotoNearestPatrolPoint = true;*/
    public enum PatrolStyle { PatrolInOrder, PatrolRandomly, PatrolNearestPoint }
    [SerializeField] public PatrolStyle _patrolStyle = PatrolStyle.PatrolInOrder;

    [SerializeField] public Transform[] patrolTransforms;
    public int patrolIndex = 0;
    public float patrolTargetRange = 1.0F;
    private NavMeshAgent _agent;
    private Transform previousPatrolPoint;

    // Line Of Sight/Player Targetting 
    [Header("Line Of Sight")]
    public GameObject playerTarget;
    public bool foundTarget = false;
    public float maxRadius;
    [Range(0, 360)] public float maxAngle;
    public float updateDelay = 0.5f;

    [Header("Layer Masks")]
    public LayerMask targetMask;
    public LayerMask LayerMaskobstructionMask;

    private Rigidbody rb;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        UpdatePatrolDestination();

        if (playerTarget == null)
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player");
        }
    }
    // Update is called once per frame
    void Update()
    {
        //_agent.destination = destination.position;


        if (patrol)
        {
            if (_agent.remainingDistance < patrolTargetRange)
            //if (Vector3.Distance(transform.position, destination.position) < 1)
            {
                IteratePatrolIndex();
                UpdatePatrolDestination();
            }
        }
    }

    private void LineOfSightCheck()
    {
        Collider[] overlaps = Physics.OverlapSphere(transform.position, maxRadius, targetMask);

        if (overlaps.Length != 0)
        {

        }

    }

    void UpdatePatrolDestination()
    {
        destination = patrolTransforms[patrolIndex];
        _agent.SetDestination(destination.position);
    }
    void IteratePatrolIndex()
    {
        previousPatrolPoint = patrolTransforms[patrolIndex];
        switch (_patrolStyle)
        {
            case PatrolStyle.PatrolInOrder:
                // Patrols In Order of the added Patrol Points
                patrolIndex++;
                if (patrolIndex == patrolTransforms.Length)
                {
                    patrolIndex = 0;
                }
                break;
            case PatrolStyle.PatrolRandomly:
                // Randomly Chooses from the added Patrol Points
                patrolIndex = Random.Range(0, patrolTransforms.Length);
                break;
            case PatrolStyle.PatrolNearestPoint:
                // Patrols between the two closest points when activated
                float closestPoint = Mathf.Infinity;
                for (int i = 0; i < patrolTransforms.Length; i++)
                {
                    if (patrolTransforms[i] == previousPatrolPoint)
                    { continue; }
                    else
                    {
                        float distance = Vector3.Distance(patrolTransforms[i].position, transform.position);

                        if (distance < closestPoint)
                        {
                            closestPoint = distance;
                            patrolIndex = i;
                        }
                    }
                }
                break;
        }
    }
}
