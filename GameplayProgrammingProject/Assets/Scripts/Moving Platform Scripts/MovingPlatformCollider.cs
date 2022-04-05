using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformCollider : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(this.transform);
            other.GetComponent<TestingInputSystem>().onMovingPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
            other.GetComponent<TestingInputSystem>().onMovingPlatform = false;

        }

    }
}


