using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для AddRota.xaml
    /// </summary>
    public partial class AddRota : Window
    {
        int[] chasti = new int[200];
        public AddRota()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int sel1 = chast.SelectedIndex;
            if (sel1 == -1)
            {
                MessageBox.Show("Выберите значение в списке.");
                return;
            }

            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Роты (Наименование_роты, Код_части) VALUES ('" + name.Text + "', " + chasti[sel1] + ")", sql);
            command.ExecuteNonQuery();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Части", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = chast.Items.Add(reader["Наименование_части"].ToString());
                chasti[row] = Convert.ToInt16(reader["Номер_части"].ToString());
            }
            reader.Close();
        }
    }
}
