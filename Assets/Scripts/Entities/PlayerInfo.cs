using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public enum PurchaseModeEnum
    {
        X1,
        X10,
        X50,
        X100
    };

    [SerializeField]
    private int rank;
    [SerializeField]
    private int currentRankValue;
    [SerializeField]
    private int requiredRankValue;
    [SerializeField]
    private MainCurrency mainCurrency;
    [SerializeField]
    private ScientistCurrency scientistCurrency;
    [SerializeField]
    private PurchaseModeEnum purchaseMode;
    [SerializeField]
    private List<Scientist> scientists;
    [SerializeField]
    private List<Factory> factories;
    [SerializeField]
    private List<Quest> quests;
    [SerializeField]
    private Text purchaseModeText;
    [SerializeField]
    private Text mainCurrencyText;
    [SerializeField]
    private Text scientificCurrencyText;
    [SerializeField]
    private CustomSlider rankSlider;

    public PurchaseModeEnum PurchaseMode
    {
        get => purchaseMode;
        set => purchaseMode = value;
    }
    public int Rank
    {
        get => rank;
        set => rank = value;
    }
    public int CurrentRankValue
    {
        get => currentRankValue;
        set => currentRankValue = value;
    }
    public int RequiredRankValue
    {
        get => requiredRankValue;
        set => requiredRankValue = value;
    }

    public MainCurrency MainCurrency
    {
        get => mainCurrency;
        set => mainCurrency = value;
    }
    public ScientistCurrency ScientistCurrency
    {
        get => scientistCurrency;
        set
        {
            scientistCurrency = value;
            UpdateScientificCurrencyText();
        }
    }
    public List<Scientist> Scientists
    {
        get => scientists;
        set => scientists = value;
    }
    public List<Factory> Factories
    {
        get => factories;
        set => factories = value;
    }
    public List<Quest> Quests
    {
        get => quests;
        set => quests = value;
    }

    private void Start()
    {
        PurchaseMode = PurchaseModeEnum.X100;
        mainCurrency = new MainCurrency(0);
        ScientistCurrency = new ScientistCurrency(0);
        purchaseModeText.text = GetPurchaseMode();
        foreach (var factory in factories)
        {
            factory.PlayerRef = this;
        }

        StartCoroutine(UpdateMainCurrency());
        UpdateRankSlider();
    }

    private void Update()
    {
    }

    public void ClickPurchaseModeButton()
    {
        PurchaseMode = (PurchaseModeEnum)(((int)PurchaseMode + 1) % 4);
        purchaseModeText.text = GetPurchaseMode();
        UpdateBuyButtonsText();
    }
    public void UpdateScientificCurrencyText()//Сделать евентом, вызывать при изменении
    {
        scientificCurrencyText.text = ScientistCurrency.Amount.ToString();
    }

    private string GetPurchaseMode()
    {
        switch (PurchaseMode)
        {
            case PurchaseModeEnum.X1:
                return "x1";
            case PurchaseModeEnum.X10:
                return "10%";
            case PurchaseModeEnum.X50:
                return "50%";
            case PurchaseModeEnum.X100:
                return "max";
            default:
                throw new FormatException();
        }
    }

    private IEnumerator UpdateMainCurrency()
    {
        while (true)
        {
            MainCurrency.Amount++;
            UpdateMainCurrencyText();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateMainCurrencyText()
    {
        mainCurrencyText.text = MainCurrency.Amount.ToString();
        UpdateBuyButtonsText();
    }
    private void UpdateBuyButtonsText()
    {
        foreach (var factory in Factories)
        {
            foreach (var manufacture in factory.Manufactures)
            {
                var purchaseNumber = factory.CheckManufacturePurchase(manufacture);
                var purchaseText = purchaseNumber == null ? "": purchaseNumber.ToString() ;
                manufacture.PurchaseButtonText.text = "BUY x" + purchaseText;
            }
        }
    }

    private void UpdateRankSlider()
    {
        rankSlider.Text.text = currentRankValue + "/" + RequiredRankValue;
        rankSlider.DrawLayer((float)currentRankValue / (float)RequiredRankValue);
    }

}
