using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private int _rowId;
        public EditWindow(int id, string name, int count, string type, decimal price)
        {
            InitializeComponent();
            _rowId = id;

            // Заполняем поля
            NameTextBox.Text = name;
            CountTextBox.Text = count.ToString();
            TypeTextBox.Text = type;
            PriceTextBox.Text = price.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем данные из полей
                string name = NameTextBox.Text;
                int count = int.Parse(CountTextBox.Text);
                string type = TypeTextBox.Text;
                decimal price = decimal.Parse(PriceTextBox.Text);

                // Обновляем базу данных
                DB db = new DB();
                SqlCommand command = new SqlCommand(@"UPDATE Datails SET Name = @name, Count = @count, Type = @type, Price_For_One = @price WHERE id = @id", db.GetConnection());
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@count", count);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@id", _rowId);

                db.GetConnection().Open();
                command.ExecuteNonQuery();
                db.GetConnection().Close();

                MessageBox.Show("Row updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
