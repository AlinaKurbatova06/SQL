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
    /// Логика взаимодействия для AddChast.xaml
    /// </summary>
    public partial class AddChast : Window
    {
        int[] types = new int[200], places = new int[200];
        public AddChast()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int sel1 = type.SelectedIndex, sel2 = address.SelectedIndex;
            if(sel1 == -1 || sel2 == -1)
            {
                MessageBox.Show("Выберите значение в списке.");
                return;
            }

            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Части (Наименование_части, Код_места, Код_вида, Количество_рот) VALUES ('" + name.Text + "', " + places[sel2] + ", " + types[sel1] + ", 0)", sql);
            command.ExecuteNonQuery();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Виды_войск", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = type.Items.Add(reader["Наименование_вида"].ToString());
                types[row] = Convert.ToInt16(reader["Номер_вида"].ToString());
            }
            reader.Close();

            command = new SqlCommand("SELECT * FROM Места_дислокации", sql);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = address.Items.Add(reader["Страна"].ToString() + ", " + reader["Город"].ToString());
                places[row] = Convert.ToInt16(reader["Номер_места"].ToString());
            }

        }
    }
}
