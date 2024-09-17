using SecurityApi.Domain.Entities;

namespace SecurityApi.Infra.Data.Interfaces;
public interface ISecurityPriceRepository
{
    Task AddSecurityPrices(SecurityPrice securityPrice);
    Task<IEnumerable<SecurityPrice>> GetAllPriceByISIN(string isin);
}