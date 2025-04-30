using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace BLA
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
            TypeTextBox.Items.Add("Военный");
            TypeTextBox.Items.Add("Гражданский");
            TypeTextBox.Text = "Гражданский";
        }
        int getTypeId(string value)
        {
            if (value == "Гражданский")
            {
                return 1;
            }
            else if (value == "Военный")
            {
                return 2;
            }
            return 1;
        }

        private void CountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void PriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            // Валидация полей
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Введите название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(CountTextBox.Text, out int count) || count <= 0)
            {
                MessageBox.Show("Введите корректное количество (целое число больше 0)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(PriceTextBox.Text, out int price) || price <= 0)
            {
                MessageBox.Show("Введите корректную цену (целое число больше 0)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                DB db = new DB();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                SqlCommand cmd = new SqlCommand(@"Insert Into Datails (Name, Count, Type, Price_For_One, Description, Characteristics) Values (@Name, @Count, @Type, @PFO, @Disc, @Char )", db.GetConnection());
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = NameTextBox.Text;
                cmd.Parameters.Add("@Count", SqlDbType.Int).Value = Convert.ToInt32(CountTextBox.Text);
                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = getTypeId(TypeTextBox.Text);
                cmd.Parameters.Add("@PFO", SqlDbType.Int).Value = Convert.ToInt32(PriceTextBox.Text);
                cmd.Parameters.Add("@Disc", SqlDbType.VarChar).Value = discBox.Text;
                cmd.Parameters.Add("@Char", SqlDbType.VarChar).Value = charBox.Text;

                adapter.SelectCommand = cmd;
                adapter.Fill(table);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
