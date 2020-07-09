using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private string description;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private int name;
    public ShortBigInteger requiredAmount;
    public ShortBigInteger currentAmount;
    [SerializeField]
    private Product product;
    [SerializeField]
    private List<Award> awards;
    [SerializeField]
    private GameObject chestPanel;

    public PlayerInfo PlayerRef;

    #region UI
    public Text DescriptionText;
    public Text CurrentProcessText;
    #endregion

    public string Describtion { get => description; set => description = value; }
    public Product Product { get => product; set => product = value; }
    private void Start()
    {
        currentAmount = (ShortBigInteger)"0";

        UpdateTextFields();
        StartCoroutine(CheckProducts());
    }

    IEnumerator CheckProducts()
    {
        while (true)
        {
            if (currentAmount >= requiredAmount)
            {
                chestPanel.SetActive(true);
            }
            else
            {
                currentAmount = Product.Amount;
                UpdateTextFields();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {

    }

    public void ClickQuestButton()
    {
        if (currentAmount >= requiredAmount)
        {
            var randomSCurrency = (ShortBigInteger) Random.Range(50,70);
            PlayerRef.ScientistCurrency.Amount += randomSCurrency;
            PlayerRef.CurrentRankValue++;
            PlayerRef.UpdateScientificCurrencyText();
            Debug.Log("You get reward");
            Destroy(this.gameObject);
        }
    }

    public void UpdateTextFields()
    {
        DescriptionText.text = Describtion;
        CurrentProcessText.text = currentAmount.ToString() + "/" + requiredAmount.ToString();
    }
}
