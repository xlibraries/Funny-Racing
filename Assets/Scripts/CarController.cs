using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [HideInInspector]
    public SuspensionManager suspensionManager;
    [HideInInspector]
    public EngineManager engineManager;

    public float speed;
    public float rotationSpeed = 15;
    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;
    public Rigidbody2D rb;


    private float movement = 0f;
    private float rotation = 0f;
    private GameManager gameManager;


    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogError("Gyroscope not supported on this device.");
        }
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    void Update()
    {
        //movement = -Input.GetAxisRaw("Vertical") * speed; //this is used while playing on computer
        // Check if the screen is touched
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is on the left quarter of the screen
            if (touch.position.x < Screen.width * 0.25f)
            {
                movement = speed; // Move backward
                AudioManager.Instance.SetCarMoving(true);
                
            }
            // Check if the touch is on the right quarter of the screen
            else if (touch.position.x > Screen.width * 0.75f)
            {
                movement = -speed; // Move forward
                AudioManager.Instance.SetCarMoving(true);
            }
            else
            {
                movement = 0f; // No movement
                AudioManager.Instance.SetCarMoving(false);
            }
        }  
        else
        {
            movement = 0f; // No movement
            AudioManager.Instance.SetCarMoving(false);
        }
        //rotation = Input.GetAxisRaw("Horizontal"); // this be used if paying on computer
        rotation = Input.gyro.rotationRate.y; // Use gyroscope input for rotation
        gameManager.DistanceCovered();
        //FuelManagement();
    }

    void FixedUpdate()
    {
        if (movement == 0 ||GameManager.fuelPresent <= 0)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
        }
        else
        {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
           gameManager.FuelManagement();
            //UpgradeSuspension();
            JointMotor2D motor = new JointMotor2D { motorSpeed = movement, maxMotorTorque = backWheel.motor.maxMotorTorque };
            backWheel.motor = motor;
            frontWheel.motor = motor;
        }
        if (GameManager.fuelPresent <= 0)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
            CurrencyManager.Instance.AddBaseCurrency(GameManager.distanceCovered); //will add base currency as per every 100m distance covered rule
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        rb.AddTorque(-rotation * rotationSpeed * Time.fixedDeltaTime);
    }
}
