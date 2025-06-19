using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Forms;
using Image = iTextSharp.text.Image;

namespace BLA
{
    /// <summary>
    /// Логика взаимодействия для DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            InitializeComponent();
            initTable();
        }
        void initTable()
        {
            DB db = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand(@"Select id, Name, Count, Type, Price_For_One From Details_View", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            DetailsData.ItemsSource = table.DefaultView;
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            initTable();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int id = int.Parse(button.Tag.ToString());

                DataRowView row = (DataRowView)DetailsData.SelectedItem;
                if (row != null)
                {
                    string name = row["Name"].ToString();
                    int count = int.Parse(row["Count"].ToString());
                    string type = row["Type"].ToString();
                    decimal price = decimal.Parse(row["Price_For_One"].ToString());

                    EditWindow editWindow = new EditWindow(id, name, count, type, price);
                    editWindow.ShowDialog();

                    initTable();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int id = int.Parse(button.Tag.ToString());

                // Удаляем запись из базы данных
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        DB db = new DB();
                        SqlCommand command = new SqlCommand(@"DELETE FROM Datails WHERE id = @id", db.GetConnection());
                        command.Parameters.AddWithValue("@id", id);

                        db.GetConnection().Open();
                        command.ExecuteNonQuery();
                        db.GetConnection().Close();

                        MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                        initTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void AddDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
        }

        private void ToInfoPageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int id = int.Parse(button.Tag.ToString());

                DataRowView row = (DataRowView)DetailsData.SelectedItem;
                if (row != null)
                {
                    string name = row["Name"].ToString();
                    int count = int.Parse(row["Count"].ToString());
                    string type = row["Type"].ToString();
                    decimal price = decimal.Parse(row["Price_For_One"].ToString());

                    InfoPage editWindow = new InfoPage(id, name, count, type, price);
                    NavigationService.Navigate(editWindow);

                }
            }
        }

        private void PDFBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Получаем данные из представления
                DataTable dataTable = GetDataFromView();

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    System.Windows.MessageBox.Show("Нет данных для экспорта.");
                    return;
                }

                // 2. Создаем диалог сохранения файла
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "PDF файлы (*.pdf)|*.pdf",
                    FileName = $"Отчёт по запчастям {DateTime.Now:yyyy-MM-dd}.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    // 3. Создаем PDF документ
                    CreatePdfDocument(dataTable, saveFileDialog.FileName);

                    System.Windows.MessageBox.Show("Данные успешно экспортированы в PDF.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при экспорте в PDF: {ex.Message}");
            }
        }

        private DataTable GetDataFromView()
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT id as Номер, Name as Название, Count as Количество, Price_For_One as Цена_за_шт, Description as Описание," +
                "Characteristics as Характеристики, Image as Фото FROM Details_View";
            DB dB = new DB();


            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            dB.openConnection();
            adapter.Fill(dataTable);

            return dataTable;
        }

        private void CreatePdfDocument(DataTable dataTable, string filePath)
        {
            using (var document = new Document(PageSize.A4.Rotate()))
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font headerFont = new Font(baseFont, 10, Font.BOLD);
                Font cellFont = new Font(baseFont, 9);

                // Заголовок документа
                document.Add(new Paragraph($"Отчёт по запчастям от {DateTime.Now:yyyy-MM-dd}", new Font(baseFont, 14, Font.BOLD)));
                document.Add(new Paragraph("\n"));

                // Создаем таблицу в PDF
                PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
                pdfTable.WidthPercentage = 100;

                // Устанавливаем относительные ширины столбцов
                float[] columnWidths = new float[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    columnWidths[i] = dataTable.Columns[i].ColumnName == "Image" ? 2f : 1f; // Больше места для изображения
                }
                pdfTable.SetWidths(columnWidths);

                // Заголовки столбцов
                foreach (DataColumn column in dataTable.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, headerFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(240, 240, 240);
                    cell.Padding = 5;
                    pdfTable.AddCell(cell);
                }

                // Данные таблицы
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        if (column.ColumnName == "Фото" && row[column] != DBNull.Value)
                        {
                            // Обработка изображения
                            byte[] imageData = (byte[])row[column];
                            PdfPCell imageCell = CreateImageCell(imageData);
                            pdfTable.AddCell(imageCell);
                        }
                        else if (column.ColumnName == "Количество")
                        {
                            Font quantityFont = new Font(baseFont, 15, Font.NORMAL, BaseColor.BLACK);

                            PdfPCell cell = new PdfPCell(new Phrase(row[column].ToString(), quantityFont));
                            cell.Padding = 4;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_CENTER;
                            pdfTable.AddCell(cell);
                        }
                        else if (column.ColumnName == "Цена_за_шт")
                        {
                            Font quantityFont = new Font(baseFont, 15, Font.NORMAL, BaseColor.BLACK);

                            PdfPCell cell = new PdfPCell(new Phrase(row[column].ToString(), quantityFont));
                            cell.Padding = 4;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_CENTER;
                            pdfTable.AddCell(cell);
                        }
                        else
                        {
                            Font quantityFont = new Font(baseFont, 15, Font.NORMAL, BaseColor.BLACK);
                            // Обычные текстовые ячейки
                            PdfPCell cell = new PdfPCell(new Phrase(row[column].ToString(), quantityFont));
                            cell.Padding = 4;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_CENTER;
                            pdfTable.AddCell(cell);
                        }
                    }
                }

                document.Add(pdfTable);
            }
        }

        private PdfPCell CreateImageCell(byte[] imageData)
        {
            try
            {
                // Создаем изображение из байтового массива
                Image image = Image.GetInstance(imageData);

                // Масштабируем изображение, чтобы оно помещалось в ячейку
                image.ScaleToFit(80f, 80f); // Максимальные размеры 80x80 пикселей

                PdfPCell cell = new PdfPCell(image);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Padding = 5;

                return cell;
            }
            catch
            {
                // Если не удалось создать изображение, возвращаем пустую ячейку
                return new PdfPCell(new Phrase("[Изображение]"));
            }
        }
    }
}

