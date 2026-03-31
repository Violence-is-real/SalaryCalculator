using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalaryCalculator.WPF;

namespace SalaryCalculator.Tests
{
    [TestClass]
    public class SalaryCalculatorTests
    {
        [TestMethod]
        public void Calculate_Assistant_10Hours_13PercentTax_ReturnsCorrectGrossAndTax()
        {
            // Arrange
            var position = Position.Assistant;
            var hours = 10m;
            var taxRate = 13m;

            // Act
            var result = SalaryCalculator.WPF.SalaryCalculator.Calculate(position, hours, taxRate);

            // Assert
            Assert.AreEqual(1500m, result.GrossSalary);
            Assert.AreEqual(195m, result.TaxAmount);
            Assert.AreEqual(1305m, result.NetSalary);
        }

        [TestMethod]
        public void Calculate_Professor_15Hours_0PercentTax_ReturnsGrossAndNoTax()
        {
            // Arrange
            var position = Position.Professor;
            var hours = 15m;
            var taxRate = 0m;

            // Act
            var result = SalaryCalculator.WPF.SalaryCalculator.Calculate(position, hours, taxRate);

            // Assert
            Assert.AreEqual(5250m, result.GrossSalary);
            Assert.AreEqual(0m, result.TaxAmount);
            Assert.AreEqual(5250m, result.NetSalary);
        }

        [TestMethod]
        public void Calculate_ZeroHours_ReturnsZeroGrossAndTax()
        {
            // Arrange
            var position = Position.Assistant;
            var hours = 0m;
            var taxRate = 13m;

            // Act
            var result = SalaryCalculator.WPF.SalaryCalculator.Calculate(position, hours, taxRate);

            // Assert
            Assert.AreEqual(0m, result.GrossSalary);
            Assert.AreEqual(0m, result.TaxAmount);
            Assert.AreEqual(0m, result.NetSalary);
        }

        [TestMethod]
        public void Calculate_NegativeHours_ThrowsArgumentExceptionWithCorrectMessage()
        {
            // Arrange
            var position = Position.Assistant;
            var hours = -5m;
            var taxRate = 13m;

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => SalaryCalculator.WPF.SalaryCalculator.Calculate(position, hours, taxRate));
            Assert.AreEqual("Количество часов не может быть отрицательным.", ex.Message);
        }

        [TestMethod]
        public void Calculate_InvalidTaxRateGreaterThan100_ThrowsArgumentExceptionWithCorrectMessage()
        {
            // Arrange
            var position = Position.Assistant;
            var hours = 10m;
            var taxRate = 150m;

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => SalaryCalculator.WPF.SalaryCalculator.Calculate(position, hours, taxRate));
            Assert.AreEqual("Налоговая ставка должна быть от 0 до 100.", ex.Message);
        }

        [TestMethod]
        public void Calculate_FractionalHours_CalculatesCorrectly()
        {
            // Arrange
            var position = Position.Assistant;
            var hours = 7.5m;
            var taxRate = 13m;

            // Act
            var result = SalaryCalculator.WPF.SalaryCalculator.Calculate(position, hours, taxRate);

            // Assert
            Assert.AreEqual(1125m, result.GrossSalary);
            Assert.AreEqual(146.25m, result.TaxAmount);
            Assert.AreEqual(978.75m, result.NetSalary);
        }
    }
}