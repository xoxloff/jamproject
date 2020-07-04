using UnityEngine;
using UnityEngine.UI;

public class ScientistCurrency : MonoBehaviour, ICurrency
{
    [SerializeField]
    private int id;
    [SerializeField]
    private Image image;
    [SerializeField]
    private string name;
    [SerializeField]
    private ShortBigInteger value;

    public ScientistCurrency(int _id)
    {
        Id = _id;
    }
    public ScientistCurrency(int id, string _name) : this(id)
    {
        Name = _name;
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