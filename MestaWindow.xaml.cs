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
using static ИС_Военный_округ.VidWindow;

namespace ИС_Военный_округ
{
    /// <summary>
    /// Логика взаимодействия для MestaWindow.xaml
    /// </summary>
    public partial class MestaWindow : Window
    {
        public class DataItem
        {
            public string ID { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
            public string Square { get; set; }
        }
        public MestaWindow()
        {
            InitializeComponent();
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
            SqlCommand command = new SqlCommand("SELECT * FROM Места_дислокации", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DataItem item = new DataItem();
                item.ID = reader["Номер_места"].ToString();
                item.Country = reader["Страна"].ToString();
                item.City = reader["Город"].ToString();
                item.Address = reader["Адрес"].ToString();
                item.Square = reader["Площадь"].ToString();
                dataGrid.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddMesto form = new AddMesto();
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
            EditMesto form = new EditMesto(Convert.ToInt16(row.ID));
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
                SqlCommand command = new SqlCommand("DELETE FROM Места_дислокации WHERE Номер_места = " + delete, sql);
                command.ExecuteNonQuery();
                loadData();
            }
        }
    }
}
