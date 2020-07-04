using UnityEngine.UI;

public interface ICurrency
{
    public int Id { get; }
    public Image Image { get; set; }
    public string Name { get; set; }
    public ShortBigInteger Value { get; set; }
}