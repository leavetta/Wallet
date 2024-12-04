using Moq;
using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services;

namespace WalletAspNetCore.Tests.ServicesTests
{
    public class TransactionServiceTest
    {
        private readonly Mock<ITransactionRepository> _moqTransactionRepository;
        private readonly Mock<IUserRepository> _moqUserRepository;
        private readonly Mock<ICategoryRepository> _moqCategoryRepository;
        private readonly Mock<IBalanceRepository> _moqBalanceRepository;
        private readonly Transaction _transaction;
        public TransactionServiceTest()
        {
            _moqTransactionRepository = new Mock<ITransactionRepository>();
            _moqUserRepository = new Mock<IUserRepository>();
            _moqCategoryRepository = new Mock<ICategoryRepository>();
            _moqBalanceRepository = new Mock<IBalanceRepository>();
            _transaction = new Transaction { Id = new Guid("498f2feb-0615-40be-a2f1-99a3b0acf88e"), Amount = 10, OperationDate = new DateTime(2024, 07, 22, 10, 30, 0) };
        }


        [Fact]
        public async Task CreateAsync_ShouldReturnTransactionId_WhenTrasactionCreated()
        {
            var user = GetUser();
            var category = GetCategory();
            var transaction = GetTransaction();
            var balance = GetBalance();
            decimal amount = 10;
            _moqUserRepository.Setup(x => x.GetByIdAsync(user.Id)).ReturnsAsync(user);
            _moqCategoryRepository.Setup(x => x.GetByIdAsync(category.Id)).ReturnsAsync(category);

            _moqTransactionRepository.Setup(x => x.CreateAsync(user, category, transaction.Amount)).ReturnsAsync(transaction);
            _moqBalanceRepository.Setup(x => x.ApplyTransactionAsync(user, transaction)).ReturnsAsync(balance.Id);

            var transactionService = new TransactionService(
                                            _moqTransactionRepository.Object, 
                                            _moqUserRepository.Object, 
                                            _moqCategoryRepository.Object,
                                            _moqBalanceRepository.Object);

            var transactionIdResult= await transactionService.CreateAsync(user.Id, category.Id, amount);

            Assert.Equal(_transaction.Id, transactionIdResult);
        }

        //[Fact]
        //public async Task GetTransactionsAsync_ShouldReturnCountAllTransactions_WhenStartDateEqualNull(Guid userId, DateTime? startDate, DateTime? endDate)
        //{
        //    var user = GetUser();
        //    var category = GetCategory();
        //    var transaction = GetTransaction();
        //    _moqTransactionRepository.Setup(x => x.GetAllAsync(user.Id, )).ReturnsAsync(transaction);
            

        //    return await _transactionRepository.GetTransactionsOfRangeDateAsync(userId, notNullStartDate.AddHours(7), notNullEndDate.AddHours(7));
        //}

        //[Fact]
        //public async Task GetTransactionsAsync_ShouldReturnTransactions_WhenEndDateEqualNull(Guid userId, DateTime? startDate, DateTime? endDate)
        //{
        //    if (startDate == null)
        //    {
        //        return await _transactionRepository.GetAllAsync(userId);
        //    }
        //    DateTime notNullStartDate = (DateTime)startDate;
        //    if (endDate == null)
        //    {

        //        return await _transactionRepository.GetTransactionsOfRangeDateAsync(userId, notNullStartDate.AddHours(7), notNullStartDate.AddHours(7));
        //    }
        //    DateTime notNullEndDate = (DateTime)endDate;

        //    return await _transactionRepository.GetTransactionsOfRangeDateAsync(userId, notNullStartDate.AddHours(7), notNullEndDate.AddHours(7));
        //}

        //[Fact]
        //public async Task GetTransactionsAsync_ShouldReturnTransactions_WhenDatesNotNull(Guid userId, DateTime? startDate, DateTime? endDate)
        //{
        //    if (startDate == null)
        //    {
        //        return await _transactionRepository.GetAllAsync(userId);
        //    }
        //    DateTime notNullStartDate = (DateTime)startDate;
        //    if (endDate == null)
        //    {

        //        return await _transactionRepository.GetTransactionsOfRangeDateAsync(userId, notNullStartDate.AddHours(7), notNullStartDate.AddHours(7));
        //    }
        //    DateTime notNullEndDate = (DateTime)endDate;

        //    return await _transactionRepository.GetTransactionsOfRangeDateAsync(userId, notNullStartDate.AddHours(7), notNullEndDate.AddHours(7));
        //}

        //Used db
        //[Fact]
        //public async Task GetReportSelectedKindTransactionsAsync_SouldReturnDictionaryWithCatedoriesAndSums_WhenCategoriesTaken()
        //{
        //    var user = GetUser();
        //    var category = GetCategory();
        //    bool selectedKey = false;

        //    _moqCategoryRepository.Setup(x => x.GetSelectedCategoriesAsync(category.Id, selectedKey)).ReturnsAsync(category);

        //    var transactionService = new TransactionService(
        //                                    _moqTransactionRepository.Object,
        //                                    _moqUserRepository.Object,
        //                                    _moqCategoryRepository.Object,
        //                                    _moqBalanceRepository.Object);

        //    var transactionIdResult = await transactionService.GetReportSelectedKindTransactionsAsync(user.Id, selectedKey);

        //    Assert.Equal(_transaction.Id, transactionIdResult);

        //    Dictionary<Category, decimal> categoriesAmount = new Dictionary<Category, decimal>();

        //    var categories = await _categoryRepository.GetSelectedCategoriesAsync(userId, selectedKey);

        //    foreach (var category in categories)
        //    {
        //        decimal amount = Math.Abs(category.Transactions.Sum(t => t.Amount));
        //        categoriesAmount[category] = amount;
        //    }

        //    return categoriesAmount;
        //}

        public User GetUser()
        {
            return new User { Id = new Guid("c4026057-5942-4050-9778-053628e56931") };
        }

        public Category GetCategory()
        {
            return new Category { Id = new Guid("ca88c438-9edb-4268-a606-186db3dbb547"), Name = "Супермаркеты", IsIncome = false };
        }

        public Transaction GetTransaction()
        {
            return new Transaction { Id = new Guid("498f2feb-0615-40be-a2f1-99a3b0acf88e"), Amount = 10, OperationDate = new DateTime(2024, 07, 22, 10, 30, 0) };
        }

        public Balance GetBalance()
        {
            return new Balance { Id = new Guid("d7bdc041-0aa0-412a-92e0-c93090fb79c5"), CurrentAmount = 10 };
        }

        
    }
}
