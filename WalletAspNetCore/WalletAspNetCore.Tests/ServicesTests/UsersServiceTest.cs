using Moq;
using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services;

namespace WalletAspNetCore.Tests.ServicesTests
{
    public class UsersServiceTest
    {

        private readonly Mock<IUserRepository> _moqUserRepository;
        private readonly User _user;
        public UsersServiceTest() 
        {
            _moqUserRepository = new Mock<IUserRepository>();
            _user = new User { Id = new Guid("c4026057-5942-4050-9778-053628e56931") };
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserFoundById()
        {
            Guid userId = new Guid("c4026057-5942-4050-9778-053628e56931");
            User user = new User { Id = userId };
            _moqUserRepository
                .Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(user);

            var usersService = new UsersService(_moqUserRepository.Object);
            var resultUser =  await usersService.GetUserByIdAsync(userId);
            Assert.Equal(_user.Id, resultUser.Id);
        }
    }
}