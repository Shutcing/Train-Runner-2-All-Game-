using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        if (GameManager.CameraMove && Train.arrived)
        {
            target = GameObject.Find("Ryan").transform;
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, -1.6f, 1.6f);

            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}
