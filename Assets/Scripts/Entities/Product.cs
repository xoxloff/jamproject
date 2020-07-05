using System.Collections;
using System.Collections.Generic;
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

    public bool Check(ShortBigInteger cost) => Amount >= cost;

    public void Buy(ShortBigInteger cost)
    {
        if (Check(cost))
        {
            Amount -= cost;
        }
    }

    public Product(int id, Image image, string name, ShortBigInteger amount, CurrencyType type)
    {
        this.id = id;
        this.image = image;
        this.name = name;
        this.amount = amount;
        this.type = type;
    }
}
