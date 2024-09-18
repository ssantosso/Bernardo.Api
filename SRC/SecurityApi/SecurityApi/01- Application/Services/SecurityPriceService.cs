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
    public async Task<bool> AddSecurityPrices(IEnumerable<string> isins)
    {
        try
        {
            foreach (var item in isins)
            {
                if (IsinValid(item))
                {
                    var _isinprice = await _repo.GetPriceByISIN(item);
                    await _repo.AddSecurityPrices(_isinprice);
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