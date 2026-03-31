using System;
using System.Windows;

namespace SalaryCalculator.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение должности
                Position position;
                if (rbAssistant.IsChecked == true)
                    position = Position.Assistant;
                else if (rbDocent.IsChecked == true)
                    position = Position.Docent;
                else if (rbProfessor.IsChecked == true)
                    position = Position.Professor;
                else
                    throw new InvalidOperationException("Выберите должность.");

                // Получение часов
                if (!decimal.TryParse(txtHours.Text, out decimal hours))
                    throw new FormatException("Введите корректное число часов.");
                if (hours <= 0)
                    throw new ArgumentException("Количество часов должно быть положительным.");

                // Получение налоговой ставки
                decimal taxRate;
                if (rbTax13.IsChecked == true)
                    taxRate = 13;
                else if (rbTax0.IsChecked == true)
                    taxRate = 0;
                else
                    throw new InvalidOperationException("Выберите ставку налога.");

                // Расчёт
                var result = SalaryCalculator.Calculate(position, hours, taxRate);

                // Отображение
                tbGross.Text = $"{result.GrossSalary:F2} руб";
                tbTax.Text = $"{result.TaxAmount:F2} руб";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}