using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XOProject
{
    public class TradeRepository : GenericRepository<Trade>, ITradeRepository
    {
        public TradeRepository(ExchangeContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Trade>> GetAllTradings(int portFolioId)
        {
            return Query().Where(x => x.PortfolioId.Equals(portFolioId)).ToListAsync();
        }

        public Task<List<Trade>> GetBySymbol(string symbol)
        {
            return Query().Where(x => x.Symbol.Equals(symbol)).ToListAsync();
        }

        public Task<List<TradeAnalysis>> GetAnalysis(string symbol)
        {
            return Query().Where(x => x.Symbol.Equals(symbol)).GroupBy(x => x.Action).Select(x => new TradeAnalysis
            {
                Sum = x.Sum(z => z.NoOfShares),
                Average = x.Average(z => z.NoOfShares),
                Maximum = x.Max(z => z.NoOfShares),
                Minimum = x.Min(z => z.NoOfShares),
                Action = x.Key
            }).ToListAsync();
        }
    }
}