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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BLA
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void helpBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Позвоните системному администратору на номер +7 999 999 99 99", "Помощь" , MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
                string loginUser = UsernameBox.Text;
                string password = PasswordBox.Password;

                DB dB = new DB();
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(@"SELECT Role,Login, Password From Users Where login = @uL AND password = @uP", dB.GetConnection());
                command.Parameters.Add("@uL", SqlDbType.VarChar).Value = loginUser;
                command.Parameters.Add("@uP", SqlDbType.VarChar).Value = password;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    string group = table.Rows[0]["role"].ToString();
                    if (group == "1")
                    {
                        MainWindow mainWindowAdmin = new MainWindow(Convert.ToInt32(group));
                        mainWindowAdmin.Show();
                        this.Close();
                }
                    else if (group == "2")
                    {
                        MainWindow mainWindowManager = new MainWindow(Convert.ToInt32(group));
                        mainWindowManager.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("У вас нет доступа");
                    }

                }
        }
    }
}
