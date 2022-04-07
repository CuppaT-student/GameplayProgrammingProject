using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAINavMesh3D : MonoBehaviour
{

    [Header("Arena & Player References")]
    [SerializeField] private GameObject playerReference;
    [SerializeField] private GameObject _arena;
    [SerializeField] private ZoneDetection detectionZones;

    [Header("Detection Zone Variables")]
    [SerializeField] private bool autoDetectZones = true;
    [SerializeField] private bool usingDetectionZones = false;
    [SerializeField] private bool detectedPlayerInZone = false;

    [Header("Speeds & Current Destination Variables")]
    public float patrolSpeed = 1.0F;
    public float aggroSpeed = 2.0F;
    [SerializeField] public Transform currentDestination;

    // Line Of Sight/Player Targetting 
    [Header("Line Of Sight")]
    public float maxRadius;
    [Range(0, 360)] public float maxAngle;
    public float updateDelay = 0.5f;

    [Header("Layer Masks")]
    public LayerMask targetMask;
    public LayerMask LayerMaskobstructionMask;


    // Patrol Specific
    [Header("Patrol Settings")]
    [SerializeField] public bool patrolling = true;
    public enum PatrolStyle { PatrolInOrder, PatrolRandomly, PatrolNearestPoint }
    [SerializeField] public PatrolStyle _patrolStyle = PatrolStyle.PatrolInOrder;

    [SerializeField] public Transform[] patrolPointsTransforms;
    public int patrolIndex = 0;
    public int prevIndex = 0;
    public float requiredDistanceToPatrolPoint = 1.0F;
    private NavMeshAgent _agent;



    private Rigidbody rb;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        UpdatePatrolDestination();

        if (playerReference == null)
        {
            playerReference = GameObject.FindGameObjectWithTag("Player");
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
        _agent.speed = patrolSpeed;

    }


    // Update is called once per frame
    void Update()
    {

        if (usingDetectionZones)
        {
            CheckDetectionZones();
        }
        if (detectedPlayerInZone)
        {
            currentDestination = playerReference.transform;
            _agent.SetDestination(currentDestination.position);
        }
        else if (patrolling)
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
            patrolling = false;
            _agent.speed = aggroSpeed;
            detectedPlayerInZone = true;
        }
        else
        {
            detectedPlayerInZone = false;
            _agent.speed = patrolSpeed;
            patrolling = true;
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
        currentDestination = patrolPointsTransforms[patrolIndex];
        //transform.rotation = Quaternion.LookRotation(destination.transform.position);
        _agent.SetDestination(currentDestination.position);
    }
    void IteratePatrolIndex()
    {
        switch (_patrolStyle)
        {
            case PatrolStyle.PatrolInOrder:
                // Patrols In Order of the added Patrol Points
                prevIndex = patrolIndex;
                patrolIndex++;
                if (patrolIndex == patrolPointsTransforms.Length)
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
                    patrolIndex = Random.Range(0, patrolPointsTransforms.Length);
                    if (patrolPointsTransforms[patrolIndex] == null || patrolIndex == prevIndex) { continue; }
                    else { break; }
                }
                break;

            case PatrolStyle.PatrolNearestPoint:
                // Patrols between the two-three closest points when activated
                float closestPoint = Mathf.Infinity;
                int tempShortest = 0;
                for (int i = 0; i < patrolPointsTransforms.Length; i++)
                {
                    // if tansform missing, or index is the same as current or previous patrol points then continue loop
                    if (patrolPointsTransforms[i] == null || i == patrolIndex || i == prevIndex) { continue; }
                    // else check if its the closest and if so assign it to tempory variable
                    else
                    {
                        float distance = Vector3.Distance(patrolPointsTransforms[i].position, transform.position);

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
