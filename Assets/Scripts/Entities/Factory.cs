using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private enum PurchaseModeEnum
    {
        X1,
        X10,
        X100,
        Max
    };
   
    [SerializeField]
    private MainCurrency mainCurrency;
    [SerializeField]
    private List<Manufacture> manufactures;
    [SerializeField] 
    private PurchaseModeEnum purchaseMode;
}
