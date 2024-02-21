using UnityEngine;

public class MapPointsController : MonoBehaviour
{
    [SerializeField]
    private Transform referenceObject;

    void Update()
    {
        this.transform.position = new Vector3(referenceObject.position.x, 200f, referenceObject.position.z);
    }
}
