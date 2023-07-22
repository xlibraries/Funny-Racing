using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject carPrefab;
    public TextMeshProUGUI prefabUpgrades;

    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = carPrefab.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        WheelJoint2D wheelJoint = rb2D.GetComponent<WheelJoint2D>();
        JointMotor2D motor = wheelJoint.motor;

        // Get the current motor speed based on the active instance in the scene or fallback to the prefab's motor speed
        float currentMotorSpeed = (wheelJoint.connectedBody != null) ? motor.motorSpeed : motor.motorSpeed;

        prefabUpgrades.text = "Suspension: Damping Ratio: " + wheelJoint.suspension.dampingRatio + " Frequency: " + wheelJoint.suspension.frequency + "\n" +
                              "Engine: Torque: " + motor.maxMotorTorque + " Speed: " + currentMotorSpeed + "\n" +
                              "Chassis: Mass: " + rb2D.mass + "\n" +
                              "Currency: " + CurrencyManager.Instance.GetTotalCurrency().ToString();

        //Debug.LogWarning(prefabUpgrades.text);
    }

}
