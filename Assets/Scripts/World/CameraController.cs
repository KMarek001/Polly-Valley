using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineFollowZoom zoom;

    private float inputScrollValue;

    void Start()
    {
        zoom = GetComponent<CinemachineFollowZoom>();
        zoom.m_Width = 8;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            inputScrollValue = Input.GetAxis("Mouse ScrollWheel");
            zoom.m_Width -= (inputScrollValue * 10);
            if(zoom.m_Width < 0)
                zoom.m_Width = 0;
            else if(zoom.m_Width > 10)
                zoom.m_Width = 10;
        }
    }
}