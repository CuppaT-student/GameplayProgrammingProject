using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Cinemachine;

public class SplineFollowSwitch : MonoBehaviour
{

    public PathCreator spline;
    public SplineFollower follower;
    public bool isExit;
    public CinemachineVirtualCamera splineCamera;
    public CinemachineVirtualCamera optionalSplineCamera;


    private void OnTriggerExit(Collider other)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            follower = other.GetComponent<SplineFollower>();
            if (!isExit)
            {
                /*if(follower.splineSceneActive)
                {
                    //get prev spline camera and reset its priority
                }*/
                follower.distanceTravelled = 0;
                splineCamera.Priority = 11;
                follower.currentPath = spline;
                follower.splineSceneActive = true;
            }
            else
            {
                splineCamera.Priority = 9;
                if (optionalSplineCamera != null)
                {
                    optionalSplineCamera.Priority = 9;
                }
                follower.currentPath = null;
                follower.splineSceneActive = false;
            }
        }
    }


}
