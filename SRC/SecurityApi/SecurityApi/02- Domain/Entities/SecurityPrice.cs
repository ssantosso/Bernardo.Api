namespace SecurityApi.Domain.Entities;
public class SecurityPrice
{
    public string ISIN { get; private set; }
    public double Price { get; private set; }

    public SecurityPrice()
    {
        
    }
    public SecurityPrice(string iSIN, double price)
    {
        ISIN = iSIN;
        Price = price;
    }
}