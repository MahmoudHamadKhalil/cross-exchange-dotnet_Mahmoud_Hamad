using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace XOProject.Tests
{
    public class ContextTests
{
        public void exchangeContextInit(ExchangeContext context)
        {
            context.Portfolios.Add(new Portfolio { Name = "John Doe", Trade = null });
            context.Trades.Add(new Trade { Symbol = "REL", NoOfShares = 50, Price = 5000, PortfolioId = 1, Action = "BUY" });
            context.Trades.Add(new Trade { Symbol = "REL", NoOfShares = 100, Price = 10000, PortfolioId = 1, Action = "BUY" });
            context.SaveChanges();
        }

        [Test]
        public async Task Get_AllPortfolios_Valid_Count()
        {
            var builder = new DbContextOptionsBuilder<ExchangeContext>();
            builder.UseInMemoryDatabase(databaseName: "TestPortfolio");

            var context = new ExchangeContext(builder.Options);
            exchangeContextInit(context);

            var repo = new PortfolioRepository(context);


            var result = await repo.GetAll() as List<Portfolio>; ;
            var count = result.Count;

            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task Get_AllTrades_Valid_Count()
        {
            int portfolioID = 1;
            var builder = new DbContextOptionsBuilder<ExchangeContext>();
            builder.UseInMemoryDatabase(databaseName: "TestTrade1");

            var context = new ExchangeContext(builder.Options);
            exchangeContextInit(context);

            var repo = new TradeRepository(context);

            var result = await repo.GetAllTradings(portfolioID) as List<Trade>; ;
            var count = result.Count;

            Assert.AreEqual(2, count);
        }

        [Test]
        public async Task Get_Trades_By_Symbol_Valid_Count()
        {
            string symbol = "REL";
            var builder = new DbContextOptionsBuilder<ExchangeContext>();
            builder.UseInMemoryDatabase(databaseName: "TestTrade2");

            var context = new ExchangeContext(builder.Options);
            exchangeContextInit(context);

            var repo = new TradeRepository(context);

            var result = await repo.GetBySymbol(symbol) as List<Trade>; ;
            var count = result.Count;

            Assert.AreEqual(2, count);
        }

        [Test]
        public async Task Get_Analysis_Valid_Maximum()
        {
            string symbol = "REL";
            var builder = new DbContextOptionsBuilder<ExchangeContext>();
            builder.UseInMemoryDatabase(databaseName: "TestTrade3");

            var context = new ExchangeContext(builder.Options);
            exchangeContextInit(context);

            var repo = new TradeRepository(context);

            var result = await repo.GetAnalysis(symbol) as List<TradeAnalysis>; ;
            
            Assert.AreEqual(100, result[0].Maximum);
        }
    }
}
