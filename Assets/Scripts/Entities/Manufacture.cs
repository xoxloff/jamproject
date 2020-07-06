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
    private Product workers;
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
    public event EventHandler<AddCurrencyEventArgs> ScientificCurrencyUpdate;
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
    public Product Workers
    {
        get => workers;
        set => workers = value;
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
        addingProducts = Workers.Amount * addingProductsNumber * productsRatio;
        UpdateTextFields();

        Debug.Log("Manufacture added");
    }

    public void ProductBtnClick()
    {
        UpdateTextFields();
        product.Amount += addingProducts;
        FactoryTextUpdate?.Invoke(this, new ManufactureEventArgs(addingProducts));
    }
    public void BuyBtnClick()
    {
        Buy?.Invoke(this, new BuyEventArgs(workers.Cost));
    }
   

    public void BuyWorker(ShortBigInteger workerNumber)
    {
        Workers.Amount += workerNumber;
        addingProducts = Workers.Amount * addingProductsNumber * productsRatio;
        productsSlider.DrawLayer(ShortBigInteger.Division(Workers.Amount, scientificTrigger));

        CheckScientificTrigger();
        UpdateTextFields();
    }

    private void CheckScientificTrigger()
    {
        var workers = Workers.Amount;

        if (workers < ScientificTrigger) 
            return;

        var scientificCurrencyCount = 0;
        while (workers / 10 >= scientificTriggerBorder)
        {
            scientificCurrencyCount += 1;
            workers /= 10;
        }

        scientificTriggerBorder *= (ShortBigInteger)Math.Pow(10,scientificCurrencyCount);
        ScientificTrigger *= 10;

        scientistCurrency.Amount = scientificCurrencyCount * 5; //создать переменную
        ScientificCurrencyUpdate?.Invoke(this, new AddCurrencyEventArgs(scientistCurrency.Amount));
        scientistCurrency.Amount = 0;
    }

    public void UpdateTextFields()
    {
        productsSlider.Text.text = Workers.Amount.ToString();
        addingProductsSlider.Text.text = addingProducts.ToString();
    }
}
