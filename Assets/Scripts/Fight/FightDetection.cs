using UnityEngine;

public class FightDetection : MonoBehaviour
{
    public bool isFightDetected = false;
    public bool isOpeningChestAvailable = false;

    private void OnTriggerStay(Collider other)
    {
        Physics.IgnoreLayerCollision(6, 3);
        if (other.transform.gameObject.layer == 7)
        {
            isFightDetected = true;
        }
        else if(other.tag == "Chest")
        {
            isOpeningChestAvailable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.layer == 7)
        {
            isFightDetected = false;
        }
        else if (other.tag == "Chest")
        {
            isOpeningChestAvailable = false;
        }
    }
}
