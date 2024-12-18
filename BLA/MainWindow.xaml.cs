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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DetailsPage detailsPage = new DetailsPage();
            MainFrame.Content = detailsPage;
        }

        private void toPartnersBtn_Click(object sender, RoutedEventArgs e)
        {
            PartnersPage partnersPage = new PartnersPage();
            MainFrame.Navigate(partnersPage);
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                toPartnersBtn.Visibility = Visibility.Hidden;
                BackBtn.Visibility = Visibility.Visible;
            }
            else
            {
                toPartnersBtn.Visibility = Visibility.Visible;
                BackBtn.Visibility = Visibility.Hidden;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered_1(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                toPartnersBtn.Visibility = Visibility.Hidden;
                BackBtn.Visibility = Visibility.Visible;
            }
            else
            {
                toPartnersBtn.Visibility = Visibility.Visible;
                BackBtn.Visibility = Visibility.Hidden;
            }
        }
    }       
}
