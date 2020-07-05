using System;
using System.Collections.Generic;

public class BuyEventArgs : EventArgs
{
    public List<ICurrency> Currencies { get; set; }

    public BuyEventArgs()
    {

    }
    public BuyEventArgs(List<ICurrency> currencies)
    {
        Currencies = currencies;
    }
}