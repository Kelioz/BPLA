using System.Windows;
using System.Windows.Controls;

namespace BLA
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void ResetSettings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Настройки сброшены к значениям по умолчанию.", "Сброс настроек", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
