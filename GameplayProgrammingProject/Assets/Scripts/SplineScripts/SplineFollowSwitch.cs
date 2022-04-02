using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Cinemachine;

public class SplineFollowSwitch : MonoBehaviour
{

    public PathCreator spline;
    public SplineFollower follower;
    public CinemachineVirtualCamera splineCamera;
    public CinemachineVirtualCamera optionalSplineCamera;
    public bool isExit;
    public bool toggleEnabled = true;
    public float toggleTime = 3f;
    public bool isToggling = false;
    public bool isSwitched = false;
    public float startPosition = 1.5f;


    private void OnTriggerExit(Collider other)
    {
        if (toggleEnabled)
        {
            if (!isToggling)
            {
                StartCoroutine(ToggleSwitchType(toggleTime));
                isToggling = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            follower = other.GetComponent<SplineFollower>();
            if (follower.marioAutoRunStyle)
            {
                if (isSwitched)
                {
                    follower.marioSwitchedDirection = true;
                }
                else
                {
                    follower.marioSwitchedDirection = false;
                }
            }
            if (!isExit)
            {
                /*if(follower.splineSceneActive)
                {
                    //get prev spline camera and reset its priority
                }*/
                if (!follower.splineStarted)
                {
                    //follower.distanceTravelled = 0;
                    splineCamera.Priority = 11;
                    follower.currentPath = spline;
                    follower.distanceTravelled = startPosition;
                    follower.splineSceneActive = true;

                }
            }
            else
            {
                splineCamera.Priority = 9;
                if (optionalSplineCamera != null)
                {
                    optionalSplineCamera.Priority = 9;
                }
                follower.splineStarted = false;
                follower.currentPath = null;
                follower.splineSceneActive = false;

            }
        }
    }



    IEnumerator ToggleSwitchType(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Toggling Spline Switch Type!");
        isExit = !isExit;
        isSwitched = !isSwitched;
        isToggling = false;

    }

}
