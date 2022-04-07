using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DoorButtonTrigger : MonoBehaviour
{
    private MyCharacterController inputsystem;
    public PlayableDirector playable;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player In Trigger Area!");
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inputsystem = other.GetComponent<MyCharacterController>();
            if(inputsystem.triggerHeld)
            {
                playable.Play();
                inputsystem.triggerHeld = false;
            }
        }

    }
}
