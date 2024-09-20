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
       
        public async Task<SecurityPrice> GetPriceByISIN(SecurityPrice isin)
        {
            //httpcliente
            // go to https://securities.dataprovider.com/securityprice/{isin} e retorna o price do security
            isin.SetPrice(150.00);
            return isin;
        }
    }
}
