using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace BLA
{
    public partial class EditPartnerWindow : Window
    {
        private readonly PartnersPage.Parnetrs _partner;

        public EditPartnerWindow(PartnersPage.Parnetrs partner)
        {
            InitializeComponent();
            _partner = partner;

            // Заполняем поля данными выбранного партнёра
            nameCompanyBox.Text = _partner.Name;
            FIOBox.Text = _partner.FIO;
            PhoneBox.Text = _partner.Phone.ToString();
            InnBox.Text = _partner.INN.ToString();
            adressBox.Text = _partner.Adress;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем, что все поля заполнены
                if (string.IsNullOrWhiteSpace(nameCompanyBox.Text) ||
                    string.IsNullOrWhiteSpace(FIOBox.Text) ||
                    string.IsNullOrWhiteSpace(PhoneBox.Text) ||
                    string.IsNullOrWhiteSpace(InnBox.Text) ||
                    string.IsNullOrWhiteSpace(adressBox.Text))
                {
                    MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверяем корректность числовых полей
                if (!long.TryParse(PhoneBox.Text, out long phone) || !long.TryParse(InnBox.Text, out long inn))
                {
                    MessageBox.Show("Телефон и ИНН должны быть числовыми значениями!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Обновляем данные в базе
                DB db = new DB();
                SqlCommand command = new SqlCommand(
                    @"UPDATE Partners 
                      SET Name = @Name, 
                          FIO_Director = @FIO, 
                          Phone = @Phone, 
                          INN = @Inn, 
                          Adress = @Adress 
                      WHERE id = @Id",
                    db.GetConnection());

                command.Parameters.AddWithValue("@Name", nameCompanyBox.Text);
                command.Parameters.AddWithValue("@FIO", FIOBox.Text);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Inn", inn);
                command.Parameters.AddWithValue("@Adress", adressBox.Text);
                command.Parameters.AddWithValue("@Id", _partner.id);

                db.GetConnection().Open();
                command.ExecuteNonQuery();
                db.GetConnection().Close();

                MessageBox.Show("Данные партнёра успешно обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}