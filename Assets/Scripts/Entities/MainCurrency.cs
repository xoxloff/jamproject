using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainCurrency : MonoBehaviour, ICurrency
{

    [SerializeField]
    private int id;
    [SerializeField]
    private Image image;
    [SerializeField]
    private string name;
    [SerializeField]
    private ShortBigInteger value;

    public MainCurrency(int _id) : base()
    {
        Id = _id;
    }
    public MainCurrency(int id, string _name) : this(id)
    {
        Name = _name;
    }
    public int Id { get; private set; }

    public Image Image { get; set; }

    public string Name { get; set; }

    public ShortBigInteger Value { get; set; }
}