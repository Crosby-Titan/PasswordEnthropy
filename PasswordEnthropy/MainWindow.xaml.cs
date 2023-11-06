using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordEnthropy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Enthropy.EnthropyCalculator calculator = new();
        public MainWindow()
        {
            InitializeComponent();
            InitializeTable();
        }

        private void InitializeTable()
        {
            EnthropyTable.Columns.Add(new DataGridTextColumn
            {
                Header = "Значение",
                Binding = new Binding("Value")
            });
            EnthropyTable.Columns.Add(new DataGridTextColumn
            {
                Header = "Степень",
                Binding = new Binding("Cathegory")
            });

            var values = new object[]
            {
                new EnthropyTable{Value="0-24",Cathegory="Очень слабый пароль"},
                new EnthropyTable{Value="25-49",Cathegory="Слабый пароль"},
                new EnthropyTable{Value="50-74",Cathegory="Хороший пароль"},
                new EnthropyTable {Value = "75-100", Cathegory = "Отличный пароль" },
            };

            foreach (var value in values)
            {
                EnthropyTable.Items.Add(value);
            }

            EnthropyTable.IsReadOnly = true;
            EnthropyTable.SelectionMode = DataGridSelectionMode.Single;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(InputPassword.Text))
            {
                MessageBox.Show("Не введён пароль!");
            }

            double enthropyValue = calculator.Calculate(InputPassword.Text);

            Output.Content = $"Результат: {GetRank(enthropyValue)} ({Math.Round(enthropyValue,1)})";
        }

        private string? GetRank(double enthropyValue)
        {
            if (enthropyValue >= 0 && enthropyValue <= 24)
            {
                return (EnthropyTable.Items[0] as EnthropyTable)?.Cathegory;
            }
            else if (enthropyValue >= 25 && enthropyValue <= 49)
            {
                return (EnthropyTable.Items[1] as EnthropyTable)?.Cathegory;
            }
            else if (enthropyValue >= 50 && enthropyValue <= 74)
            {
                return (EnthropyTable.Items[2] as EnthropyTable)?.Cathegory;
            }
            else if (enthropyValue >= 75 && enthropyValue <= 100)
            {
                return (EnthropyTable.Items[3] as EnthropyTable)?.Cathegory;
            }
            else
                return string.Empty;
        }

    }

    class EnthropyTable
    {
        public string Value { get; set; }
        public string Cathegory { get; set; }
    }
}
