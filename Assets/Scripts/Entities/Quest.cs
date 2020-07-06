using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private int description;
    [SerializeField] 
    private Image icon;
    [SerializeField]
    private int name;
    [SerializeField]
    private ShortBigInteger requiredAmount;
    [SerializeField]
    private ShortBigInteger currentAmount;
    [SerializeField]
    private Manufacture manufacture;
    [SerializeField]
    private List<Award> awards;

    #region UI
    public Text Description;
    public Text CurrentProcess;
    #endregion


    private void Start()
    {
        currentAmount = (ShortBigInteger)"30 B";
        requiredAmount = (ShortBigInteger)"50 B";

        UpdateTextFields();
    }

    private void Update()
    {
        
    }

    public void ClickQuestButton()
    {
        //Reminder: Нажимается только тогда, когда выполнено условие current>=required
    }

    private void UpdateTextFields()
    {
        Description.text = "Desc";
        CurrentProcess.text = currentAmount.ToString() + "/" + requiredAmount.ToString();
    }
}
