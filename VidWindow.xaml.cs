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
using System.Windows.Shapes;

namespace ИС_Военный_округ
{
    /// <summary>
    /// Логика взаимодействия для VidWindow.xaml
    /// </summary>
    public partial class VidWindow : Window
    {
        public class DataItem
        {
            public string ID { get; set; }
            public string Name { get; set; }
        }
        public VidWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddVid form = new AddVid();
            form.ShowDialog();
            loadData();
        }
        private void loadData()
        {
            dataGrid.Items.Clear();
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Виды_войск", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DataItem item = new DataItem();
                item.ID = reader["Номер_вида"].ToString();
                item.Name = reader["Наименование_вида"].ToString();
                dataGrid.Items.Add(item);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int selected = dataGrid.SelectedIndex;
            if(selected == -1)
            {
                MessageBox.Show("Для изменения данных выберите нужную строку в таблице.");
                return;
            }
            DataItem row = (DataItem) dataGrid.Items[selected];
            EditVid form = new EditVid(Convert.ToInt16(row.ID));
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
            if(rsltMessageBox == MessageBoxResult.Yes) {
                SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
                sql.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Виды_войск WHERE Номер_вида = " + delete, sql);
                command.ExecuteNonQuery();
                loadData();
            }
        }
    }
}
