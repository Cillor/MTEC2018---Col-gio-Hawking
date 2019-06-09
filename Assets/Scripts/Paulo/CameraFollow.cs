using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    float xCoord, yCoord;
    public float xTop, xBot, yTop, yBot;

    public float smoothSpeed = 0.01f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private void LateUpdate()
    {
        if (target.position.x < xTop && target.position.x > xBot)
        {
            xCoord = target.position.x;
        }

        if (target.position.y < yTop && target.position.y > yBot)
        {
            yCoord = target.position.y;
        }


        Vector3 desiredPosition = new Vector3(xCoord, yCoord, this.transform.position.z);
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition + offset, ref velocity, smoothSpeed);
        this.transform.position = smoothedPosition;
    }
}
