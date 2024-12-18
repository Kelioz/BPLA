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
    /// Логика взаимодействия для AddPartnerWindow.xaml
    /// </summary>
    public partial class AddPartnerWindow : Window
    {
        public AddPartnerWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand(@"Insert Into Partners (Name, FIO_Director, Phone, INN, Adress) Values (@Name, @FIO, @Phone, @Inn, @Adress)", db.GetConnection());
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = nameCompanyBox.Text;
            command.Parameters.Add("@FIO", SqlDbType.VarChar).Value = FIOBox.Text;
            command.Parameters.Add("@Phone", SqlDbType.BigInt).Value = PhoneBox.Text;
            command.Parameters.Add("@Inn", SqlDbType.BigInt).Value = InnBox.Text;
            command.Parameters.Add("@Adress", SqlDbType.VarChar).Value = adressBox.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            this.Close();
            
        }
    }
}
