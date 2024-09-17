using SecurityApi.Application.Interfaces;
using SecurityApi.Infra.Data.Interfaces;

namespace SecurityApi.Application.Services;
public class SecurityPriceService : ISecurityPriceService
{
    private readonly ISecurityPriceRepository _repo;
    public SecurityPriceService(ISecurityPriceRepository repo)
    {
        _repo = repo;
    }
    public async Task<bool> AddSecurityPrices(string isin)
    {
        try
        {
            if (IsinValid(isin))
            {
                var prices = await _repo.GetAllPriceByISIN(isin);
                foreach (var price in prices)
                {
                    if (IsinValid(price.ISIN))
                        await _repo.AddSecurityPrices(price);
                }
            }
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    private bool IsinValid(string iSIN)
    {
        if (!string.IsNullOrEmpty(iSIN) && iSIN.Length == 12)
            return true;

        return false;

    }
}