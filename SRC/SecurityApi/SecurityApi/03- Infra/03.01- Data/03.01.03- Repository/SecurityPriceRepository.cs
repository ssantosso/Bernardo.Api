using SecurityApi.Domain.Entities;
using SecurityApi.Infra.Data.Interfaces;

namespace SecurityApi.Infra.Data.Repository
{
    public class SecurityPriceRepository : ISecurityPriceRepository
    {
        public SecurityPriceRepository()
        {
            //context xyz
        }
        public async Task AddSecurityPrices(SecurityPrice securityPrice)
        {
            //_context.DbSet.Add(securityPrice);
        }

        public async Task<IEnumerable<SecurityPrice>> GetAllPriceByISIN(string isin)
        {
            // go to https://securities.dataprovider.com/securityprice/{isin} e retorna a lista
            //httpcliente _clientSecurityProvader.List
            var result = new List<SecurityPrice>();
            result.Add(new SecurityPrice(isin, 150.00));
            result.Add(new SecurityPrice("012345678912", 170.00));
            return result;
        }
    }
}
