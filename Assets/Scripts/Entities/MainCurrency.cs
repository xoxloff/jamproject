using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainCurrency : MonoBehaviour, ICurrency
{
    public MainCurrency(int id) : base()
    {
        Id = id;
    }
    public MainCurrency(int id, string name) : this(id)
    {
        Name = name;
    }
    [SerializeField]
    public int Id { get; private set; }

    [SerializeField]
    public Image Image { get; set; }

    [SerializeField]
    public string Name { get; set; }

    [SerializeField]
    public ShortBigInteger Value { get; set; }
}