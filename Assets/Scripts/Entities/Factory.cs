using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    void Start()
    {
        products = new List<Product>();
        MainProduct = AddProduct("Potata", null, CurrencyType.potata);
        products.Add(MainProduct);
        Manufactures = new List<Manufacture>
        {
            AddManufacture(MainProduct)
        };
    }

    public Product AddProduct(string name, Image image, CurrencyType type) => new Product(products.Count, image, name, 0, type);

    public Manufacture AddManufacture(Product currency)
    {
        var manufacture = Instantiate(manufacturePrefab, manufactureContainer.transform).GetComponent<Manufacture>();
        manufacture.WorkersNumber = 1;
        manufacture.ScientificTrigger = new ShortBigInteger(10);
        manufacture.ProductsRatio = "1";
        manufacture.AddingProductsNumber = 50;
        manufacture.Product = currency;
        manufacture.FactoryTextUpdate += Factory_FactoryTextUpdate;
        manufacture.Buy += Manufacture_Buy; ;
        manufacture.Cost = new List<ICurrency>
        {
            new MainCurrency(1),
            new Product(currency.Id, currency.Image, currency.Name, 1, currency.Type)
        };

        return manufacture;
    }

    public ShortBigInteger? CheckManufacturePurchase(Manufacture manufacture)
    {
        var isBought = false;
        var count = new List<ShortBigInteger>();
        if (manufacture.Cost.Count == 0)
        {
            throw new Exception("empty cost");
        }
        foreach (var currency in manufacture.Cost)
        {

            switch (currency)
            {
                case MainCurrency c:
                    {
                        var cost = GetPurchaseValue(c.Amount, PlayerRef.MainCurrency.Amount);
                        count.Add(cost);
                        isBought = PlayerRef.MainCurrency.Check(cost * c.Amount);
                        break;
                    }
                case ScientistCurrency c:
                    {
                        var cost = GetPurchaseValue(c.Amount, PlayerRef.ScientistCurrency.Amount);
                        count.Add(cost);
                        isBought = PlayerRef.ScientistCurrency.Check(cost * c.Amount);
                        break;
                    }
                case Product c:
                    {
                        var cost = GetPurchaseValue(c.Amount, Products[c.Id].Amount);
                        count.Add(cost);
                        isBought = Products[c.Id].Check(cost * c.Amount);
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

    private ShortBigInteger GetPurchaseValue(ShortBigInteger cost, ShortBigInteger max)
    {
        switch (PlayerRef.PurchaseMode)
        {
            case PlayerInfo.PurchaseModeEnum.X1:
                return 1;
            case PlayerInfo.PurchaseModeEnum.X10:
                return (max / 10) / cost;
            case PlayerInfo.PurchaseModeEnum.X50:
                return (max / 2) / cost;
            case PlayerInfo.PurchaseModeEnum.X100:
                return max / cost;
            default:
                throw new ArgumentOutOfRangeException();
        }
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
    }
}
