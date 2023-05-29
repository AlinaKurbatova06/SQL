using System;
using System.Collections.Generic;
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

namespace ИС_Военный_округ
{
    /// <summary>
    /// Логика взаимодействия для ChastiWindow.xaml
    /// </summary>
    public partial class ChastiWindow : Window
    {
        public class DataItem
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Place { get; set; }
            public string Type { get; set; }
            public string Count { get; set; }
        }
        public ChastiWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddChast form = new AddChast();
            form.ShowDialog();
            loadData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int selected = dataGrid.SelectedIndex;
            if (selected == -1)
            {
                MessageBox.Show("Для изменения данных выберите нужную строку в таблице.");
                return;
            }
            DataItem row = (DataItem)dataGrid.Items[selected];
            EditChast form = new EditChast(Convert.ToInt16(row.ID));
            form.ShowDialog();
            loadData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int selected = dataGrid.SelectedIndex;
            if (selected == -1)
            {
                MessageBox.Show("Для изменения данных выберите нужную строку в таблице.");
                return;
            }
            DataItem row = (DataItem)dataGrid.Items[selected];
            int delete = Convert.ToInt16(row.ID);
            MessageBoxResult rsltMessageBox = MessageBox.Show("Вы действительно хотите удалить эту строку?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
                sql.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Части WHERE Номер_части = " + delete, sql);
                command.ExecuteNonQuery();
                loadData();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int selected = dataGrid.SelectedIndex;
            if (selected == -1)
            {
                MessageBox.Show("Для просмотра подразделений выберите нужную строку в таблице.");
                return;
            }
            DataItem row = (DataItem)dataGrid.Items[selected];
            RotaWindow form = new RotaWindow(Convert.ToInt16(row.ID));
            form.ShowDialog();
            loadData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
        }
        private void loadData()
        {
            dataGrid.Items.Clear();
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT Части.*, Места_дислокации.Страна, Места_дислокации.Город, Виды_войск.Наименование_вида FROM Части, Виды_войск, Места_дислокации WHERE Части.Код_места = Места_дислокации.Номер_места AND Части.Код_вида = Виды_войск.Номер_вида", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DataItem item = new DataItem();
                item.ID = reader["Номер_части"].ToString();
                item.Name = reader["Наименование_части"].ToString();
                item.Place = reader["Страна"].ToString() + ", " + reader["Город"].ToString();
                item.Type = reader["Наименование_вида"].ToString();
                item.Count = reader["Количество_рот"].ToString();
                dataGrid.Items.Add(item);
            }
        }
    }
}
