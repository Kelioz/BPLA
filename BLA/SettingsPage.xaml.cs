using BLA.utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BLA
{
    public partial class SettingsPage : Page
    {
        private MainWindow _mainWindow;

        public SettingsPage(MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;

            sizeComboBox.Items.Add("По умолчанию");
            sizeComboBox.Items.Add("1300 * 720");
            sizeComboBox.Items.Add("1650 * 980");
            sizeComboBox.Items.Add("1920 * 1080");
        }

        private void ResetSettings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Настройки сброшены к значениям по умолчанию.", "Сброс настроек", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void sizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (sizeComboBox.SelectedItem.ToString() == "По умолчанию")
                {
                _mainWindow.WindowState = WindowState.Normal;
                _mainWindow.Width = 1400;
                _mainWindow.Height = 650;
                double screenHeight = SystemParameters.FullPrimaryScreenHeight;
                double screenWidth = SystemParameters.FullPrimaryScreenWidth;
                _mainWindow.Top = (screenHeight - _mainWindow.Height) / 2;
                _mainWindow.Left = (screenWidth - _mainWindow.Width) / 2;

            }
                else if (sizeComboBox.SelectedItem.ToString() == "1300 * 720")
                {
                _mainWindow.WindowState = WindowState.Normal;
                _mainWindow.Width = 1300;
                _mainWindow.Height = 720;

                double screenHeight = SystemParameters.FullPrimaryScreenHeight;
                double screenWidth = SystemParameters.FullPrimaryScreenWidth;
                _mainWindow.Top = (screenHeight - _mainWindow.Height) / 2;
                _mainWindow.Left = (screenWidth - _mainWindow.Width) / 2;


            }
                else if (sizeComboBox.SelectedItem.ToString() == "1650 * 980")
                {
                _mainWindow.WindowState = WindowState.Normal;
                _mainWindow.Width = 1650;
                    _mainWindow.Height = 980;
                double screenHeight = SystemParameters.FullPrimaryScreenHeight;
                double screenWidth = SystemParameters.FullPrimaryScreenWidth;
                _mainWindow.Top = (screenHeight - _mainWindow.Height) / 2;
                _mainWindow.Left = (screenWidth - _mainWindow.Width) / 2;
            }
            else if (sizeComboBox.SelectedItem.ToString() == "1920 * 1080")
                {
                _mainWindow.WindowState = WindowState.Normal;
                _mainWindow.Width = 1920;
                    _mainWindow.Height = 1080;
                double screenHeight = SystemParameters.FullPrimaryScreenHeight;
                double screenWidth = SystemParameters.FullPrimaryScreenWidth;
                _mainWindow.Top = (screenHeight - _mainWindow.Height) / 2;
                _mainWindow.Left = (screenWidth - _mainWindow.Width) / 2;
            }
        }
    }
}
