using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public SuspensionManager suspensionManager;
    public EngineManager engineManager;
    public TyreManager tyreManager;
    public ChasisManager chasisManager;
    public CarController carController;

    public TextMeshProUGUI scriptUpgrades;
    public TextMeshProUGUI prefabUpgrades;

    private void Start()
    {
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            scriptUpgrades.text = "Suspension: " + suspensionManager.frontWheelDampingRatio + "\n" +
                "Engine: " + engineManager.frontWheeelPower + "\n" +
                "Tyre: " + tyreManager.frontLinearDrag + "\n" +
                "Chasis: " + chasisManager.veichelMass + "\n" +
                "Currency" + CurrencyManager.Instance.GetTotalCurrency(); ;

            prefabUpgrades.text = "Suspension: " + carController.frontWheel.suspension + "\n" +
                "Engine: " + carController.backWheel.motor + "\n" +
                "Chasis: " + carController.GetComponent<Rigidbody2D>().mass;
        }
        else if (SceneManager.GetActiveScene().name == "Scene1")
        {
            prefabUpgrades.text = "Suspension: " + carController.frontWheel.suspension.dampingRatio + "\n" +
                "Engine: " + carController.backWheel.motor.motorSpeed + "\n" +
                "Chasis: " + carController.GetComponent<Rigidbody2D>().mass + "\n" +
                "Currency" + CurrencyManager.Instance.GetTotalCurrency();
        }
    }
}