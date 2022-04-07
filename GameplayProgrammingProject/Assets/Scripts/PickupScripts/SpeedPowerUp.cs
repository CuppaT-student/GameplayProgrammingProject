using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{

    public GameObject particleEffect;
    [SerializeField] public float buffLengthSecs = 3.0F;
    [SerializeField] public float buffAmount = 6.0F;

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    private void Pickup(Collider player)
    {
        Debug.Log("Power Up Picked Up!!");
        //spawn particles
        if (particleEffect != null)
        {
            Instantiate(particleEffect, transform.position, transform.rotation);
        }

        //apply effect to player

        //Rigidbody rb = player.GetComponent<Rigidbody>();
        //Vector3 forceDirection = (300, 0, 0)
        //rb.AddForce(rb.transform.forward, ForceMode.Impulse);
        player.GetComponent<MyCharacterController>().increaseMaxSpeed(buffAmount, buffLengthSecs);
        //buffActive = true;

        //destroy this object
        Destroy(gameObject);
    }


    private void Update()
    {
    }


}
