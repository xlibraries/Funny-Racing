using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 2000;
    public WheelJoint2D backWheel;

    private float movment = 0f;

    void Update()
    {
        movment = -Input.GetAxisRaw("Vertical") * speed;
    }

    void FixedUpdate()
    {
        if (movment == 0)
        {
            backWheel.useMotor = false;
        }
        else 
        {
            backWheel.useMotor = true;
        }

        JointMotor2D motor = new JointMotor2D { motorSpeed = movment, maxMotorTorque = backWheel.motor.maxMotorTorque };
        backWheel.motor = motor;
    }
}
