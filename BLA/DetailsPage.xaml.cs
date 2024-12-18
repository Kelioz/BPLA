using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace BLA
{
    /// <summary>
    /// Логика взаимодействия для DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            InitializeComponent();
            initTable();
        }
        void initTable()
        {
            DB db = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand(@"Select id, Name, Count, Type, Price_For_One From Datails", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            DetailsData.ItemsSource = table.DefaultView;
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            initTable();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int id = int.Parse(button.Tag.ToString());

                DataRowView row = (DataRowView)DetailsData.SelectedItem;
                if (row != null)
                {
                    string name = row["Name"].ToString();
                    int count = int.Parse(row["Count"].ToString());
                    string type = row["Type"].ToString();
                    decimal price = decimal.Parse(row["Price_For_One"].ToString());

                    EditWindow editWindow = new EditWindow(id, name, count, type, price);
                    editWindow.ShowDialog();

                    initTable();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int id = int.Parse(button.Tag.ToString());

                // Удаляем запись из базы данных
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        DB db = new DB();
                        SqlCommand command = new SqlCommand(@"DELETE FROM Datails WHERE id = @id", db.GetConnection());
                        command.Parameters.AddWithValue("@id", id);

                        db.GetConnection().Open();
                        command.ExecuteNonQuery();
                        db.GetConnection().Close();

                        MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                        initTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void AddDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int id = int.Parse(button.Tag.ToString());

                DataRowView row = (DataRowView)DetailsData.SelectedItem;
                if (row != null)
                {
                    string name = row["Name"].ToString();
                    int count = int.Parse(row["Count"].ToString());
                    string type = row["Type"].ToString();
                    decimal price = decimal.Parse(row["Price_For_One"].ToString());

                    InfoPage editWindow = new InfoPage(id, name, count, type, price);
                    NavigationService.Navigate(editWindow);

                }
            }
        }
    }
}

