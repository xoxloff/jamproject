using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private List<Manufacture> manufactures;
    [SerializeField]
    private ICurrency FactoryCurrency;
    [SerializeField]
    private enum purchaseMode { x1,x10,x100,max};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
