using UnityEngine;
using UnityEngine.UI;

public class Manufacture : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private ShortBigInteger currentCountOfCurrency;
    [SerializeField]
    private ShortBigInteger additionalCountOfCurrency;
    [SerializeField]
    private ShortBigInteger currentCurrencyMultiplier;
    [SerializeField]
    private ShortBigInteger additionalCurrencyMultiplier;
    [SerializeField]
    private double timeForCreate;
    [SerializeField]
    private double timeMultiplier;
    [SerializeField]
    private ShortBigInteger cost;
    [SerializeField]
    private Scientist scientistCard;
    [SerializeField]
    private int scientificCurrency;
    [SerializeField]
    private int scientificTrigger;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
