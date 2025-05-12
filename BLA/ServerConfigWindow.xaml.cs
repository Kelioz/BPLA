using System.Windows;

namespace BLA
{
    public partial class ServerConfigWindow : Window
    {
        public ServerConfigWindow()
        {
            InitializeComponent();
            if (System.IO.File.Exists(DB.ConnectionFilePath))
            {
                ServerNameTextBox.Text = System.IO.File.ReadAllText(DB.ConnectionFilePath).Trim();
            }
            else
            {
                ServerNameTextBox.Text = "DESKTOP-Q09V9UI"; 
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string serverName = ServerNameTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(serverName))
            {
                DB.UpdateServerName(serverName);
                MessageBox.Show("Имя сервера успешно сохранено", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите имя сервера", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}