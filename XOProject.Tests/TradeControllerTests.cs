using System;
using System.Threading.Tasks;
using XOProject.Controller;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace XOProject.Tests
{
    public class TradeControllerTests
{
        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();
        private readonly Mock<ITradeRepository> _tradeRepositoryMock = new Mock<ITradeRepository>();
        private readonly Mock<IPortfolioRepository> _portfolioRepositoryMock = new Mock<IPortfolioRepository>();


        private readonly TradeController _tradeController;

        public TradeControllerTests()
        {
            _tradeController = new TradeController(_shareRepositoryMock.Object, _tradeRepositoryMock.Object, _portfolioRepositoryMock.Object);
        }

        [Test]
        public async Task Get_AllTradings_Valid()
        {
            int portfolioId = 1;

            var trade = new[]
            {
                new Trade() { PortfolioId = portfolioId }
            };

            _tradeRepositoryMock.Setup(x => x.GetAllTradings(It.Is<int>(id => id == portfolioId)))
                .Returns<int>(x => Task.FromResult(new List<Trade>(trade)));

            var result = await _tradeController.GetAllTradings(portfolioId);

            Assert.NotNull(result);

            var okResult = result as OkObjectResult;

            var tradeList = okResult.Value as List<Trade>;

            Assert.NotNull(tradeList);

            Assert.Greater(tradeList.Count, 0);

        }

        [Test]
        public async Task Get_GetTradingAnalys_Valid_Values()
        {
            string symbol = "REL";
            var tradeAnalysis = new[]
            {
                new TradeAnalysis() { Sum = 150, Average = 75, Maximum = 100, Minimum = 50, Action = "BUY"},
            };

            _tradeRepositoryMock.Setup(x => x.GetAnalysis(It.Is<string>(s => s.Equals(symbol))))
                .Returns(Task.FromResult(new List<TradeAnalysis>(tradeAnalysis)));

            var result = await _tradeController.GetAnalysis(symbol);

            Assert.NotNull(result);

            var okResult = result as OkObjectResult;

            var tradeAnalysisList = okResult.Value as List<TradeAnalysis>;

            Assert.NotNull(tradeAnalysisList);

            Assert.AreEqual(150, tradeAnalysisList[0].Sum);

        }
    }
}
