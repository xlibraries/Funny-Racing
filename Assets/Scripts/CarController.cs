using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : GameManager
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
            }
            // Check if the touch is on the right quarter of the screen
            else if (touch.position.x > Screen.width * 0.75f)
            {
                movement = -speed; // Move forward
            }
            else
            {
                movement = 0f; // No movement
            }
        }
        else
        {
            movement = 0f; // No movement
        }
        //rotation = Input.GetAxisRaw("Horizontal"); // this be used if paying on computer
        rotation = Input.gyro.rotationRate.y; // Use gyroscope input for rotation
        DistanceCovered();
        //FuelManagement();
    }

    void FixedUpdate()
    {
        if (movement == 0 || fuelPresent <= 0)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
        }
        else
        {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            FuelManagement();
            //UpgradeSuspension();
            JointMotor2D motor = new JointMotor2D { motorSpeed = movement, maxMotorTorque = backWheel.motor.maxMotorTorque };
            backWheel.motor = motor;
            frontWheel.motor = motor;
        }
        if (fuelPresent <= 0)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        rb.AddTorque(-rotation * rotationSpeed * Time.fixedDeltaTime);
    }
}
