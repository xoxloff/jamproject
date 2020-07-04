using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manufacture : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private ShortBigInteger productsNumber;
    [SerializeField]
    private ShortBigInteger addingProductsNumber;
    [SerializeField]
    private ShortBigInteger productsRatio;
    [SerializeField]
    private ShortBigInteger addingProductsRatio;
    [SerializeField]
    private float productionTime;
    [SerializeField]
    private float timeRatio;
    [SerializeField]
    private List<ICurrency> cost;
    [SerializeField]
    private Scientist scientist;
    [SerializeField]
    private ScientistCurrency scientistCurrency;
    [SerializeField]
    private int scientificTrigger; //?
}
