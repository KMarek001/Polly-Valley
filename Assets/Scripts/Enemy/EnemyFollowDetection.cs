using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowDetection : MonoBehaviour
{
    public bool isFollowDetected = false;

    public NavMeshAgent detectedAgent;

    private void OnTriggerStay(Collider other)
    {
        Physics.IgnoreLayerCollision(2, 3);
        if (other.transform.gameObject.layer == 6)
        {
            isFollowDetected = true;
            detectedAgent = other.gameObject.GetComponent<NavMeshAgent>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.layer == 6)
        {
            isFollowDetected = false;
        }
    }
}
