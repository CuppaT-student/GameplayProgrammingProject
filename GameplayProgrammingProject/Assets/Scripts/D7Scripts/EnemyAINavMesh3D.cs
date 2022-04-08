using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAINavMesh3D : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Arena & Player References")]
    [SerializeField] private GameObject playerReference;
    [SerializeField] private GameObject _arena;
    [SerializeField] private ZoneDetection detectionZones;

    [Header("Detection Zone Variables")]
    [SerializeField] private bool autoDetectZones = true;
    [SerializeField] private bool usingDetectionZones = false;
    [SerializeField] private bool detectedPlayerInZone = false;

    [Header("Speeds & Attack Variables")]
    public float patrolSpeed = 1.0F;
    public float aggroSpeed = 2.0F;
    [Range(0.0F, 1.0F)] public float aggroRotationSpeed = 1.0F;
    public float jumpForce = 10000.0F;
    public float attackChargeTime = 2f;
    public float attackRange = 1.0F;
    public float attackForce = 2000f;
    public float hitRecoilForce = 500f;
    public float attackCooldown = 2.0F;
    public bool canAttack = false;
    public bool isAttacking = false;
    public bool hasAttacked = false;
    public bool hitPlayer = false;

    public float health = 100.0F;
    public bool canTakeDMG = true;
    public bool spawnMoreOnDeath = true;
    public int spawnAmount = 2;
    public GameObject spawnObject;

    [SerializeField] public Transform currentDestination;

    private Quaternion lookRotation;
    private Vector3 targetDirection;
    private bool isLookingAtDestination = false;
    private float distanceToTarget;

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

    // Line Of Sight/Player Targetting (started but not finished)
    /*[Header("Line Of Sight")]
    public float maxRadius;
    [Range(0, 360)] public float maxAngle;
    public float updateDelay = 0.5f;

    [Header("Layer Masks")]
    public LayerMask targetMask;
    public LayerMask LayerMaskobstructionMask;
*/





    [Header("Material Settings")]
    [SerializeField] public Material material;
    [SerializeField] public Color defaultMaterialColor;
    [SerializeField] public Color defaultMaterialEmissiveColor;
    [SerializeField] public Color damageMaterialColor;
    [SerializeField] public Color damageMaterialEmissiveColor;
    public float dmgIndicatorTimer = 2.0F;


    private void OnTriggerEnter(Collider other)
    {
        if (hasAttacked && !hitPlayer)
        {
            if (other.tag == "Player")
            {

                Debug.Log("----Enemy cube hit Player!!!----");
                Vector3 v3RecoilForce = hitRecoilForce * transform.forward;
                rb.AddForce(-v3RecoilForce, ForceMode.Impulse);
                TookDamage();
                hitPlayer = true;
                // do damage
                // move back
            }
        }

    }


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        material = GetComponent<Renderer>().material;
        defaultMaterialColor = material.GetColor("_Color");
        defaultMaterialEmissiveColor = material.GetColor("_EmissionColor");




        if (playerReference == null)
        {
            playerReference = GameObject.FindGameObjectWithTag("Player");
        }
        if (!_arena)
        {
            Debug.Log("----Enemy has no Assigned Arena!!!----");
            _arena = GameObject.FindGameObjectWithTag("Arena");


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

            int index = 0;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PatrolPoint"))
            {
                patrolPointsTransforms[index] = obj.transform;
                index++;
            }

            UpdatePatrolDestination();
            _agent.speed = patrolSpeed;

        }
    }
    private void Start()
    {
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
                HuntTarget();
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



    private void HuntTarget()
    {
        if (LookAtTarget())
        {
            if (canAttack)
            {
                if (TargetInAttackRange())
                {
                    isAttacking = true;
                    Debug.Log("----Enemy Cube Preparing to Attacking Target!!!----");
                    _agent.enabled = false;
                    rb.AddForce(0, jumpForce * Time.fixedDeltaTime, 0, ForceMode.Impulse);
                    Vector3 v3Force = attackForce * transform.forward;
                    StartCoroutine(JumpWaitTimer(attackChargeTime));
                }
            }
            if (hasAttacked)
            {
                StartCoroutine(AttackCooldownTimer(attackCooldown));
            }
            _agent.SetDestination(currentDestination.position);
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
            canAttack = true;
            distanceToTarget = Vector3.Distance(currentDestination.transform.position, transform.position);
            return true;
        }
        return false;
    }

    private bool TargetInAttackRange()
    {
        if (distanceToTarget < attackRange)
        {
            return true;
        }
        return false;
    }

    IEnumerator AttackCooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        hasAttacked = false;
        canAttack = true;
    }

    IEnumerator JumpWaitTimer(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("----Enemy Cube Attacking Target!!!----");
        hitPlayer = false;
        Vector3 v3Force = attackForce * transform.forward;
        rb.AddForce(v3Force, ForceMode.Impulse);

        Debug.Log("----Enemy Cube Has Attacked!!!----");
        canAttack = false;
        _agent.enabled = true;
        hasAttacked = true;
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

    public void TakeDamage(float dmg)
    {
        if (canTakeDMG)
        {
            health -= dmg;
            Vector3 v3RecoilForce = hitRecoilForce * transform.forward;
            rb.AddForce(-v3RecoilForce, ForceMode.Impulse);
            TookDamage();
            canTakeDMG = false;
        }
    }

    public void TookDamage()
    {

        material.SetColor("_Color", damageMaterialColor);
        material.SetColor("_EmissionColor", damageMaterialEmissiveColor);

        StartCoroutine(ResetMaterialColour(dmgIndicatorTimer));

    }

    IEnumerator ResetMaterialColour(float time)
    {
        yield return new WaitForSeconds(time);

        material.SetColor("_Color", defaultMaterialColor);
        material.SetColor("_EmissionColor", defaultMaterialEmissiveColor);

        if (health < 0)
        {
            if (spawnObject != null)
            {
                if (spawnMoreOnDeath)
                {
                    for (int i = 0; i < spawnAmount; i++)
                    {
                        Instantiate(spawnObject, transform.position, transform.rotation);
                    }
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        canTakeDMG = true;
    }


    /*private void LineOfSightCheck()
    {
        Collider[] overlaps = Physics.OverlapSphere(transform.position, maxRadius, targetMask);

        if (overlaps.Length != 0)
        {

        }

    }*/
}
