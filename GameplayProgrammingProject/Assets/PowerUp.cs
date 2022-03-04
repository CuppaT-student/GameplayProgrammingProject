using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public GameObject particleEffect;

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        Debug.Log("Power Up Picked Up!!");
        //spawn particles
        Instantiate(particleEffect, transform.position, transform.rotation);

        //apply effect to player

        //destroy this object
        Destroy(gameObject);
    }
}
