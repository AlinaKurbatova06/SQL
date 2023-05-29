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
    /// Логика взаимодействия для EditSoldier.xaml
    /// </summary>
    public partial class EditSoldier : Window
    {
        int[] roti = new int[200], chasti = new int[200];
        int ID;
        public EditSoldier(int iD)
        {
            InitializeComponent();
            ID = iD;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Личный_состав WHERE Номер_служащего = " + ID, sql);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            family.Text = reader["Фамилия"].ToString();
            rank.Text = reader["Должность"].ToString();
            year.Text = reader["Год_рождения"].ToString();
            year2.Text = reader["Год_призыва"].ToString();
            nagr.Text = reader["Награды"].ToString();
            uch.Text = reader["Участие_в_воен"].ToString();
            int crota = Convert.ToInt32(reader["Код_роты"].ToString());
            int cchast = Convert.ToInt32(reader["Код_части"].ToString());
            reader.Close();

            command = new SqlCommand("SELECT * FROM Роты", sql);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = rota.Items.Add(reader["Наименование_роты"].ToString());
                roti[row] = Convert.ToInt16(reader["Номер_роты"].ToString());
                if (crota == roti[row]) rota.SelectedIndex = row;
            }
            reader.Close();

            command = new SqlCommand("SELECT * FROM Части", sql);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = chast.Items.Add(reader["Наименование_части"].ToString());
                chasti[row] = Convert.ToInt16(reader["Номер_части"].ToString());
                if (cchast == chasti[row]) chast.SelectedIndex = row;
            }
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
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("UPDATE Личный_состав SET Фамилия = '" + family.Text + "', Код_роты = " + crota + ", Код_части = " + cchast + ", Должность = '" + rank.Text + "'" +
                ", Год_рождения = '" + year.Text + "', Год_призыва = '" + year2.Text + "', Награды = '" + nagr.Text + "', Участие_в_воен = '" + uch.Text + "' WHERE Номер_служащего = " + ID, sql);
            command.ExecuteNonQuery();
            Close();
        }
    }
}
