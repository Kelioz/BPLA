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
            PriceTextBox.Text = price.ToString();
            getDiscAnd();
            TypeTextBox.Items.Add("Военный");
            TypeTextBox.Items.Add("Гражданский");
            TypeTextBox.Text = type;

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

        void getDiscAnd()
        {
            try
            {
                DB dB = new DB();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dataTable = new DataTable();
                SqlCommand command = new SqlCommand(@"Select Description, Characteristics From Details_View Where id = @id ", dB.GetConnection());
                command.Parameters.Add("@id", SqlDbType.Int).Value = this._rowId;
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);

                string desc = dataTable.Rows[0]["Description"].ToString();
                string charc = dataTable.Rows[0]["Characteristics"].ToString();

                charBox.Text = charc;
                discBox.Text = desc;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }


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
                SqlCommand command = new SqlCommand(@"UPDATE Datails SET Name = @name, Count = @count, Type = @type, Price_For_One = @price, Description = @disc, Characteristics = @char WHERE id = @id", db.GetConnection());
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@count", count);
                command.Parameters.AddWithValue("@type", getTypeId(type));
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@disc", discBox.Text);
                command.Parameters.AddWithValue("@char", charBox.Text);
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
