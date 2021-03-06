﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace XOProject
{
    public interface ITradeRepository : IGenericRepository<Trade>
    {
        Task<List<Trade>> GetAllTradings(int portFolioId);

        Task<List<Trade>> GetBySymbol(string symbol);

        Task<List<TradeAnalysis>> GetAnalysis(string symbol);
    }
}