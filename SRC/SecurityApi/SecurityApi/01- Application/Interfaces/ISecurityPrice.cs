namespace SecurityApi.Application.Interfaces;

public interface ISecurityPriceService
{
    Task<bool> AddSecurityPrices(IEnumerable<string> isin);
}