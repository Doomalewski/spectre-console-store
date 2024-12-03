using Xunit;
using Sklep_Konsola;
using Sklep_Konsola.AccountRelated;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sklep_Konsola.Tests
{
    public class StoreRepositoryTests
    {
        private readonly StoreRepository _storeRepository;

        public StoreRepositoryTests()
        {
            _storeRepository = StoreRepository.GetInstance();
        }

        [Fact]
        public void TestSingletonPattern_ReturnsSameInstance()
        {
            // Arrange
            var firstInstance = StoreRepository.GetInstance();
            var secondInstance = StoreRepository.GetInstance();

            // Assert
            Assert.Same(firstInstance, secondInstance);
        }

        [Fact]
        public void TestAddToStock_AddsProductToList() 
        {
            // Arrange
            var product = new Product("Test Product", new Brand("nazwa",1990,"opis"), 100, new Tax("VAT",23), 10, "Sample description");

            // Acts
            _storeRepository.AddToStock(product);

            // Assert
            Assert.Contains(product, _storeRepository.Products);
        }

        [Fact]
        public void TestAddAccount_AddsAccountToList()
        {
            // Arrange
            var account = new Account("testuser", "test@example.com", "Password123");

            // Act
            _storeRepository.AddAccount(account);

            // Assert
            Assert.Contains(account, _storeRepository.Accounts);
        }

        [Fact]
        public void TestAddAccount_EmailShouldBeUnique()
        {
            // Arrange
            var account = new Account("testuser", "test@example.com", "Password123");
            _storeRepository.AddAccount(account);

            // Act
            var result = _storeRepository.IsEmailUnique("test@example.com");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TestAddTax_AddsTaxToList()
        {
            // Arrange
            var tax = new Tax("VAT",23);

            // Act
            _storeRepository.AddTax(tax);

            // Assert
            Assert.Contains(tax, _storeRepository.Taxes);
        }

        [Fact]
        public void TestGetCurrencyByName_ReturnsCorrectCurrency()
        {
            // Arrange
            var currency = new Currency { Name = "USD", Symbol = "$" };
            _storeRepository.AddCurrency(currency);

            // Act
            var result = _storeRepository.GetCurrencyByName("USD");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("USD", result.Name);
        }

        [Fact]
        public void TestGetCurrencyByName_ReturnsNullForNonExistingCurrency()
        {
            // Act
            var result = _storeRepository.GetCurrencyByName("SHT");

            // Assert
            Assert.Null(result);
        }
    }
}
