using AutoMapper;
using FundAdministration.Application.DTOs;
using FundAdministration.Application.Services;
using FundAdministration.Domain.Entities;
using FundAdministration.Domain.Interfaces;
using Humanizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FundAdministration.Tests.Services
{
    [TestClass]
    public class FundServiceTests
    {
        private Mock<IFundRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ITransactionRepository> _mockTransactionRepository;
        private Mock<IInvestorRepository> _mockInvestorRepository;
        private FundService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IFundRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockInvestorRepository = new Mock<IInvestorRepository>();

            // Provide safe defaults so tests that don't explicitly setup these calls won't fail.
            _mockTransactionRepository
                .Setup(x => x.GetTransactionsByFundIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<Transaction>());

            _mockInvestorRepository
                .Setup(x => x.GetInvestorCountByFundAsync(It.IsAny<Guid>()))
                .ReturnsAsync(0);

            _service = new FundService(
                _mockRepository.Object,
                _mockMapper.Object,
                _mockTransactionRepository.Object,
                _mockInvestorRepository.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_Should_Return_All_Funds()
        {
            // Arrange
            var funds = new List<Fund>
            {
                new Fund
                {
                    FundId = Guid.NewGuid(),
                    Name = "Fund 1",
                    Currency = "INR"
                }
            };

            var fundDtos = new List<FundDto>
            {
                new FundDto
                {
                    FundId = funds[0].FundId,
                    Name = "Fund 1",
                    Currency = "INR"
                }
            };

            _mockRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(funds);

            _mockMapper
                .Setup(x => x.Map<IEnumerable<FundDto>>(funds))
                .Returns(fundDtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsync_Should_Return_Fund()
        {
            // Arrange
            var fundId = Guid.NewGuid();

            var fund = new Fund
            {
                FundId = fundId,
                Name = "Equity Fund",
                Currency = "USD"
            };

            var fundDto = new FundDto
            {
                FundId = fundId,
                Name = "Equity Fund",
                Currency = "USD"
            };

            _mockRepository
                .Setup(x => x.GetByIdAsync(fundId))
                .ReturnsAsync(fund);

            _mockMapper
                .Setup(x => x.Map<FundDto>(fund))
                .Returns(fundDto);

            // Act
            var result = await _service.GetByIdAsync(fundId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Equity Fund", result.Name);
        }

        [TestMethod]
        public async Task CreateAsync_Should_Add_Fund()
        {
            // Arrange
            var dto = new CreateFundDto
            {
                Name = "New Fund",
                Currency = "INR",
                LaunchDate = DateTime.UtcNow
            };

            var fund = new Fund
            {
                Name = dto.Name,
                Currency = dto.Currency,
                LaunchDate = dto.LaunchDate
            };

            _mockMapper
                .Setup(x => x.Map<Fund>(dto))
                .Returns(fund);

            // Act
            await _service.CreateAsync(dto);

            // Assert
            _mockRepository.Verify(
                x => x.AddAsync(It.IsAny<Fund>()),
                Times.Once);

            _mockRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_Should_Update_Fund()
        {
            // Arrange
            var fundId = Guid.NewGuid();

            var dto = new UpdateFundDto
            {
                Name = "Updated Fund",
                Currency = "USD",
                LaunchDate = DateTime.UtcNow
            };

            var existingFund = new Fund
            {
                FundId = fundId,
                Name = "Old Fund",
                Currency = "INR"
            };

            _mockRepository
                .Setup(x => x.GetByIdAsync(fundId))
                .ReturnsAsync(existingFund);

            // Act
            await _service.UpdateAsync(fundId, dto);

            // Assert
            Assert.AreEqual("Updated Fund", existingFund.Name);

            _mockRepository.Verify(
                x => x.Update(existingFund),
                Times.Once);

            _mockRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_Should_Throw_Exception_When_Fund_Not_Found()
        {
            // Arrange
            var fundId = Guid.NewGuid();

            var dto = new UpdateFundDto
            {
                Name = "Updated Fund",
                Currency = "USD"
            };

            _mockRepository
                .Setup(x => x.GetByIdAsync(fundId))
                .ReturnsAsync((Fund)null);

            // Act & Assert
            

            await Assert.ThrowsExactlyAsync<Exception>(
                  () => _service.UpdateAsync(fundId, dto));
        }

        [TestMethod]
        public async Task DeleteAsync_Should_Delete_Fund()
        {
            // Arrange
            var fundId = Guid.NewGuid();

            var fund = new Fund
            {
                FundId = fundId,
                Name = "Delete Fund",
                Currency = "INR"
            };

            _mockRepository
                .Setup(x => x.GetByIdAsync(fundId))
                .ReturnsAsync(fund);

            // Act
            await _service.DeleteAsync(fundId);

            // Assert
            _mockRepository.Verify(
                x => x.Delete(fund),
                Times.Once);

            _mockRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_Should_Throw_Exception_When_Fund_Not_Found()
        {
            // Arrange
            var fundId = Guid.NewGuid();

            _mockRepository
                .Setup(x => x.GetByIdAsync(fundId))
                .ReturnsAsync((Fund)null);

            // Act & Assert
            
            await Assert.ThrowsExactlyAsync<Exception>(
                  () => _service.DeleteAsync(fundId));
        }

        [TestMethod]
        public async Task GetFundSummaryAsync_Should_Return_Summary()
        {
            // Arrange
            var fundId = Guid.NewGuid();

            var fund = new Fund
            {
                FundId = fundId,
                Name = "Summary Fund",
                Currency = "USD"
            };

            _mockRepository
                .Setup(x => x.GetByIdAsync(fundId))
                .ReturnsAsync(fund);

            // Act
            var result = await _service.GetFundSummaryAsync(fundId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fundId, result.FundId);
        }

        [TestMethod]
        public async Task GetFundSummaryAsync_Should_Throw_Exception_When_Fund_Not_Found()
        {
            // Arrange
            var fundId = Guid.NewGuid();

            _mockRepository
                .Setup(x => x.GetByIdAsync(fundId))
                .ReturnsAsync((Fund)null);

            // Act & Assert
            
            await Assert.ThrowsExactlyAsync<Exception>(
                  () => _service.GetFundSummaryAsync(fundId));

        }
    }
}