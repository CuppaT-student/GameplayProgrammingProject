using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointActivator : MonoBehaviour
{
    public CheckpointCollisionChecker[] CheckpointRespawners;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (CheckpointCollisionChecker collider in CheckpointRespawners)
            {
                collider.ActiveCheckpoint = this.transform;
            }
        }
    }
}
