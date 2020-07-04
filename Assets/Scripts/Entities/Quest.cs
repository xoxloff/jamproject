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

}
