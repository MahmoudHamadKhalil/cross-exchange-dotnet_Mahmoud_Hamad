using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace XOProject.Controller
{
    [Route("api/Trade/")]
    public class TradeController : ControllerBase
    {
        private IShareRepository _shareRepository { get; set; }
        private ITradeRepository _tradeRepository { get; set; }
        private IPortfolioRepository _portfolioRepository { get; set; }

        public TradeController(IShareRepository shareRepository, ITradeRepository tradeRepository, IPortfolioRepository portfolioRepository)
        {
            _shareRepository = shareRepository;
            _tradeRepository = tradeRepository;
            _portfolioRepository = portfolioRepository;
        }


        [HttpGet("{portfolioid}")]
        public async Task<IActionResult> GetAllTradings([FromRoute]int portFolioid)
        {
            var trade = await _tradeRepository.GetAllTradings(portFolioid);
            return Ok(trade);
        }


        /// <summary>
        /// For a given symbol of share, get the statistics for that particular share calculating the maximum, minimum, 
        /// average and Sum of all the trades for that share individually grouped into Buy trade and Sell trade.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>

        [HttpGet("Analysis/{symbol}")]
        public async Task<IActionResult> GetAnalysis([FromRoute]string symbol)
        {
            var result = await _tradeRepository.GetAnalysis(symbol);
            return Ok(result);
        }


    }
}
