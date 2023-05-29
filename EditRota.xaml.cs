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
    /// Логика взаимодействия для EditRota.xaml
    /// </summary>
    public partial class EditRota : Window
    {
        int ID;
        int[] chasti = new int[200];
        public EditRota(int iD)
        {
            InitializeComponent();
            ID = iD;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Роты WHERE Номер_роты = " + ID, sql);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            name.Text = reader["Наименование_роты"].ToString();
            int chas = Convert.ToInt32(reader["Код_части"].ToString());
            reader.Close();

            command = new SqlCommand("SELECT * FROM Части", sql);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = chast.Items.Add(reader["Наименование_части"].ToString());
                chasti[row] = Convert.ToInt16(reader["Номер_части"].ToString());
                if (chas == chasti[row]) chast.SelectedIndex = row;
            }
            reader.Close();
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
            SqlCommand command = new SqlCommand("UPDATE Роты SET Наименование_роты = '" + name.Text + "', Код_части = " + chasti[sel1] + " WHERE Номер_роты = " + ID, sql);
            command.ExecuteNonQuery();
            Close();
        }
    }
}
