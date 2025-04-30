using LiveCharts;
using LiveCharts.Wpf;
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
    /// Логика взаимодействия для DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        public SeriesCollection PieSeries { get; set; }

        public DashboardPage()
        {
            InitializeComponent();
            statistic();
            // Создаем данные для диаграммы
            PieSeries = new SeriesCollection
            {
                    new PieSeries
                {
                    Title = "Военные",
                    Values = new ChartValues<double> { getMilitaryDetails() },
                    DataLabels = true, // Отображение цифр
                    LabelPoint = chartPoint => $"{chartPoint.Y}", // Формат метки
                    FontSize = 24, // Размер цифр на частях
                    Fill = Brushes.DarkGreen// Цвет сегмента
                },
                    new PieSeries
                {
                    Title = "Гражданские",
                    Values = new ChartValues<double> { getPeaceDetails() },
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y}",
                    FontSize = 24, // Размер цифр на частях
                    Fill = Brushes.Gray // Цвет сегмента
                }
            };

            // Устанавливаем DataContext для привязки данных
            DataContext = this;
        }

        int getPeaceDetails()
        {
            DB dB = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand(@"Select SUM(Count) as Sum From Datails Where Type = '1'", dB.GetConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(dataTable);
            int count = Convert.ToInt32(dataTable.Rows[0]["Sum"]);
            return count;

        }
        int getMilitaryDetails()
        {
            DB dB = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand(@"Select SUM(Count) as Sum From Datails Where Type = '2'", dB.GetConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(dataTable);
            int count = Convert.ToInt32(dataTable.Rows[0]["Sum"]);
            return count;

        }
        
        int getuniqueCount()
        {
            DB dB = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand(@"Select COUNT(*) as countd From Datails", dB.GetConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(dataTable);
            int count = Convert.ToInt32(dataTable.Rows[0]["countd"]);
            return count;

        }
        int getuniqueCountPartner()
        {
            DB dB = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand(@"Select COUNT(*) as countp From Partners", dB.GetConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(dataTable);
            int count = Convert.ToInt32(dataTable.Rows[0]["countp"]);
            return count;

        }
        

        int getAvgPrice()
        {
            DB dB = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand(@"Select AVG(Price_For_One) as AvgPrice from Datails;", dB.GetConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(dataTable);
            int count = Convert.ToInt32(dataTable.Rows[0]["AvgPrice"]);
            return count;

        }
        void statistic()
        {
            int countDetails = getMilitaryDetails() + getPeaceDetails();
            countDetailsText.Content = countDetails;

            uniqueCountTex.Content = getuniqueCount();
            countPartnersText.Content = getuniqueCountPartner();
            avgPriceText.Content = getAvgPrice();
        }
    }
}
