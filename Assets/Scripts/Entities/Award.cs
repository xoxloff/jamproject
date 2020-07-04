using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour
{
    [SerializeField]
    private ScientistCurrency scientistCurrency;
    [SerializeField]
    private List<Scientist> availableScientists;
    [SerializeField]
    private List<Scientist> droppedScientists;
    [SerializeField]
    private ShortBigInteger maxScientistCurrency;
    [SerializeField]
    private ShortBigInteger minScientistCurrency;
    [SerializeField]
    private int maxScientistsNumber;
    [SerializeField]
    private int minScientistsNumber;
}
