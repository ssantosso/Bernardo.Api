using SecurityApi.Domain.Entities;

namespace SecurityApi.Application.Interfaces;

public interface ISecurityPriceService
{
    Task<bool> AddSecurityPrices(string isin);
}