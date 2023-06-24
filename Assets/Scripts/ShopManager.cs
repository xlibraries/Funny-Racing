using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    private List<int> suspensionPrice= new() { 300, 400, 550, 750};
    private int sI = 0;

    private List<int> enginePrice = new() { 300, 400, 550, 750 };
    private int eI = 0;

    private List<int> tyrePrice = new() { 300, 400, 550, 750 };
    private int tI = 0;

    private List<int> chakraChargePrice = new() { 300, 400, 550, 750 };
    private int cI = 0;

    public void UpgradeSuspensions()
    {
        if (CurrencyManager.Instance.AllowUpgrade(suspensionPrice[sI]))
        {
            SuspensionManager.Instance.UpgradeSuspension();
            Debug.Log("Suspension Upgraded");
            sI++;
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void UpgrageEngine()
    {
        if (CurrencyManager.Instance.AllowUpgrade(enginePrice[eI]))
        {
            EngineManager.Instance.UpgradeEngine();
            Debug.Log("Engine Upgraded");
            eI++;
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void UpgradeTyres()
    {
        if (CurrencyManager.Instance.AllowUpgrade(tyrePrice[tI]))
        {
            TyreManager.Instance.UpgradeTyre();
            Debug.Log("Tyre Upgraded");
            tI++;
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void UpgradeChakraCharge()
    {
        //coming soon
        if (CurrencyManager.Instance.AllowUpgrade(chakraChargePrice[cI]))
        {
            //ChakraChargeManager.Instance.UpgradeChakraCharge();
            Debug.Log("Chakra Charge Upgraded");
            cI++;
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void ResetGame()
    {
        SuspensionManager.Instance.ResetSuspension();
        EngineManager.Instance.ResetEngine();
        TyreManager.Instance.ResetTyre();
        //ChakraChargeManager.Instance.ResetChakraCharge();
        CurrencyManager.Instance.ResetCurrency();
    }
}
