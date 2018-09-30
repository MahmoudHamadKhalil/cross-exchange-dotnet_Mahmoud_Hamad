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
    public class ShareControllerTests
    {
        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();

        private readonly ShareController _shareController;

        public ShareControllerTests()
        {
            _shareController = new ShareController(_shareRepositoryMock.Object);
        }

        [Test]
        public async Task Post_ShouldInsertHourlySharePrice()
        {
            var hourRate = new HourlyShareRate
            {
                Symbol = "CBI",
                Rate = 330.0M,
                TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
            };

            // Arrange

            // Act
            var result = await _shareController.Post(hourRate);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }

        [Test]
        public async Task Get_HourlyShare_Greater_Than_Zero()
        {
            string symbol = "CBI";
            var hourRate = new[]
            {
                new HourlyShareRate() { Id = 1, Symbol = symbol, Rate = 330.0M, TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0) }
            };
            
            _shareRepositoryMock.Setup(x => x.GetBySymbol(It.Is<string>(s => s == symbol)))
                .Returns(Task.FromResult(new List<HourlyShareRate>(hourRate)));

            var result = await _shareController.Get(symbol);

            Assert.NotNull(result);

            var okResult = result as OkObjectResult;

            var shareList = okResult.Value as List<HourlyShareRate>;

            Assert.NotNull(shareList);

            Assert.Greater(shareList.Count, 0);


        }

        [Test]
        public async Task Get_latestHourlyShare_Valid_Value()
        {
            string symbol = "REL";
            var hourRate = new[]
            {
                new HourlyShareRate() { Id = 1, Symbol = symbol, Rate = 10, TimeStamp = DateTime.Now.AddDays(-1) },
                new HourlyShareRate() { Id = 1, Symbol = symbol, Rate = 20, TimeStamp = DateTime.Now }
            };
            _shareRepositoryMock.Setup(x => x.GetBySymbol(It.Is<string>(s => s.Equals(symbol))))
                .Returns(Task.FromResult(new List<HourlyShareRate>(hourRate)));

            var result = await _shareController.GetLatestPrice(symbol);

            Assert.NotNull(result);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult.Value);

            Assert.AreEqual(20, okResult.Value);

        }

    }
}
