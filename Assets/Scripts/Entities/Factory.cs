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

    public List<Manufacture> Manufactures
    {
        get => manufactures;
        set => manufactures = value;
    }

    public event EventHandler<EventArgs> PlayerTextUpdate;

    public event EventHandler<AddCurrencyEventArgs> ScientificCurrencyUpdate;

    public Factory()
    {

        // AddManufacture(products[1])
    }

    void Start()
    {
        Manufactures = new List<Manufacture>();

        //products.Add(AddProduct("Some", null, CurrencyType.bus, new MainCurrency(1), Product.GetCostValue(Products[Products.Count - 1], (Products.Count - 1) * 1.5f)));
        var image = Resources.Load<Sprite>("Icons/Products/farmer");
        var image2 = Resources.Load<Sprite>("Icons/Products/camp");
        var image3 = Resources.Load<Sprite>("Icons/Products/truck");
        Manufactures.Add(AddManufacture(PlayerRef.Products[0], PlayerRef.Products[1], 1, image));
        Manufactures.Add(AddManufacture(PlayerRef.Products[1], PlayerRef.Products[2], 2, image2));
        Manufactures.Add(AddManufacture(PlayerRef.Products[2], PlayerRef.Products[3], 4, image3));

        StartCoroutine(AutoIncrement());
    }

    private void Update()
    {



    }

    IEnumerator AutoIncrement()
    {
        if (manufactures.Count > 0)
        {
            var man = manufactures[0];
            man.AutomativeProcess = true;
            while (true)
            {
                man.SliderAnimator.Play("SliderAnim");
                man.StartCoroutine(man.ProductBtnClickCoroutine(1));
                yield return new WaitForSeconds(1);
            }
        }
    }

    public Manufacture AddManufacture(Product currency, Product workerProduct, float productionTime, Sprite img)
    {
        var manufacture = Instantiate(manufacturePrefab, manufactureContainer.transform).GetComponent<Manufacture>();
        manufacture.Workers = workerProduct;
        manufacture.ScientificTrigger = new ShortBigInteger(10);
        manufacture.ProductsRatio = "1";
        manufacture.AddingProductsNumber = 1;
        manufacture.Product = currency;
        manufacture.ProductionTime = productionTime;
        manufacture.SliderAnimator.speed = 1f / manufacture.ProductionTime;
        manufacture.FactoryTextUpdate += Factory_FactoryTextUpdate;
        manufacture.Buy += Manufacture_Buy;
        manufacture.ScientificCurrencyUpdate += Factory_ScientificCurrencyUpdate;
        manufacture.ProductBtnImage.sprite = img;
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
                        var cost = GetPurchaseValue(c.Amount, PlayerRef.Products[c.Id].Amount);
                        isBought = PlayerRef.Products[c.Id].Check(cost * c.Amount);
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
                    if (num == 0)
                    {
                        goto case PlayerInfo.PurchaseModeEnum.X50;
                    }
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
                    PlayerRef.Products[c.Id].Buy(minCostNumber.Value * c.Amount);
                    break;
            }
        }

        manufacture.BuyWorker(minCostNumber.Value);
        PlayerRef.UpdateBuyButtonsText();
        UpdateTextFields();
        foreach (var m in Manufactures)
        {
            m.UpdateAddingProducts();
            m.UpdateTextFields();
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
            m.UpdateAddingProducts();
            m.UpdateTextFields();
        }

        UpdateTextFields();
    }

    private void Factory_ScientificCurrencyUpdate(object sender, AddCurrencyEventArgs e)
    {
        ScientificCurrencyUpdate?.Invoke(sender, e);
    }

    private void UpdateTextFields()
    {
        factoryText.text = PlayerRef.Products[0].Amount.ToString();
        PlayerTextUpdate?.Invoke(this, EventArgs.Empty);
    }
}
