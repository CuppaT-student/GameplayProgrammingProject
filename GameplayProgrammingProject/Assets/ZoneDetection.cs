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
            if (other.GetComponent<MyCharacterController>()._stance == MyCharacterController.CharacterStance.Standing)
            {
                Debug.Log("----Player has entered Detection Zones!!!----");
                playerInDetectionZone = true;
            }
            else
            {
                playerInDetectionZone = false;
                Debug.Log("----Player has entered Detection Zones but is Hidden!!!----");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<MyCharacterController>()._stance == MyCharacterController.CharacterStance.Standing)
            {
                playerInDetectionZone = true;
            }
            else
            {
                playerInDetectionZone = false;
            }
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
