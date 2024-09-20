using SecurityApi.Application.Interfaces;
using SecurityApi.Domain.Entities;
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
                var _security = new SecurityPrice(item);
                if (_security.IsValid())
                {
                    var _isinprice = await _repo.GetPriceByISIN(_security);
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
}