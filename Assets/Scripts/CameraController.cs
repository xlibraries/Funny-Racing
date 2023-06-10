using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject targetPrefab;

    private void Start()
    {
        targetPrefab = GameObject.Find("Car"); // Assuming "Car" is the name of the instantiated prefab
    }

    private void FixedUpdate()
    {
        if (targetPrefab != null)
        {
            Vector3 newPosition = targetPrefab.transform.position;
            newPosition.z = -10;
            transform.position = newPosition;
        }
    }
}
