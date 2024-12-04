using Moq;
using System.Xml.Linq;
using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services;

namespace WalletAspNetCore.Tests.ServicesTests
{
    public class CategoriesServiceTest
    {
        private readonly Mock<ICategoryRepository> _moqCategoryRepository;
        private readonly Category _category;
        public CategoriesServiceTest()
        {
            _moqCategoryRepository = new Mock<ICategoryRepository>();
            _category = new Category { Id = new Guid("ca88c438-9edb-4268-a606-186db3dbb547"), Name = "Супермаркеты", IsIncome = false };
        }

        [Fact]
        public async Task CreateCategoryAsync_ShouldReturnCategoryId_WhenCategoryCreated()
        {
            string categoryName = "Супермаркеты";
            bool isIncome = false;
            Category category = SetCategory();
            _moqCategoryRepository.Setup(x => x.CreateAsync(categoryName, isIncome)).ReturnsAsync(category);

            var categoryServices = new CategoriesService(_moqCategoryRepository.Object);


            var categoryIdResult = await categoryServices.CreateCategoryAsync(categoryName, isIncome);

            Assert.Equal(_category.Id, categoryIdResult);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ShouldReturnCategory_WhenCategoryExists()
        {
            Guid categoryId = new Guid("ca88c438-9edb-4268-a606-186db3dbb547");

            Category category = SetCategory();
            _moqCategoryRepository.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(category);

            var categoryServices = new CategoriesService(_moqCategoryRepository.Object);


            var categoryResult = await categoryServices.GetCategoryByIdAsync(categoryId);

            Assert.Equal(_category.Id, categoryResult.Id);
            Assert.Equal(_category.Name, categoryResult.Name);
            Assert.Equal(_category.IsIncome, categoryResult.IsIncome);
        }

        [Fact]
        public async Task GetSelectedCategoriesAsync_ShouldReturnListCategories_WhenCategoryBelongsToTheKey()
        {
            bool selectedKey = false;

            _moqCategoryRepository.Setup(x => x.GetCategoriesAsync(selectedKey)).ReturnsAsync(ReturnListCategories());

            var categoryServices = new CategoriesService(_moqCategoryRepository.Object);


            var categoriesListResult = await categoryServices.GetSelectedCategoriesAsync(selectedKey);

            var expectedList = ReturnListCategories();
            for(int i = 0; i < expectedList.Count; i ++) 
            {
                Assert.Equal(expectedList[i].Id, categoriesListResult[i].Id);
                Assert.Equal(expectedList[i].Name, categoriesListResult[i].Name);
                Assert.Equal(expectedList[i].IsIncome, categoriesListResult[i].IsIncome);

            }
        }

        
        public Category SetCategory()
        {
            return new Category { Id = new Guid("ca88c438-9edb-4268-a606-186db3dbb547"), Name = "Супермаркеты", IsIncome = false };
        }

        public List<Category> ReturnListCategories()
        {
            return new List<Category>
            {
                new Category { Id = new Guid("ca88c438-9edb-4268-a606-186db3dbb547"), Name = "Супермаркеты", IsIncome = false },
                new Category { Id = new Guid("ca88c438-9edb-4268-a606-186db3dbb548"), Name = "Кафе", IsIncome = false },
                new Category { Id = new Guid("ca88c438-9edb-4268-a606-186db3dbb549"), Name = "Подарки", IsIncome = false },
            };
        }
    }
}
