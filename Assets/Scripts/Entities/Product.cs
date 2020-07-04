using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour, ICurrency
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private string name;
    [SerializeField]
    private ShortBigInteger amount;

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
    public ShortBigInteger Amount
    {
        get => amount;
        set => amount = value;
    }
}
