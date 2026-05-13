using FundAdministration.Domain.Entities;
using FundAdministration.Infrastructure.Persistence;
using FundAdministration.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.EntityFrameworkCore;

namespace FundAdministration.Tests.Repositories
{
    [TestClass]
    public class FundRepositoryTests
    {
        private Mock<AppDbContext> _mockContext;
        private FundRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .Options;

            _mockContext = new Mock<AppDbContext>(options);

            /*_repository = new FundRepository(_mockContext.Object);*/
        }

        [TestMethod]
        public async Task GetAllAsync_Should_Return_All_Funds()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);

            context.Funds.AddRange(
                new Fund
                {
                    FundId = Guid.NewGuid(),
                    Name = "Fund 1",
                    Currency = "INR"
                },
                new Fund
                {
                    FundId = Guid.NewGuid(),
                    Name = "Fund 2",
                    Currency = "USD"
                });

            await context.SaveChangesAsync();

            var repository = new FundRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsync_Should_Return_Fund_When_Fund_Exists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);

            var fundId = Guid.NewGuid();

            var fund = new Fund
            {
                FundId = fundId,
                Name = "Equity Fund",
                Currency = "INR"
            };

            context.Funds.Add(fund);

            await context.SaveChangesAsync();

            var repository = new FundRepository(context);

            // Act
            var result = await repository.GetByIdAsync(fundId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Equity Fund", result.Name);
        }

        [TestMethod]
        public async Task AddAsync_Should_Add_Fund()
        {
            // Arrange
            var fund = new Fund
            {
                FundId = Guid.NewGuid(),
                Name = "New Fund",
                Currency = "INR"
            };

            var mockSet = new Mock<DbSet<Fund>>();

            _mockContext
                .Setup(x => x.Set<Fund>())
                .Returns(mockSet.Object);

            _repository = new FundRepository(_mockContext.Object);

            // Act
            await _repository.AddAsync(fund);

            // Assert
            mockSet.Verify(
                x => x.AddAsync(fund, CancellationToken.None),
                Times.Once);
        }

        [TestMethod]
        public void Update_Should_Update_Fund()
        {
            // Arrange
            var fund = new Fund
            {
                FundId = Guid.NewGuid(),
                Name = "Updated Fund",
                Currency = "USD"
            };

            var mockSet = new Mock<DbSet<Fund>>();

            _mockContext
                .Setup(x => x.Set<Fund>())
                .Returns(mockSet.Object);

            _repository = new FundRepository(_mockContext.Object);
            // Act
            _repository.Update(fund);

            // Assert
            mockSet.Verify(x => x.Update(fund), Times.Once);
        }

        [TestMethod]
        public void Delete_Should_Remove_Fund()
        {
            // Arrange
            var fund = new Fund
            {
                FundId = Guid.NewGuid(),
                Name = "Delete Fund",
                Currency = "INR"
            };

            var mockSet = new Mock<DbSet<Fund>>();

            _mockContext
                .Setup(x => x.Set<Fund>())
                .Returns(mockSet.Object);

            _repository = new FundRepository(_mockContext.Object);
            // Act
            _repository.Delete(fund);

            // Assert
            mockSet.Verify(x => x.Remove(fund), Times.Once);
        }

        [TestMethod]
        public async Task SaveChangesAsync_Should_Call_DbContext_SaveChanges()
        {
            // Arrange
            _mockContext
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _repository = new FundRepository(_mockContext.Object);
            // Act
            await _repository.SaveChangesAsync();

            // Assert
            _mockContext.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}