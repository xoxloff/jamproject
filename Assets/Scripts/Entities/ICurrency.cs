using UnityEngine.UI;

public interface ICurrency
{
    Image Image { get; set; }
    string Name { get; set; }
    ShortBigInteger Amount { get; set; }
}