using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scientist : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private string name;
    [SerializeField]
    private string discription;
    [SerializeField]
    private int upgradeCost;
    [SerializeField]
    private Manufacture manufacture;
    [SerializeField]
    private int level;
    [SerializeField]
    private int performanceRatio;
}
