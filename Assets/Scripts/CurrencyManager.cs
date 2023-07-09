using UnityEngine;
using static TyreManager;

public class CurrencyManager : MonoBehaviour
{
    public int baseCurrencyPer100Meters = 10; // Base currency earned per 100 meters traveled
    public int backflipCurrency = 50; // Currency earned for successful combo stunts
    public int frontflipCurrency = 50; // Currency earned for successful combo stunts
    public int hiddenCollectibleCurrency = 100; // Currency earned for collecting hidden collectibles

    private int totalCurrency = 0; // Total currency earned

    private static CurrencyManager instance; // Create a static instance of the CurrencyManager class
    private const string CurrencyKey = "Money"; // Key to store the total currency in PlayerPrefs
    private TotalAmount totalAmount = new(); // Create a new instance of the TotalAmount class

    private CarController carController;

    public static CurrencyManager Instance
    {
        get
        {
            // If the instance is null, find the CurrencyManager in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<CurrencyManager>();

                // If no instance is found, create a new game object and attach the CurrencyManager script
                if (instance == null)
                {
                    GameObject currencyManagerObj = new GameObject("CurrencyManager");
                    instance = currencyManagerObj.AddComponent<CurrencyManager>();
                }
            }

            return instance;
        }
    }

    // Function to calculate and add base currency based on distance traveled
    public void AddBaseCurrency(float distanceTraveled)
    {
        int baseCurrency = Mathf.RoundToInt((distanceTraveled/ 100) * baseCurrencyPer100Meters);
        totalCurrency += baseCurrency;
        Debug.Log("TotalCurrency: " + totalCurrency);
        SaveTotalAmount();
    }

    // Function to add currency for successful combo stunts
    public void AddComboStuntCurrency()
    {
        Debug.Log("BACKFLIP" + totalCurrency);
        if (carController.CheckFlip() == 1)
        {
            Debug.Log("BACKFLIP" + totalCurrency);
            totalCurrency += backflipCurrency;
        }
        else if (carController.CheckFlip() == -1)
        {
            totalCurrency += frontflipCurrency;
        }
        SaveTotalAmount();
    }

    // Function to add currency for collecting hidden collectibles
    public void AddHiddenCollectibleCurrency()
    {
        totalCurrency += hiddenCollectibleCurrency;
        SaveTotalAmount();
    }

    // Function to add currency for achieving specific milestones or completing achievements
    public void AddAchievementCurrency(int achievementCurrency)
    {
        totalCurrency += achievementCurrency;
        SaveTotalAmount();
    }

    // Function to retrieve the current total currency
    public int GetTotalCurrency()
    {
        if (PlayerPrefs.HasKey(CurrencyKey))
        {
            totalCurrency = PlayerPrefs.GetInt("TotalAmount");
        }
        return totalCurrency;
    }

    // Function to reset the current total currency
    public void ResetCurrency()
    {
        totalCurrency = 0;
        Debug.Log("Reset TotalCurrency: " + totalCurrency);
        SaveTotalAmount();
    }

    public bool AllowUpgrade(int amountrequired)
    {
        if (totalCurrency - amountrequired >= 0)
        {
            totalCurrency -= amountrequired;
            SaveTotalAmount();
        }
        return totalCurrency >= amountrequired;
    }

    // Function to save the current total currency
    private void SaveTotalAmount()
    {
        totalAmount.money = totalCurrency;
        PlayerPrefs.SetInt("TotalAmount", totalAmount.money);
        string json = JsonUtility.ToJson(totalAmount);
        PlayerPrefs.SetString(CurrencyKey, json);
        PlayerPrefs.Save();
    }

    // Class to store the total currency
    [System.Serializable]
    public class TotalAmount
    {
        public int money;
    }
}
