using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public Quest(int id)
    {
        this.id = id;
    }

    [SerializeField]
    private int id;
    [SerializeField]
    private int description;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private int upgradeCost;
    [SerializeField]
    private int manufacture;
    [SerializeField]
    private int level;
    [SerializeField]
    private int performanceRatio;
}
