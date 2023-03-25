using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*speed = distance / time;
      distance = fuel present * milage;
    */
    public Transform startpoint;
    public static bool isGrounded;

    //Parameters for fuel management system
    private const float fuelCapacity = 10.0f;
    public static float fuelPresent;
    private const float BurnRate = 1.0f;


    //Paraments for distance calculation
    public static float distanceCovered;
    private float distX;
    private float distY;
    private float distZ;
    private float startX;
    private float startY;
    private float startZ;

    // Start is called before the first frame update
    void Start()
    {
        fuelPresent = fuelCapacity;
        isGrounded = false;
        Debug.Log("Fuel present on start: " + fuelPresent);
        startX = startpoint.position.x;
        startY = startpoint.position.y;
        startZ = startpoint.position.z;
    }

    public void DistanceCovered()
    {
        distX = this.transform.position.x;
        distY = this.transform.position.y;
        distZ = this.transform.position.z;

        distanceCovered = Mathf.Sqrt(
            Mathf.Pow(distX - startX, 2) +
            Mathf.Pow(distY - startY, 2) +
            Mathf.Pow(distZ - startZ, 2)
            );
        //Debug.Log("Distance Covered: " + distanceCovered);
    }

    public void FuelManagement()
    {
        fuelPresent -= BurnRate * Time.deltaTime;
        Debug.Log("Fuel Present: " + fuelPresent);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            isGrounded = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collider.CompareTag("Fuel"))
        {
            fuelPresent = fuelCapacity;
            Debug.Log("Fuel Refilled " + fuelPresent);
        }
    }
}