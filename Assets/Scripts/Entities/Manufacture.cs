﻿using System.Collections.Generic;
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
    private int productsRatio; // current ration
    [SerializeField]
    private int addingProductsRatio; // adding ratio
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
    private int scientificTrigger; //rename
    [SerializeField]
    private Text productsNumberText;
    [SerializeField]
    private FillLayer fillLayer;


    public float ProductionTime { get=>productionTime; set=>productionTime=value; }

    private void Start()
    {
        productsNumber = new Product();
        productsNumberText.text = "0";
    }

    public void BtnClick(GameObject go)
    {
        productsNumber.Amount+=1;
        productsNumberText.text = productsNumber.Amount.ToString();

        fillLayer.DrawLayer(ShortBigInteger.Division(productsNumber.Amount,scientificTrigger));
        if (productsNumber.Amount+1 > scientificTrigger)
        {
            scientificTrigger *= 10;
        }
    }

}
