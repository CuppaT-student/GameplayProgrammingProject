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
                splineCamera.Priority = 11;
                follower.currentPath = spline;
                follower.splineSceneActive = true;
            }
            else
            {
                splineCamera.Priority = 9;
                follower.currentPath = null;
                follower.splineSceneActive = false;
            }
        }
    }


}
