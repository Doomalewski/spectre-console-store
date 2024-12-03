using Sklep_Konsola;
using Sklep_Konsola.AccountRelated;
using Xunit;

namespace Test
{
    public class StoreRepositoryTests
    {
        // Test dodawania konta
        [Fact]
        public void AddAccount_ShouldAddAccount()
        {
            // Arrange
            var store = StoreRepository.GetInstance();
            var account = new Account { Username = "user1", Email = "user@example.com", Password = "password123" };

            // Act
            store.AddAccount(account);

            // Assert
            Assert.Contains(account, store.GetAllAccounts());
        }

        // Test sprawdzania unikalnoœci e-maila
        [Fact]
        public void IsEmailUnique_ShouldReturnTrueForUniqueEmail()
        {
            // Arrange
            var store = StoreRepository.GetInstance();
            var account = new Account { Username = "user1", Email = "user@example.com", Password = "password123" };
            store.AddAccount(account);

            // Act
            var result = store.IsEmailUnique("newuser@example.com");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmailUnique_ShouldReturnFalseForDuplicateEmail()
        {
            // Arrange
            var store = StoreRepository.GetInstance();
            var account = new Account { Username = "user1", Email = "user@example.com", Password = "password123" };
            store.AddAccount(account);

            // Act
            var result = store.IsEmailUnique("user@example.com");

            // Assert
            Assert.False(result);
        }


        // Test dodawania marki
        [Fact]
        public void AddBrand_ShouldAddBrand()
        {
            // Arrange
            var store = StoreRepository.GetInstance();
            var brand = new Brand("Brand1", 2000, "A popular brand");

            // Act
            store.AddBrand(brand);

            // Assert
            Assert.Contains(brand, store.GetAllBrands());
        }
    }
}
