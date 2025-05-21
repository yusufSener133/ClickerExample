using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUpdate : MonoBehaviour
{
    [Header("Componentes")]
    public TMP_Text priceText;
    public TMP_Text incomeInfoText;
    public TMP_Text upgradeNameText;
    public Button button;
    public Image charImage;

    [Header("Managers")]
    public GameManager gameManager;

    [Header("Generator Values")]
    public string upgradeName = "";
    public int startPrice = 15;
    public float upgradePriceMultiplier;
    public float cookiesPerUpgrade = 0.1f;

    private int _level = 0;

    private void Start()
    {
        UpdateUI();
    }

    public void ClickAction()
    {
        int price = CalculatePrice();
        bool purchaseSuccess = gameManager.PurchaseAction(price);
        if (purchaseSuccess)
        {
            _level++;
            UpdateUI();
        }
    }
    public void UpdateUI()
    {
        priceText.text = CalculatePrice().ToString();
        incomeInfoText.text = _level.ToString() + " x " + cookiesPerUpgrade + "/s";

        bool canAfford = gameManager.count >= CalculatePrice();
        button.interactable = canAfford;

        bool isPurchase = _level > 0;
        charImage.color = isPurchase ? Color.white : Color.black;
        upgradeNameText.text = isPurchase ? upgradeName : " ??? ";
    }
    private int CalculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, _level));
        return price;
    }
    public float CalculateIncomePerSecond()
    {
        return cookiesPerUpgrade * _level;
    }

}/**/
