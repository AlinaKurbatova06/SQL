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
    /// Логика взаимодействия для RotaWindow.xaml
    /// </summary>
    public partial class RotaWindow : Window
    {
        public class DataItem
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Count { get; set; }
            public string Chast { get; set; }
        }
        int ID;
        public RotaWindow(int iD)
        {
            InitializeComponent();
            ID = iD;
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
            SqlCommand command = new SqlCommand("SELECT Роты.*, Части.* FROM Части, Роты WHERE Части.Номер_части = Роты.Код_части AND Роты.Код_части = " + ID, sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DataItem item = new DataItem();
                item.ID = reader["Номер_роты"].ToString();
                item.Name = reader["Наименование_роты"].ToString();
                item.Count = reader["Количество_служащих"].ToString();
                item.Chast = reader["Наименование_части"].ToString();
                dataGrid.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddRota form = new AddRota();
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
            EditRota form = new EditRota(Convert.ToInt16(row.ID));
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
                SqlCommand command = new SqlCommand("DELETE FROM Роты WHERE Номер_роты = " + delete, sql);
                command.ExecuteNonQuery();
                loadData();
            }
        }
    }
}
