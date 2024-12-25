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

            if (MainFrame.Content == detailsPage)
            {
                homePageBtn.Background = Brushes.DimGray;
            }
        }

        private void toPartnersBtn_Click(object sender, RoutedEventArgs e)
        {
            PartnersPage partnersPage = new PartnersPage();
            MainFrame.Navigate(partnersPage);
            Brush brush = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255));
            Brush brush1 = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
            homePageBtn.Background = brush1;
            partnerPageBtn.Background = brush;
            SettingsBtn.Background = brush1;
            aboutBtn.Background = brush1;
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {

            }
            else
            {

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

            }
            else
            {

            }
        }

        private void homePageBtn_Click(object sender, RoutedEventArgs e)
        {
            DetailsPage detailsPage = new DetailsPage();
            MainFrame.Navigate(detailsPage);

            Brush brush = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255));
            Brush brush1 = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
            homePageBtn.Background = brush;
            partnerPageBtn.Background = brush1;
            SettingsBtn.Background = brush1;
            aboutBtn.Background = brush1;
            

        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage detailsPage = new SettingsPage();
            MainFrame.Navigate(detailsPage);

            Brush brush = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255));
            Brush brush1 = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
            homePageBtn.Background = brush1;
            partnerPageBtn.Background = brush1;
            SettingsBtn.Background = brush;
            aboutBtn.Background = brush1;
        }
    }       
}
