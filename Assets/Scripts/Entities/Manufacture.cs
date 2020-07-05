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

    #region UI
    [SerializeField]
    private Text productsNumberText;
    [SerializeField]
    private FillLayer productsFillLayer;
    [SerializeField]
    private Text addingProductsNumberText;
    [SerializeField]
    private FillLayer addingProductsFillLayer;
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
        productsNumberText.text = workersNumber.ToString();
        addingProductsNumberText.text = addingProducts.ToString();
    }

    public void BuyWorker(ShortBigInteger workerNumber)
    {
        workersNumber += workerNumber;
        addingProducts = workersNumber * addingProductsNumber * productsRatio;
        productsFillLayer.DrawLayer(ShortBigInteger.Division(workersNumber, scientificTrigger));

        if (workersNumber + 1 > scientificTrigger)
        {
            scientificTrigger *= 10;
        }
        UpdateTextFields();
    }

}
