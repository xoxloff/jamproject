using System;

public class ManufactureEventArgs: EventArgs
{
    public ShortBigInteger addingProductsNumber { get; set; }

    public ManufactureEventArgs()
    {
        
    }
    public ManufactureEventArgs(ShortBigInteger addingProductsNumber)
    {
        this.addingProductsNumber = addingProductsNumber;
    }
}