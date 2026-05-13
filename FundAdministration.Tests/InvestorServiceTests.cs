using AutoMapper;
using FundAdministration.Application.DTOs;
using FundAdministration.Application.Services;
using FundAdministration.Domain.Entities;
using FundAdministration.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FundAdministration.Tests.Services
{
    [TestClass]
    public class InvestorServiceTests
    {
        private Mock<IInvestorRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private InvestorService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IInvestorRepository>();

            _mockMapper = new Mock<IMapper>();

            _service = new InvestorService(
                _mockRepository.Object,
                _mockMapper.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_Should_Return_All_Investors()
        {
            // Arrange
            var investors = new List<Investor>
            {
                new Investor
                {
                    InvestorId = Guid.NewGuid(),
                    FullName = "John Doe",
                    Email = "john@test.com"
                }
            };

            var investorDtos = new List<InvestorDto>
            {
                new InvestorDto
                {
                    InvestorId = investors[0].InvestorId,
                    FullName = "John Doe",
                    Email = "john@test.com"
                }
            };

            _mockRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(investors);

            _mockMapper
                .Setup(x => x.Map<IEnumerable<InvestorDto>>(investors))
                .Returns(investorDtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsync_Should_Return_Investor()
        {
            // Arrange
            var investorId = Guid.NewGuid();

            var investor = new Investor
            {
                InvestorId = investorId,
                FullName = "John Doe",
                Email = "john@test.com"
            };

            var investorDto = new InvestorDto
            {
                InvestorId = investorId,
                FullName = "John Doe",
                Email = "john@test.com"
            };

            _mockRepository
                .Setup(x => x.GetByIdAsync(investorId))
                .ReturnsAsync(investor);

            _mockMapper
                .Setup(x => x.Map<InvestorDto>(investor))
                .Returns(investorDto);

            // Act
            var result = await _service.GetByIdAsync(investorId);

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual("John Doe", result.FullName);
        }

        [TestMethod]
        public async Task CreateAsync_Should_Add_Investor()
        {
            // Arrange
            var dto = new CreateInvestorDto
            {
                FullName = "New Investor",
                Email = "new@test.com"
            };

            var investor = new Investor
            {
                FullName = dto.FullName,
                Email = dto.Email
            };

            _mockMapper
                .Setup(x => x.Map<Investor>(dto))
                .Returns(investor);

            // Act
            await _service.CreateAsync(dto);

            // Assert
            _mockRepository.Verify(
                x => x.AddAsync(It.IsAny<Investor>()),
                Times.Once);

            _mockRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_Should_Delete_Investor()
        {
            // Arrange
            var investorId = Guid.NewGuid();

            var investor = new Investor
            {
                InvestorId = investorId,
                FullName = "Delete Investor",
                Email = "delete@test.com"
            };

            _mockRepository
                .Setup(x => x.GetByIdAsync(investorId))
                .ReturnsAsync(investor);

            // Act
            await _service.DeleteAsync(investorId);

            // Assert
            _mockRepository.Verify(
                x => x.Delete(investor),
                Times.Once);

            _mockRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_Should_Throw_Exception_When_Investor_Not_Found()
        {
            // Arrange
            var investorId = Guid.NewGuid();

            _mockRepository
                .Setup(x => x.GetByIdAsync(investorId))
                .ReturnsAsync((Investor)null);

            // Act
            try
            {
                await _service.DeleteAsync(investorId);

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("Investor not found", ex.Message);
            }
        }

        [TestMethod]
        public async Task GetInvestorTransactionsAsync_Should_Return_Transactions()
        {
            // Arrange
            var investorId = Guid.NewGuid();

            var transactions = new List<Transaction>
            {
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
                }
            };

            var transactionDtos = new List<TransactionDto>
            {
                new TransactionDto
                {
                    TransactionId = transactions[0].TransactionId,
                    Amount = 1000
                },
                new TransactionDto
                {
                    TransactionId = transactions[1].TransactionId,
                    Amount = 2000
                }
            };

            _mockRepository
                .Setup(x => x.GetInvestorTransactionsAsync(investorId))
                .ReturnsAsync(transactions);

            _mockMapper
                .Setup(x =>
                    x.Map<IEnumerable<TransactionDto>>(transactions))
                .Returns(transactionDtos);

            // Act
            var result = await _service
                .GetInvestorTransactionsAsync(investorId);

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Count());
        }
    }
}