using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private int rank;
    [SerializeField]
    private int currentRankValue;
    [SerializeField]
    private int requiredRankValue;
    [SerializeField]
    private MainCurrency mainCurrency;
    [SerializeField]
    private ScientistCurrency scientistCurrency;
    [SerializeField]
    private List<Scientist> scientists;
    [SerializeField]
    private List<Factory> factories;
    [SerializeField]
    private List<Quest> quests;
}
