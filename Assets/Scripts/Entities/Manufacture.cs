using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manufacture : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Product product;
    [SerializeField]
    private ShortBigInteger workersNumber;
    [SerializeField]
    private ShortBigInteger addingProducts;
    [SerializeField]
    private ShortBigInteger productsRatio; // current ration
    [SerializeField]
    private ShortBigInteger addingProductsNumber;
    [SerializeField]
    private float productionTime;
    [SerializeField]
    private float timeRatio;
    [SerializeField]
    private List<ICurrency> cost;
    [SerializeField]
    private Scientist scientist;
    [SerializeField]
    private ScientistCurrency scientistCurrency;
    [SerializeField]
    private ShortBigInteger scientificTrigger; //rename
    [SerializeField]
    private ShortBigInteger scientificTriggerBorder; //rename

    #region UI
    [SerializeField]
    private CustomSlider productsSlider;
    [SerializeField]
    private CustomSlider addingProductsSlider;
    public Text PurchaseButtonText;
    #endregion

    public event EventHandler<ManufactureEventArgs> FactoryTextUpdate;
    public event EventHandler<ManufactureEventArgs> ScientificCurrencyUpdate;
    public event EventHandler<BuyEventArgs> Buy;
    public Product Product
    {
        get => product;
        set => product = value;
    }
    public float ProductionTime
    {
        get => productionTime;
        set => productionTime = value;
    }
    public ShortBigInteger WorkersNumber
    {
        get => workersNumber;
        set => workersNumber = value;
    }
    public ShortBigInteger AddingProducts
    {
        get => addingProducts;
        set => addingProducts = value;
    }
    public ShortBigInteger ProductsRatio
    {
        get => productsRatio;
        set => productsRatio = value;
    }
    public ShortBigInteger AddingProductsNumber
    {
        get => addingProductsNumber;
        set => addingProductsNumber = value;
    }
    public float TimeRatio
    {
        get => timeRatio;
        set => timeRatio = value;
    }
    public List<ICurrency> Cost
    {
        get => cost;
        set => cost = value;
    }
    public Scientist Scientist
    {
        get => scientist;
        set => scientist = value;
    }
    public ScientistCurrency ScientistCurrencyUpdate
    {
        get => scientistCurrency;
        set => scientistCurrency = value;
    }

    public ShortBigInteger ScientificTrigger
    {
        get => scientificTrigger;
        set => scientificTrigger = value;
    }

    private void Start()
    {
        ScientistCurrencyUpdate = new ScientistCurrency(0);
        ScientificTrigger = (ShortBigInteger)"10";
        scientificTriggerBorder = (ShortBigInteger)"1";
        addingProducts = workersNumber * addingProductsNumber * productsRatio;
        UpdateTextFields();
    }

    public void ProductBtnClick()
    {
        UpdateTextFields();
        product.Amount += addingProducts;
        FactoryTextUpdate?.Invoke(this, new ManufactureEventArgs(addingProducts));
    }
    public void BuyBtnClick()
    {
        Buy?.Invoke(this, new BuyEventArgs(Cost));
    }
    public void UpdateTextFields()
    {
        productsSlider.Text.text = workersNumber.ToString();
        addingProductsSlider.Text.text = addingProducts.ToString();
    }

    public void UpdateScientificCurrency(ShortBigInteger value)
    {
        ScientificCurrencyUpdate?.Invoke(this, new ManufactureEventArgs(value));
    }

    public void BuyWorker(ShortBigInteger workerNumber)
    {
        workersNumber += workerNumber;
        addingProducts = workersNumber * addingProductsNumber * productsRatio;
        productsSlider.DrawLayer(ShortBigInteger.Division(workersNumber, scientificTrigger));

        CheckScientificTrigger();
        UpdateTextFields();
    }

    private void CheckScientificTrigger()
    {
        var workers = workersNumber;
        if (workers >= ScientificTrigger)
        {
            var scientificCurrencyCount = 0;
            while (workers / 10 >= scientificTriggerBorder)
            {
                scientificCurrencyCount += 1;
                workers /= 10;
            }

            scientificTriggerBorder *= (ShortBigInteger)Math.Pow(10,scientificCurrencyCount);
            ScientificTrigger *= 10;
            scientistCurrency.Amount = scientificCurrencyCount * 5;//создать переменную
            UpdateScientificCurrency(scientistCurrency.Amount);
            scientistCurrency.Amount = 0;
        }

        Debug.Log(ScientistCurrencyUpdate.Amount);
    }

}
