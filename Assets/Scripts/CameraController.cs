using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private void FixedUpdate()
    {
        Vector3 newPosition = target.position; ;
        newPosition.z = -10;
        transform.position = newPosition;
    }
}
