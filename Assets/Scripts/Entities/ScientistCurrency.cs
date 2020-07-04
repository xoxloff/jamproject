using UnityEngine;
using UnityEngine.UI;

public class ScientistCurrency : MonoBehaviour, ICurrency
{
    public ScientistCurrency(int id)
    {
        Id = id;
    }
    public ScientistCurrency(int id, string name) : this(id)
    {
        Name = name;
    }

    public int Id { get; private set; }
    public Image Image { get; set; }
    public string Name { get; set; }
    public ShortBigInteger Value { get; set; }
}