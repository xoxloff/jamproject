using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour, ICurrency
{
    [SerializeField]
    private int id;
    [SerializeField]
    private Image image;
    [SerializeField]
    private string name;
    [SerializeField]
    private ShortBigInteger amount;
    [SerializeField]
    private CurrencyType type;
    [SerializeField]
    private List<ICurrency> cost;

    public int Id
    {
        get => id;
        set => id = value;
    }
    public Image Image
    {
        get => image;
        set => image = value;
    }
    public string Name
    {
        get => name;
        set => name = value;
    }
    public CurrencyType Type
    {
        get => type;
        set => type = value;
    }
    public ShortBigInteger Amount
    {
        get => amount;
        set => amount = value;
    }
    public List<ICurrency> Cost
    {
        get => cost;
        set => cost = value;
    }

    public bool Check(ShortBigInteger cost) => Amount >= cost;

    public void Buy(ShortBigInteger cost)
    {
        if (Check(cost))
        {
            Amount -= cost;
        }
    }

    
    public Product(int id, Image image, string name, ShortBigInteger amount, CurrencyType type, List<ICurrency> cost)
    {
        Id = id;
        Image = image;
        Name = name;
        Amount = amount;
        Type = type;
        Cost = cost;
    }
    public static Product GetCostValue(Product product, ShortBigInteger amount) => new Product(product.Id, product.Image, product.Name, amount, product.Type, product.Cost);
}
