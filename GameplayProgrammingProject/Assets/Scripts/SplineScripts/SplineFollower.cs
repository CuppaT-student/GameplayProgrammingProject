using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SplineFollower : MonoBehaviour
{
    public PathCreator currentPath;
    public PathCreator previousPath;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    public float distanceTravelled;
    public bool onSpline = false;
    public bool splineSceneActive = false;

    public bool splineStarted = false;

    public bool marioAutoRunStyle = true;
    public bool marioSwitchedDirection = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (currentPath != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            currentPath.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        if (currentPath != null)
        {
            if (marioAutoRunStyle)
            {
                onSpline = true;
                if (marioSwitchedDirection)
                {
                    distanceTravelled -= speed * Time.deltaTime;
                }
                else
                {
                    distanceTravelled += speed * Time.deltaTime;
                }

                transform.position = currentPath.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = currentPath.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }

            else
            {

                if (!splineStarted)
                {
                    transform.position = new Vector3(currentPath.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).x, rb.transform.position.y, currentPath.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).z);
                    splineStarted = true;
                    onSpline = true;
                }
                if (splineStarted && !onSpline)
                {

                    currentPath = previousPath;
                    transform.position = new Vector3(currentPath.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).x, rb.transform.position.y, currentPath.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).z);
                    transform.rotation = currentPath.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                    onSpline = true;

                }
                else
                {

                    transform.position = new Vector3(currentPath.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).x, rb.transform.position.y, currentPath.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).z);
                    transform.rotation = currentPath.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                }
            }
        }
        else
        {
            onSpline = false;
        }

        if (!splineSceneActive)
        {
            distanceTravelled = 0;
            currentPath = null;
            previousPath = null;
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = currentPath.path.GetClosestDistanceAlongPath(transform.position);
    }

}
