using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainCurrency : MonoBehaviour, ICurrency
{
    [SerializeField]
    private static Image image;
    [SerializeField]
    private string name;
    [SerializeField]
    private ShortBigInteger amount;
    [SerializeField] 
    private CurrencyType type;

    public static Image Image
    {
        get => image;
        set => image = value;
    }
    public CurrencyType Type
    {
        get => type;
        set => type = value;
    }
    public string Name
    {
        get => name;
        set => name = value;
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

    public MainCurrency(ShortBigInteger amount)
    {
        this.name = "Main";
        this.amount = amount;
        this.type = CurrencyType.scientific;
    }
}