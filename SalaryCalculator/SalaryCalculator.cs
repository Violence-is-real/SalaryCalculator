using System;
using System.Collections.Generic;

namespace SalaryCalculator.WPF
{
    public enum Position
    {
        Assistant,   // 150 руб/час
        Docent,      // 250 руб/час
        Professor    // 350 руб/час
    }

    public class SalaryResult
    {
        public decimal GrossSalary { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetSalary { get; set; }
    }

    public static class SalaryCalculator
    {
        private static readonly Dictionary<Position, decimal> HourlyRates = new Dictionary<Position, decimal>
        {
            [Position.Assistant] = 150,
            [Position.Docent] = 250,
            [Position.Professor] = 350
        };

        public static SalaryResult Calculate(Position position, decimal hours, decimal taxRatePercent)
        {
            if (hours < 0)
                throw new ArgumentException("Количество часов не может быть отрицательным.");
            if (taxRatePercent < 0 || taxRatePercent > 100)
                throw new ArgumentException("Налоговая ставка должна быть от 0 до 100.");

            decimal rate = HourlyRates[position];
            decimal gross = hours * rate;
            decimal tax = gross * (taxRatePercent / 100);
            decimal net = gross - tax;

            return new SalaryResult
            {
                GrossSalary = gross,
                TaxAmount = tax,
                NetSalary = net
            };
        }
    }
}