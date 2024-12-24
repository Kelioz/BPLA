using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace BLA
{
    public partial class InfoPage : Page
    {
        public string Description { get; set; }
        public string Characteristics { get; set; }
        public string imgPath { get; set; }
        public int ID { get; set; }
        public decimal price { get; set; }
        public int count { get; set; }

        public InfoPage(int id, string name, int count, string type, decimal price)
        {
            InitializeComponent();
            this.price = price;
            tittleText.Text += name;
            this.count = count;
            typeText.Inlines.Add(new Run(type));
            this.ID = id;
            getDetailsInfo(id);
        }

        // Загрузка данных из базы данных и отображение на странице
        void getDetailsInfo(int id)
        {
            try
            {
                DB dB = new DB();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand(@"Select * From Datails Where id = @id", dB.GetConnection());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                adapter.SelectCommand = cmd;
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    string disc = dataTable.Rows[0]["Description"].ToString();
                    string charac = dataTable.Rows[0]["Characteristics"].ToString();
                    this.Description = disc;
                    this.Characteristics = charac;

                    discText.Inlines.Add(new Run(Description));
                    priceText.Inlines.Add(new Run(this.price.ToString()));
                    countText.Inlines.Add(new Run($"{this.count} шт."));
                    string[] characLines = Characteristics.Split(new[] { "/n" }, StringSplitOptions.None);
                    charecText.Inlines.Clear();
                    charecText.Inlines.Add(new Run("Характеристики: ") { FontWeight = FontWeights.Bold });

                    foreach (var line in characLines)
                    {
                        charecText.Inlines.Add(new LineBreak());
                        charecText.Inlines.Add(new Run(line));
                    }

                    // Проверяем и извлекаем изображение
                    if (dataTable.Rows[0]["Image"] != DBNull.Value)
                    {
                        byte[] imageData = (byte[])dataTable.Rows[0]["Image"];
                        DisplayImage(imageData); // Отображаем изображение
                    }
                }
                else
                {
                    MessageBox.Show("Данные не найдены для указанного ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при извлечении данных: {ex.Message}");
            }
        }

        // Отображение изображения в элемент Image
        private void DisplayImage(byte[] imageData)
        {
            try
            {
                if (imageData != null && imageData.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = ms;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad; // Это поможет сразу загрузить изображение в память
                        bitmap.EndInit();

                        // Убедитесь, что элемент Image правильно отображает изображение
                        detailImage.Source = bitmap;
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить изображение.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отображении изображения: {ex.Message}");
            }
        }

        // Обработчик кнопки загрузки изображения
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Открываем диалоговое окно для выбора файла
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"; // Фильтрация по типам изображений

                if (openFileDialog.ShowDialog() == true)
                {
                    // Получаем путь к выбранному файлу
                    string filePath = openFileDialog.FileName;

                    // Читаем файл как байтовый массив
                    byte[] imageData = File.ReadAllBytes(filePath);

                    // Сохраняем изображение в базу данных
                    SaveImageToDatabase(imageData);

                    // Отображаем выбранное изображение на странице
                    DisplayImage(imageData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
            }
        }

        private void SaveImageToDatabase(byte[] imageData)
        {
            try
            {
                DB dB = new DB();
                SqlCommand cmd = new SqlCommand(@"UPDATE Datails SET Image = @image WHERE id = @id", dB.GetConnection());

                // Добавляем параметры
                cmd.Parameters.Add("@image", SqlDbType.VarBinary).Value = imageData;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.ID; // Используем ID объекта, чтобы обновить правильную строку

                // Выполняем команду
                dB.openConnection();
                int rowsAffected = cmd.ExecuteNonQuery();
                dB.closeConnection();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Изображение успешно загружено в базу данных.");
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить изображение в базу данных.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изображения в базу данных: {ex.Message}");
            }
        }
    }
}
