using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightDetection : MonoBehaviour
{
    public bool isFightDetected = false;

    private void OnTriggerStay(Collider other)
    {
        Physics.IgnoreLayerCollision(2, 3);
        if (other.transform.gameObject.layer == 6)
        {
            isFightDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.layer == 6)
        {
            isFightDetected = false;
        }
    }
}
