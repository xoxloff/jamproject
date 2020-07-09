using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private CustomSlider mainCurrencySlider;
    [SerializeField]
    private CustomSlider scientificCurrencySlider;
    [SerializeField]
    private CustomSlider rankSlider;

    [SerializeField]
    private GameObject questPrefab;
    [SerializeField]
    private GameObject questContainer;

    [SerializeField]
    private GameObject factoryPrefab;
    [SerializeField]
    private GameObject factoryContainer;
    [SerializeField]
    private Animator mainCurrencySliderAnimator;

    private List<Product> products;

    public List<Product> Products { get => products; set => products = value; }


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
        set {
            currentRankValue = value;
            UpdateRankSlider();
        }
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

    public PlayerInfo()
    {

    }

    private void Start()
    {
        //set factories
        Factory newFactory = Instantiate(factoryPrefab, factoryContainer.transform).GetComponent<Factory>();
        Factories.Add(newFactory);
        PurchaseMode = PurchaseModeEnum.X1;
        mainCurrency = new MainCurrency(0);
        ScientistCurrency = new ScientistCurrency(0);
        purchaseModeText.text = GetPurchaseMode();
        foreach (var factory in Factories)
        {
            factory.PlayerRef = this;
            factory.PlayerTextUpdate += Factory_PlayerTextUpdate;
            factory.ScientificCurrencyUpdate += Manufacture_ScientificCurrencyUpdate;
        }
        //set products
        products = new List<Product>();
        products.Add(AddProduct("Potata",
                                null,
                                CurrencyType.Potato,
                                new MainCurrency(1)));
        products.Add(AddProduct("Farmer",
                    null,
                    CurrencyType.Farmer,
                    new MainCurrency(1),
                    Product.GetCostValue(products[0], 1)));
        products.Add(AddProduct("Camp",
            null,
            CurrencyType.Camp,
            new MainCurrency(1),
            Product.GetCostValue(Products[1], 20)));
        Products[1].Amount = 1;
        Products[2].Amount = 1;

        //set quests - пока хардкод, далее из файла
        Quests.Add(Instantiate(questPrefab, questContainer.transform).GetComponent<Quest>());
        Quests.Add(Instantiate(questPrefab, questContainer.transform).GetComponent<Quest>());
        Quests.Add(Instantiate(questPrefab, questContainer.transform).GetComponent<Quest>());
        Quests[0].Describtion = "Potato";
        Quests[1].Describtion = "Potato";
        Quests[2].Describtion = "Potato";
        Quests[0].requiredAmount = (ShortBigInteger)"100";
        Quests[1].requiredAmount = (ShortBigInteger)"1 A";
        Quests[2].requiredAmount = (ShortBigInteger)"10 A";
        foreach (var quest in Quests)
        {
            quest.Product = Products[0];
            quest.UpdateTextFields();
            quest.PlayerRef = this;
        }


        StartCoroutine(UpdateMainCurrency());
        UpdateRankSlider();
    }

    public Product AddProduct(string name, Image image, CurrencyType type, params ICurrency[] cost) => new Product(products.Count, image, name, 0, type, cost.ToList());

    private void Update()
    {

    }

    public void ClickPurchaseModeButton()
    {
        PurchaseMode = (PurchaseModeEnum)(((int)PurchaseMode + 1) % 4);
        purchaseModeText.text = GetPurchaseMode();
        UpdateBuyButtonsText();
    }
    public void UpdateScientificCurrencyText()
    {
        scientificCurrencySlider.Text.text = ScientistCurrency.Amount.ToString();
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
            mainCurrencySliderAnimator.speed = 1;
            mainCurrencySliderAnimator.Play("SliderAnim");
            MainCurrency.Amount++;
            UpdateMainCurrencyText();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateMainCurrencyText()
    {
        mainCurrencySlider.Text.text = MainCurrency.Amount.ToString();
        UpdateBuyButtonsText();
    }
    public void UpdateBuyButtonsText()
    {
        foreach (var factory in Factories)
        {
            foreach (var manufacture in factory.Manufactures)
            {
                var purchaseNumber = factory.CheckManufacturePurchase(manufacture);
                var purchaseText = purchaseNumber == null ? "x0" : $"x{purchaseNumber}";
                manufacture.PurchaseButtonText.text = $"BUY {purchaseText}";
            }
        }
    }

    private void Manufacture_ScientificCurrencyUpdate(object sender, AddCurrencyEventArgs e)
    {
        ScientistCurrency.Amount += e.AddingCurrency;
        UpdateScientificCurrencyText();
    }

    private void UpdateRankSlider()
    {
        rankSlider.Text.text = currentRankValue + "/" + RequiredRankValue;
        rankSlider.DrawLayer((float)currentRankValue / (float)RequiredRankValue);
    }

    private void Factory_PlayerTextUpdate(object sender, EventArgs e)
    {
        UpdateMainCurrencyText();
    }

}
