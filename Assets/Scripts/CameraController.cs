using UnityEngine;

public class CameraController : MonoBehaviour
{
    public string carObjectName = "Car";

    private void FixedUpdate()
    {
        GameObject carObject = GameObject.Find(carObjectName);
        if (carObject != null)
        {
            Vector3 newPosition = carObject.transform.position;
            newPosition.z = -10;
            transform.position = newPosition;
        }
    }
}
