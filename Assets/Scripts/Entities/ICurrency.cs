using UnityEngine.UI;

public interface ICurrency
{
    int Id { get; }
    Image Image { get; set; }
    string Name { get; set; }
    ShortBigInteger Value { get; set; }
}