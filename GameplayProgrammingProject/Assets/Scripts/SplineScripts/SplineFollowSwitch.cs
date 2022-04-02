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
    public bool coroutine_running = false;
    public float startPosition = 1.5f;


    private void OnTriggerExit(Collider other)
    {
        if (!coroutine_running)
        {
            StartCoroutine(ToggleSwitchType(3));
            coroutine_running = true;
        }
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
        coroutine_running = false;
    }

}
