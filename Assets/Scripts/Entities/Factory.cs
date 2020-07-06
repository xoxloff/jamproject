using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private Product mainProduct;
    [SerializeField]
    private List<Manufacture> manufactures;
    [SerializeField]
    private List<Product> products;

    [SerializeField]
    private GameObject manufacturePrefab;
    [SerializeField]
    private GameObject manufactureContainer;
    [SerializeField]
    private Text factoryText;

    public PlayerInfo PlayerRef { get; set; }
    public Product MainProduct
    {
        get => mainProduct;
        set => mainProduct = value;
    }
    public List<Manufacture> Manufactures
    {
        get => manufactures;
        set => manufactures = value;
    }
    public List<Product> Products
    {
        get => products;
        set => products = value;
    }


    public event EventHandler<EventArgs> PlayerTextUpdate;

    void Start()
    {
        products = new List<Product>();
        Manufactures = new List<Manufacture>();
        MainProduct = AddProduct("Potata",
                                null,
                                CurrencyType.Potato,
                                new MainCurrency(1));
        products.Add(MainProduct);
        products.Add(AddProduct("Farmer",
                    null,
                    CurrencyType.Farmer,
                    new MainCurrency(1),
                    Product.GetCostValue(MainProduct, 1)));
        //products.Add(AddProduct("Some", null, CurrencyType.bus, new MainCurrency(1), Product.GetCostValue(Products[Products.Count - 1], (Products.Count - 1) * 1.5f)));
        Products[1].Amount = 1;
        Manufactures.Add(AddManufacture(MainProduct, Products[1]));
        // AddManufacture(products[1])
    }

    public Product AddProduct(string name, Image image, CurrencyType type, params ICurrency[] cost) => new Product(products.Count, image, name, 0, type, cost.ToList());

    public Manufacture AddManufacture(Product currency, Product workerProduct)
    {
        var manufacture = Instantiate(manufacturePrefab, manufactureContainer.transform).GetComponent<Manufacture>();
        manufacture.Workers = workerProduct;
        manufacture.ScientificTrigger = new ShortBigInteger(10);
        manufacture.ProductsRatio = "1";
        manufacture.AddingProductsNumber = 50;
        manufacture.Product = currency;
        manufacture.FactoryTextUpdate += Factory_FactoryTextUpdate;
        manufacture.Buy += Manufacture_Buy;
        return manufacture;
    }

    public ShortBigInteger? CheckManufacturePurchase(Manufacture manufacture)
    {
        var isBought = false;
        var count = new List<ShortBigInteger>();
        if (manufacture.Workers.Cost.Count == 0)
        {
            throw new Exception("empty cost");
        }
        foreach (var currency in manufacture.Workers.Cost)
        {

            switch (currency)
            {
                case MainCurrency c:
                    {
                        var cost = GetPurchaseValue(c.Amount, PlayerRef.MainCurrency.Amount);
                        isBought = PlayerRef.MainCurrency.Check(cost * c.Amount);
                        if (isBought)
                        {
                            count.Add(cost);
                        }
                        break;
                    }
                case ScientistCurrency c:
                    {
                        var cost = GetPurchaseValue(c.Amount, PlayerRef.ScientistCurrency.Amount);
                        isBought = PlayerRef.ScientistCurrency.Check(cost * c.Amount);
                        if (isBought)
                        {
                            count.Add(cost);
                        }
                        break;
                    }
                case Product c:
                    {
                        var cost = GetPurchaseValue(c.Amount, Products[c.Id].Amount);
                        isBought = Products[c.Id].Check(cost * c.Amount);
                        if (isBought)
                        {
                            count.Add(cost);
                        }
                        break;
                    }
            }
            if (!isBought)
            {
                return null;
            }
        }

        return count.Min(c => c.Value);
    }
    private ShortBigInteger GetPurchaseValue(ShortBigInteger cost, ShortBigInteger max)
    {
        switch (PlayerRef.PurchaseMode)
        {
            case PlayerInfo.PurchaseModeEnum.X1:
                {
                    return 1;
                }
            case PlayerInfo.PurchaseModeEnum.X10:
                {
                    var num = (max / 10);
                    if (num == 0)
                    {
                        goto case PlayerInfo.PurchaseModeEnum.X1;
                    }
                    return num;
                }
            case PlayerInfo.PurchaseModeEnum.X50:
                {
                    var num = (max / 2) / cost;
                    if (num == 0)
                    {
                        goto case PlayerInfo.PurchaseModeEnum.X10;
                    }
                    return num;
                }
            case PlayerInfo.PurchaseModeEnum.X100:
                {
                    var num = max / cost;
                    return num;
                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void Manufacture_Buy(object sender, BuyEventArgs e)
    {
        var manufacture = sender as Manufacture;
        if (manufacture == null)
        {
            return;
        }

        var minCostNumber = CheckManufacturePurchase(manufacture);
        if (minCostNumber is null)
        {
            return;
        }
        foreach (var currency in e.Currencies)
        {
            switch (currency)
            {
                case MainCurrency c:
                    PlayerRef.MainCurrency.Buy(minCostNumber.Value * c.Amount);
                    break;
                case ScientistCurrency c:
                    PlayerRef.ScientistCurrency.Buy(minCostNumber.Value * c.Amount);
                    break;
                case Product c:
                    Products[c.Id].Buy(minCostNumber.Value * c.Amount);
                    break;
            }
        }
        manufacture.BuyWorker(minCostNumber.Value);
        UpdateTextFields();
    }

    private void Factory_FactoryTextUpdate(object sender, ManufactureEventArgs e)
    {
        var manufacture = sender as Manufacture;
        if (manufacture == null)
        {
            return;
        }

        foreach (var m in Manufactures)
        {
            m.UpdateTextFields();
        }

        UpdateTextFields();
    }

    private void UpdateTextFields()
    {
        factoryText.text = MainProduct.Amount.ToString();
        PlayerTextUpdate?.Invoke(this, EventArgs.Empty);
    }
}
