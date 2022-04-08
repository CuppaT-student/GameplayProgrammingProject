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
    public float attackRange = 1.0F;
    public float attackForce = 1000f;
    public bool canAttack = false;
    public bool isAttacking = false;

    [Range(0.0F, 1.0F)] public float patrolRotationSpeed = 1.0F;
    [Range(0.0F, 1.0F)] public float aggroRotationSpeed = 1.0F;
    [SerializeField] public Transform currentDestination;

    private Quaternion lookRotation;
    private Vector3 targetDirection;
    private bool isLookingAtDestination = false;
    private float distanceToTarget;

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

    private void FixedUpdate()
    {

        if (usingDetectionZones)
        {
            CheckDetectionZones();
        }
        if (detectedPlayerInZone)
        {

            currentDestination = playerReference.transform;
            if (!isAttacking)
            {
                MoveAgent();
            }
        }
        else if (patrolling)
        {
            if (_agent.remainingDistance < requiredDistanceToPatrolPoint)
            {
                IteratePatrolIndex();
                UpdatePatrolDestination();
            }
        }

    }


    private bool LookAtTarget()
    {

        targetDirection = (playerReference.transform.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * aggroRotationSpeed);
        float dotProduct = Vector3.Dot(targetDirection, transform.forward);
        if (dotProduct > 0.9)
        {
            distanceToTarget = Vector3.Distance(currentDestination.transform.position, transform.position);
            return true;
        }
        return false;
    }

    private bool CheckAttackRange()
    {
        if (distanceToTarget < attackRange)
        {
            //_agent.isStopped = true;
            return true;
        }
        return false;
    }
    private void MoveAgent()
    {
        if (LookAtTarget())
        {
            if (canAttack)
            {
                if (CheckAttackRange())
                {
                    isAttacking = true;
                    Debug.Log("----Attacking Target!!!----");
                    _agent.enabled = false;
                    Vector3 normalisedDirection = Vector3.Normalize(currentDestination.position);
                    Vector3 v3Force = attackForce * transform.forward;
                    rb.AddForce(v3Force.x, v3Force.y, v3Force.z, ForceMode.Impulse);
                    //                    rb.AddForce(normalisedDirection.x * 30, 500.0F, normalisedDirection.z * 30, ForceMode.Impulse);

                    canAttack = false;
                }
            }

            if (IsGrounded())
            {
                _agent.enabled = true;
                StartCoroutine(AttackCooldownTimer(5.0F));

            }
            //_agent.isStopped = false;
            _agent.SetDestination(currentDestination.position);

        }
    }

    private bool IsGrounded()
    {
        // define a new ray at with -
        // origin = slightly above the characters feet to ensure we cast above whatever surface the player is on
        // direction = down (4Head)
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25F, Vector3.down);

        Debug.DrawLine(ray.origin, ray.origin + Vector3.down * 3f, Color.red, 2f);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            Debug.Log("Enemy Cube IS GROUNDED!");
            return true;
        }
        else
        {
            Debug.Log("Enemy Cube NOT GROUNDED!");
            return false;
        }
    }

    IEnumerator AttackCooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Attack Cooldown, Now checking if Grounded!");
        canAttack = true;
        isAttacking = false;
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
