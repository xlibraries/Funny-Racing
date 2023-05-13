using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : GameManager
{
    public float speed = 1500;
    public float rotationSpeed = 15;
    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;
    public Rigidbody2D rb;
    public SuspensionManager suspensionManager;

    private float movement = 0f;
    private float rotation = 0f;

    void Update()
    {
        movement = -Input.GetAxisRaw("Vertical") * speed;
        rotation = Input.GetAxisRaw("Horizontal");
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

    // Remove the UpgradeSuspension method

    // Call the UpgradeSuspension method from SuspensionManager
    public void UpgradeSuspension()
    {
        suspensionManager.UpgradeSuspension();
    }
}
