using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollisionChecker : MonoBehaviour
{

    public Transform Checkpoint;
    public Transform Checkpoint2;
    public Transform ActiveCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = ActiveCheckpoint.position;
        }
    }

}
