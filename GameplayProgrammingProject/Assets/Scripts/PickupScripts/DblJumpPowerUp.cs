using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DblJumpPowerUp : MonoBehaviour
{

    public GameObject particleEffect;
//    public float buffLengthSecs = 2.0F;
//    public bool buffActive = false;

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
        player.GetComponent<MyCharacterController>().canDoubleJump = true;

        //destroy this object
        Destroy(gameObject);
    }


    private void Update()
    {

    }

}
