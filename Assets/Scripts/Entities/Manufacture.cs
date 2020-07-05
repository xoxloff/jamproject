using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manufacture : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Product productsNumber; //current
    [SerializeField]
    private Product addingProductsNumber; //adding
    [SerializeField]
    private ShortBigInteger productsRatio; // current ration
    [SerializeField]
    private ShortBigInteger addingProductsRatio; // adding ratio
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
    private Text productsNumberText;
    [SerializeField]
    private FillLayer productsFillLayer;
    [SerializeField]
    private Text addingProductsNumberText;
    [SerializeField]
    private FillLayer addingProductsFillLayer;

    private Product _kartoha; // remove
    [SerializeField]
    private Text _kartohaText;

    public float ProductionTime { get=>productionTime; set=>productionTime=value; }

    private void Start()
    {
        productsNumber = new Product() { Amount = new ShortBigInteger(1)};
        addingProductsNumber = new Product() {Amount=new ShortBigInteger(50)};
        _kartoha = new Product() {Name="Картоха", Amount= new ShortBigInteger(0) };
        productsNumberText.text = "1";
        addingProductsNumberText.text = "0";
        scientificTrigger = new ShortBigInteger(10);
        productsRatio = (ShortBigInteger)"1";
        addingProductsRatio = (ShortBigInteger)"50";
        UpdateTextFields();
    }

    public void ProductBtnClick(GameObject go)
    {
        _kartoha.Amount += addingProductsNumber.Amount;

        Debug.Log(_kartoha.Amount);
        UpdateTextFields();
    }

    public void BuyBtnClick(GameObject go)
    {
        productsNumber.Amount += 1;
        addingProductsNumber.Amount = productsNumber.Amount*addingProductsRatio;

        productsFillLayer.DrawLayer(ShortBigInteger.Division(productsNumber.Amount, scientificTrigger));

        if (productsNumber.Amount + 1 > scientificTrigger)
        {
            scientificTrigger *= 10;
            Debug.Log(scientificTrigger.ToString());
        }
        UpdateTextFields();
    }

    private void UpdateTextFields()
    {
        productsNumberText.text = productsNumber.Amount.ToString();
        addingProductsNumberText.text = addingProductsNumber.Amount.ToString();
        _kartohaText.text = _kartoha.Amount.ToString();
    }

}
