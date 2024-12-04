using Moq;
using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services;

namespace WalletAspNetCore.Tests.ServicesTests
{
    public class BalancesServiceTest
    {
        private readonly Mock<IBalanceRepository> _moqBalanceRepository;
        private readonly Balance _balance;
        public BalancesServiceTest()
        {
            _moqBalanceRepository = new Mock<IBalanceRepository>();
            _balance = new Balance { Id = new Guid("d7bdc041-0aa0-412a-92e0-c93090fb79c5"), CurrentAmount = 10 };
        }

        [Fact]
        public async Task GetByUserIdAsync_ShouldReturnBalanceObject_WhenBalanceExists()
        {
            Guid userId = new Guid("c4026057-5942-4050-9778-053628e56931");
            Balance balance = new Balance { Id = new Guid("d7bdc041-0aa0-412a-92e0-c93090fb79c5"), CurrentAmount = 10 };

            _moqBalanceRepository.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(balance);

            var balanceService = new BalancesService(_moqBalanceRepository.Object);


            var resultBalance = await balanceService.GetByUserIdAsync(userId);

            Assert.Equal(_balance.Id, resultBalance.Id);
            Assert.Equal(_balance.CurrentAmount, resultBalance.CurrentAmount);
        }

        [Fact]
        public async Task UpdateBalanceAsync_ShouldReturnBalanceId_WhenBalanceExistsAndModified()
        {
            Guid userId = new Guid("c4026057-5942-4050-9778-053628e56931");
            decimal newAmount = 11;
            _moqBalanceRepository.Setup(x => x.UpdateAsync(userId, newAmount)).ReturnsAsync(new Guid("d7bdc041-0aa0-412a-92e0-c93090fb79c5"));

            var balanceService = new BalancesService(_moqBalanceRepository.Object);


            var resultBalanceId = await balanceService.UpdateBalanceAsync(userId, newAmount);

            Assert.Equal(_balance.Id, resultBalanceId);
        }

        [Fact]
        public async Task UpdateBalanceAsync_ShouldReturnEmptyBalance_WhenBalanceNotExists()
        {
            Guid userId = new Guid("d7bdc041-0aa0-412a-92e0-c93090fb79c5");
            decimal newAmount = 11;
            Guid? returnsGuid = null;
            _moqBalanceRepository.Setup(x => x.UpdateAsync(userId, newAmount)).ReturnsAsync(returnsGuid);

            var balanceService = new BalancesService(_moqBalanceRepository.Object);


            var resultBalanceId = await balanceService.UpdateBalanceAsync(userId, newAmount);

            Assert.Null(resultBalanceId);
        }
    }
}
