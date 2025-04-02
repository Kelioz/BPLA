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
using BLA.utils;

namespace BLA
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int role { get; set; }
        public MainWindow(int role)
        {
            InitializeComponent();
            DashboardPage detailsPage = new DashboardPage();
            MainFrame.Content = detailsPage;

            if (MainFrame.Content == detailsPage)
            {
                homePageBtn.Background = Brushes.DimGray;
            }

            this.role = role;
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void toPartnersBtn_Click(object sender, RoutedEventArgs e)
        {
            if (role == 1)
            {
                PartnersPage partnersPage = new PartnersPage();
                MainFrame.Navigate(partnersPage);
                Brush brush = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255));
                Brush brush1 = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
                homePageBtn.Background = brush1;
                partnerPageBtn.Background = brush;
                SettingsBtn.Background = brush1;
                aboutBtn.Background = brush1;
                detailsBtn.Background = brush1;
            }
            else
            {
                MessageBox.Show("У вас нет прав");
            }

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

            DashboardPage Dashboardpage = new DashboardPage();
            MainFrame.Navigate(Dashboardpage);

            Brush brush = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255));
            Brush brush1 = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
            homePageBtn.Background = brush;
            partnerPageBtn.Background = brush1;
            SettingsBtn.Background = brush1;
            aboutBtn.Background = brush1;
            detailsBtn.Background = brush1;

        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage detailsPage = new SettingsPage(this);
            MainFrame.Navigate(detailsPage);

            Brush brush = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255));
            Brush brush1 = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
            homePageBtn.Background = brush1;
            partnerPageBtn.Background = brush1;
            SettingsBtn.Background = brush;
            aboutBtn.Background = brush1;
            detailsBtn.Background = brush1;
        }

        private void aboutBtn_Click(object sender, RoutedEventArgs e)
        {
            AboutPage detailsPage = new AboutPage();
            MainFrame.Navigate(detailsPage);

            Brush brush = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255));
            Brush brush1 = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
            homePageBtn.Background = brush1;
            partnerPageBtn.Background = brush1;
            SettingsBtn.Background = brush1;
            aboutBtn.Background = brush;
            detailsBtn.Background = brush1;
        }

        private void detailsPage_Click(object sender, RoutedEventArgs e)
        {
            DetailsPage detailsPage = new DetailsPage();
            MainFrame.Navigate(detailsPage);

            Brush brush = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255));
            Brush brush1 = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
            homePageBtn.Background = brush1;
            partnerPageBtn.Background = brush1;
            SettingsBtn.Background = brush1;
            aboutBtn.Background = brush1;
            detailsBtn.Background = brush;
        }
    }       
}
