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

    private float movment = 0f;
    private float rotation = 0f;

    CarManager carManager;

    void Update()
    {
        movment = -Input.GetAxisRaw("Vertical") * speed;
        rotation = Input.GetAxisRaw("Horizontal");
        DistanceCovered();
        //FuelManagement();
    }

    void FixedUpdate()
    {
        if (movment == 0 || fuelPresent <= 0)
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
            JointMotor2D motor = new() { motorSpeed = movment, maxMotorTorque = backWheel.motor.maxMotorTorque };
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

    public void UpgradeSuspension()
    {
        if (carManager.isSuspension)
        {
            Debug.Log("Hum Jeet gaye");
        }
        float dampingRatio = frontWheel.suspension.dampingRatio;
        int frequency = (int)frontWheel.suspension.frequency;
        dampingRatio += 0.1f;
        frequency += 500;
        frontWheel.suspension.dampingRatio.Equals(dampingRatio);
        frontWheel.suspension.frequency.Equals(frequency);
        backWheel.suspension.dampingRatio.Equals(dampingRatio);
        backWheel.suspension.frequency.Equals(frequency);
        Debug.Log(backWheel.suspension.dampingRatio.Equals(dampingRatio));
        Debug.Log(backWheel.suspension.frequency.Equals(frequency));
    }
}