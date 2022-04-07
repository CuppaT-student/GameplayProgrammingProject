using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAINavMesh3D : MonoBehaviour
{

    [SerializeField] public GameObject _arena;
    [SerializeField] private ZoneDetection detectionZones;
    public bool autoDetectZones = true;
    public bool usingDetectionZones = false;
    // Movement Specific
    [SerializeField] public Transform destination;


    // Patrol Specific
    [Header("Patrol")]
    [SerializeField] public bool patrol = true;
    public enum PatrolStyle { PatrolInOrder, PatrolRandomly, PatrolNearestPoint }
    [SerializeField] public PatrolStyle _patrolStyle = PatrolStyle.PatrolInOrder;

    [SerializeField] public Transform[] patrolTransforms;
    public int patrolIndex = 0;
    public int prevIndex = 0;
    public float requiredDistanceToPatrolPoint = 1.0F;
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
        if (!_arena)
        {
            Debug.Log("----Enemy has no Assigned Arena!!!----");
        }
        else
        {
            if (!_arena.GetComponent<ZoneDetection>())
            {
                Debug.Log("----Arena has no Assigned Detection Zones!!!----");
            }
            else
            {
                detectionZones = _arena.GetComponent<ZoneDetection>();

                if (autoDetectZones)
                {
                    usingDetectionZones = true;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        //_agent.destination = destination.position;
        if (usingDetectionZones)
        {
            CheckDetectionZones();
        }

        if (patrol)
        {
            if (_agent.remainingDistance < requiredDistanceToPatrolPoint)
            //if (Vector3.Distance(transform.position, destination.position) < 1)
            {
                IteratePatrolIndex();
                UpdatePatrolDestination();
            }
        }
    }

    private void CheckDetectionZones()
    {
        if (detectionZones.playerInDetectionZone)
        {
            patrol = false;
            foundTarget = true;
            destination = playerTarget.transform;
            _agent.SetDestination(destination.position);

        }
        else
        {
            foundTarget = false;
            patrol = true;
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
        //transform.rotation = Quaternion.LookRotation(destination.transform.position);
        _agent.SetDestination(destination.position);
    }
    void IteratePatrolIndex()
    {
        previousPatrolPoint = patrolTransforms[patrolIndex];
        switch (_patrolStyle)
        {
            case PatrolStyle.PatrolInOrder:
                // Patrols In Order of the added Patrol Points
                prevIndex = patrolIndex;
                patrolIndex++;
                if (patrolIndex == patrolTransforms.Length)
                {
                    patrolIndex = 0;
                }
                break;
            case PatrolStyle.PatrolRandomly:
                // Randomly Chooses from the added Patrol Points
                // ensures if transform missing or if random value is same as current then re-does random generator

                prevIndex = patrolIndex;
                while (true)
                {
                    patrolIndex = Random.Range(0, patrolTransforms.Length);
                    if (patrolTransforms[patrolIndex] == null || patrolIndex == prevIndex) { continue; }
                    else { break; }
                }
                break;

            case PatrolStyle.PatrolNearestPoint:
                // Patrols between the two-three closest points when activated
                float closestPoint = Mathf.Infinity;
                int tempShortest = 0;
                for (int i = 0; i < patrolTransforms.Length; i++)
                {
                    // if tansform missing, or index is the same as current or previous patrol points then continue loop
                    if (patrolTransforms[i] == null || i == patrolIndex || i == prevIndex) { continue; }
                    // else check if its the closest and if so assign it to tempory variable
                    else
                    {
                        float distance = Vector3.Distance(patrolTransforms[i].position, transform.position);

                        if (distance < closestPoint)
                        {
                            closestPoint = distance;

                            tempShortest = i;

                        }

                    }
                }
                // after loop has finished and closest point has been located, save the current index as previous and assign temp to current
                prevIndex = patrolIndex;
                patrolIndex = tempShortest;
                break;
        }
    }
}
