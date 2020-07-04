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
    public int Id { get; private set; }
    public Image Image { get; set; }
    public string Name { get; set; }
    public ShortBigInteger Value { get; set; }
}