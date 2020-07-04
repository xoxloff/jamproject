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


    [SerializeField]
    public int Id { get; private set; }

    [SerializeField]
    public Image Image { get; set; }

    [SerializeField]
    public string Name { get; set; }

    [SerializeField]
    public ShortBigInteger Value { get; set; }
}