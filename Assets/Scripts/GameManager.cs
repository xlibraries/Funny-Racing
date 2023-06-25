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
    public GameObject carPrefab;

    //Parameters for fuel management system
    public const float fuelCapacity = 10.0f;
    public static float fuelPresent;
    private const float BurnRate = 1.0f;
    private GameObject instantiatedCar;


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
        instantiatedCar = Instantiate(carPrefab);
        instantiatedCar.name = "Car";
        CarController carController = instantiatedCar.GetComponent<CarController>();
        carController.SetGameManager(this);
        fuelPresent = fuelCapacity;
        //Debug.Log("Fuel present on start: " + fuelPresent);
        startX = startpoint.position.x;
        startY = startpoint.position.y;
        startZ = startpoint.position.z;
    }

    public void DistanceCovered()
    {
        distX = instantiatedCar.transform.position.x;
        distY = instantiatedCar.transform.position.y;
        distZ = instantiatedCar.transform.position.z;

        distanceCovered = Mathf.Sqrt(
            Mathf.Pow(distX - startX, 2) +
            Mathf.Pow(distY - startY, 2) +
            Mathf.Pow(distZ - startZ, 2)
            );
        //CurrencyManager.Instance.AddBaseCurrency(distanceCovered); //will add base currency as per every 100m distance covered rule
        //Debug.Log("Distance Covered: " + distanceCovered);
    }

    public void FuelManagement()
    {
        fuelPresent -= BurnRate * Time.deltaTime;
        //Debug.Log("Fuel Present: " + fuelPresent);
    }

    //public void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //        CurrencyManager.Instance.AddBaseCurrency(distanceCovered); //will add base currency as per every 100m distance covered rule
    //        Debug.Log("Currency: " + CurrencyManager.Instance.GetTotalCurrency());
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }

    //    if (collider.CompareTag("Fuel"))
    //    {
    //        fuelPresent = fuelCapacity;
    //        Debug.Log("Fuel Refilled " + fuelPresent);
    //    }
    //}
}