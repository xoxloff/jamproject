using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    public enum PurchaseModeEnum
    {
        X1,
        X10,
        X100,
        Max
    };
   
    [SerializeField]
    private Product mainProduct;
    [SerializeField]
    private List<Manufacture> manufactures;
    [SerializeField] 
    private PurchaseModeEnum purchaseMode;

    [SerializeField]
    private GameObject manufacturePrefab;
    [SerializeField]
    private GameObject manufactureContainer;
    [SerializeField]
    private Text factoryText;

    public Product MainProduct { get => mainProduct; set => mainProduct = value; }
    public List<Manufacture> Manufactures { get => manufactures; set => manufactures = value; }
    public PurchaseModeEnum PurchaseMode { get => purchaseMode; set => purchaseMode = value; }

    private void Start()
    {
        MainProduct = new Product() { Name = "Картоха", Amount = new ShortBigInteger(0) };
        Manufactures = new List<Manufacture>();
        PurchaseMode = PurchaseModeEnum.X1;
        Manufactures.Add(Instantiate(manufacturePrefab, manufactureContainer.transform).GetComponent<Manufacture>());
        Manufactures[0].FactoryTextUpdate += Factory_FactoryTextUpdate;
    }

    private void Factory_FactoryTextUpdate(object sender, System.EventArgs e)
    {
        factoryText.text = MainProduct.Amount.ToString();
    }
}
