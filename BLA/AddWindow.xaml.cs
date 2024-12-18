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
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand(@"Insert Into Datails (Name, Count, Type, Price_For_One, Description, Characteristics) Values (@Name, @Count, @Type, @PFO, @Disc, @Char )", db.GetConnection() );
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = NameTextBox.Text;
            cmd.Parameters.Add("@Count", SqlDbType.Int).Value = Convert.ToInt32(CountTextBox.Text);
            cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = TypeTextBox.Text;
            cmd.Parameters.Add("@PFO", SqlDbType.Int).Value = Convert.ToInt32(PriceTextBox.Text);
            cmd.Parameters.Add("@Disc", SqlDbType.VarChar).Value = discBox.Text;
            cmd.Parameters.Add("@Char", SqlDbType.VarChar).Value = charBox.Text;

            adapter.SelectCommand = cmd;
            adapter.Fill(table);
            this.Close();
        }
    }
}
