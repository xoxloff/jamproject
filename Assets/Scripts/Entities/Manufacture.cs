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

    #region UI
    [SerializeField]
    private CustomSlider productsSlider;
    [SerializeField]
    private CustomSlider addingProductsSlider;
    public Text PurchaseButtonText;
    #endregion

    public event EventHandler<ManufactureEventArgs> FactoryTextUpdate;
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
    public ScientistCurrency ScientistCurrency
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
        addingProducts = Workers.Amount * addingProductsNumber * productsRatio;
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
        Buy?.Invoke(this, new BuyEventArgs(workers.Cost));
    }
    public void UpdateTextFields()
    {
        productsSlider.Text.text = Workers.Amount.ToString();
        addingProductsSlider.Text.text = addingProducts.ToString();
    }

    public void BuyWorker(ShortBigInteger workerNumber)
    {
        Workers.Amount += workerNumber;
        addingProducts = Workers.Amount * addingProductsNumber * productsRatio;
        productsSlider.DrawLayer(ShortBigInteger.Division(Workers.Amount, scientificTrigger));

        if (Workers.Amount + 1 > scientificTrigger)
        {
            scientificTrigger *= 10;
        }
        UpdateTextFields();
    }

}
