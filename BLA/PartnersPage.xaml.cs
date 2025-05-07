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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BLA
{
    /// <summary>
    /// Логика взаимодействия для PartnersPage.xaml
    /// </summary>
    public partial class PartnersPage : Page
    {
        private Parnetrs selectedPartner;

        public PartnersPage()
        {
            InitializeComponent();
            InitTable();
        }

        public class Parnetrs
        {
            public int id { get; set; }
            public string Name { get; set; }
            public string FIO { get; set; }
            public long Phone { get; set; }
            public long INN { get; set; }
            public string Adress { get; set; }
        }

        void InitTable()
        {
            List<Parnetrs> list = new List<Parnetrs>();
            DB db = new DB();
            db.openConnection();
            SqlCommand cmd = new SqlCommand(@"Select * From Partners", db.GetConnection());
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new Parnetrs
                {
                    id = Convert.ToInt32(rdr["id"]),
                    Name = rdr["Name"].ToString(),
                    FIO = rdr["FIO_Director"].ToString(),
                    Phone = Convert.ToInt64(rdr["Phone"]),
                    INN = Convert.ToInt64(rdr["INN"]),
                    Adress = rdr["Adress"].ToString()
                });
            }
            db.closeConnection();
            partnersList.ItemsSource = list;
        }

        private void partnersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPartner = partnersList.SelectedItem as Parnetrs;
            deletePartnerBtn.IsEnabled = selectedPartner != null;
            editPartnerBtn.IsEnabled = selectedPartner != null;
        }
        private void editPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPartner != null)
            {
                EditPartnerWindow editWindow = new EditPartnerWindow(selectedPartner);
                editWindow.ShowDialog();
                InitTable(); 
            }
        }

        private void deletePartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPartner != null)
            {
                int id = selectedPartner.id;

                // Удаляем запись из базы данных
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?", 
                                                            "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        DB db = new DB();
                        SqlCommand command = new SqlCommand(@"DELETE FROM Partners WHERE id = @id", db.GetConnection());
                        command.Parameters.AddWithValue("@id", id);

                        db.GetConnection().Open();
                        command.ExecuteNonQuery();
                        db.GetConnection().Close();

                        MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                        InitTable();
                        deletePartnerBtn.IsEnabled = false; // После удаления кнопка снова становится неактивной
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void addPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPartnerWindow addPartnerWindow = new AddPartnerWindow();
            addPartnerWindow.Show();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            InitTable();
        }
    }
}
