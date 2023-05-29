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
using System.Xml.Linq;

namespace ИС_Военный_округ
{
    /// <summary>
    /// Логика взаимодействия для AddSoldier.xaml
    /// </summary>
    public partial class AddSoldier : Window
    {
        int[] roti = new int[200], chasti = new int[200];
        public AddSoldier()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int sel1 = rota.SelectedIndex, sel2 = chast.SelectedIndex;
            if (sel1 == -1 || sel2 == -1)
            {
                MessageBox.Show("Выберите значение в списке.");
                return;
            }
            int crota = roti[sel1];
            int cchast = chasti[sel2];
            int visl = DateTime.Now.Year - Convert.ToInt32(year2.Text)-1;
            if (visl <= 0) visl = 0;
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Личный_состав (Фамилия, Код_роты, Код_части, Должность, Год_рождения, Год_призыва, Выслуга, Награды, Участие_в_воен) VALUES ('" + family.Text + "', " + crota + ", " + cchast + ", '" + rank.Text + "', '" + year.Text + "', '" + year2.Text + "', '" + visl + "', '" + nagr.Text + "', '" + uch.Text + "')", sql);
            command.ExecuteNonQuery();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Роты", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = rota.Items.Add(reader["Наименование_роты"].ToString());
                roti[row] = Convert.ToInt16(reader["Номер_роты"].ToString());
            }
            reader.Close();

            command = new SqlCommand("SELECT * FROM Части", sql);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = chast.Items.Add(reader["Наименование_части"].ToString());
                chasti[row] = Convert.ToInt16(reader["Номер_части"].ToString());
            }
        }
    }
}
