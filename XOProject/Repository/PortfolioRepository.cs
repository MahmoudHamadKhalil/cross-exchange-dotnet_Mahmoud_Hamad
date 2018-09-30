using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace XOProject
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(ExchangeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Portfolio>> GetAll()
        {
            return _dbContext.Portfolios.Include(x => x.Trade).ToListAsync();
        }
    }
}