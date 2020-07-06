using System;

public class AddCurrencyEventArgs : EventArgs
{
    public ShortBigInteger AddingCurrency { get; set; }

    public AddCurrencyEventArgs()
    {

    }
    public AddCurrencyEventArgs(ShortBigInteger addingcurrency)
    {
        this.AddingCurrency = addingcurrency;
    }
}
