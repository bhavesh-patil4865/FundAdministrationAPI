using FundAdministration.Domain.Entities;
using FundAdministration.Infrastructure.Persistence;
using FundAdministration.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FundAdministration.Tests.Repositories
{
    [TestClass]
    public class InvestorRepositoryTests
    {
        private Mock<AppDbContext> _mockContext;
        private InvestorRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .Options;

            _mockContext = new Mock<AppDbContext>(options);
        }

        [TestMethod]
        public async Task GetAllAsync_Should_Return_All_Investors()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);

            context.Investors.AddRange(
                new Investor
                {
                    InvestorId = Guid.NewGuid(),
                    FullName = "Investor 1",
                    Email = "investor1@test.com"
                },
                new Investor
                {
                    InvestorId = Guid.NewGuid(),
                    FullName = "Investor 2",
                    Email = "investor2@test.com"
                });

            await context.SaveChangesAsync();

            var repository = new InvestorRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsync_Should_Return_Investor_When_Exists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);

            var investorId = Guid.NewGuid();

            var investor = new Investor
            {
                InvestorId = investorId,
                FullName = "John Doe",
                Email = "john@test.com"
            };

            context.Investors.Add(investor);

            await context.SaveChangesAsync();

            var repository = new InvestorRepository(context);

            // Act
            var result = await repository.GetByIdAsync(investorId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("John Doe", result.FullName);
        }

        [TestMethod]
        public async Task AddAsync_Should_Add_Investor()
        {
            // Arrange
            var investor = new Investor
            {
                InvestorId = Guid.NewGuid(),
                FullName = "New Investor",
                Email = "new@test.com"
            };

            var mockSet = new Mock<DbSet<Investor>>();

            _mockContext
                .Setup(x => x.Set<Investor>())
                .Returns(mockSet.Object);

            _repository = new InvestorRepository(_mockContext.Object);

            // Act
            await _repository.AddAsync(investor);

            // Assert
            mockSet.Verify(
                x => x.AddAsync(
                    investor,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public void Update_Should_Update_Investor()
        {
            // Arrange
            var investor = new Investor
            {
                InvestorId = Guid.NewGuid(),
                FullName = "Updated Investor",
                Email = "updated@test.com"
            };

            var mockSet = new Mock<DbSet<Investor>>();

            _mockContext
                .Setup(x => x.Set<Investor>())
                .Returns(mockSet.Object);

            _repository = new InvestorRepository(_mockContext.Object);

            // Act
            _repository.Update(investor);

            // Assert
            mockSet.Verify(
                x => x.Update(investor),
                Times.Once);
        }

        [TestMethod]
        public void Delete_Should_Remove_Investor()
        {
            // Arrange
            var investor = new Investor
            {
                InvestorId = Guid.NewGuid(),
                FullName = "Delete Investor",
                Email = "delete@test.com"
            };

            var mockSet = new Mock<DbSet<Investor>>();

            _mockContext
                .Setup(x => x.Set<Investor>())
                .Returns(mockSet.Object);

            _repository = new InvestorRepository(_mockContext.Object);

            // Act
            _repository.Delete(investor);

            // Assert
            mockSet.Verify(
                x => x.Remove(investor),
                Times.Once);
        }

        [TestMethod]
        public async Task SaveChangesAsync_Should_Call_DbContext_SaveChanges()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Investor>>();

            _mockContext
                .Setup(x => x.Set<Investor>())
                .Returns(mockSet.Object);

            _mockContext
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _repository = new InvestorRepository(_mockContext.Object);

            // Act
            await _repository.SaveChangesAsync();

            // Assert
            _mockContext.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task GetInvestorTransactionsAsync_Should_Return_Transactions()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);

            var investorId = Guid.NewGuid();

            context.Transactions.AddRange(
                new Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    InvestorId = investorId,
                    Amount = 1000
                },
                new Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    InvestorId = investorId,
                    Amount = 2000
                });

            await context.SaveChangesAsync();

            var repository = new InvestorRepository(context);

            // Act
            var result = await repository
                .GetInvestorTransactionsAsync(investorId);

            // Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}