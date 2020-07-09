using UnityEngine.UI;
public enum CurrencyType
{
    Main,
    Scientific,
    Potato,
    Farmer,
    Camp,
    Truck
}
public interface ICurrency
{
    string Name { get; set; }
    CurrencyType Type { get; set; }
    ShortBigInteger Amount { get; set; }

    void Buy(ShortBigInteger cost);
    bool Check(ShortBigInteger cost);
}