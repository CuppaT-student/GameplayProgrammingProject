using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZoneDetection : MonoBehaviour
{

    public bool playerInDetectionZone = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInDetectionZone = true;
            Debug.Log("----Player has entered Detection Zones!!!----");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInDetectionZone = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInDetectionZone = false;

            Debug.Log("----Player has left Detection Zones!!!----");
        }
    }

}
