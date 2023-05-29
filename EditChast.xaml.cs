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
using System.Xml.Linq;

namespace ИС_Военный_округ
{
    /// <summary>
    /// Логика взаимодействия для EditChast.xaml
    /// </summary>
    public partial class EditChast : Window
    {
        int ID;
        int[] types = new int[200], places = new int[200];
        public EditChast(int iD)
        {
            InitializeComponent();
            ID = iD;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int sel1 = type.SelectedIndex, sel2 = address.SelectedIndex;
            if (sel1 == -1 || sel2 == -1)
            {
                MessageBox.Show("Выберите значение в списке.");
                return;
            }

            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("UPDATE Части SET Наименование_части = '" + name.Text + "', Код_места = " + places[sel2] + ", Код_вида = " + types[sel1] + " WHERE Номер_части = " + ID, sql);
            command.ExecuteNonQuery();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Части WHERE Номер_части = " + ID, sql);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int ctype = Convert.ToInt16(reader["Код_вида"].ToString());
            int cmesto = Convert.ToInt16(reader["Код_места"].ToString());
            name.Text = reader["Наименование_части"].ToString();
            reader.Close();

            command = new SqlCommand("SELECT * FROM Виды_войск", sql);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = type.Items.Add(reader["Наименование_вида"].ToString());
                types[row] = Convert.ToInt16(reader["Номер_вида"].ToString());
                if(ctype == types[row]) type.SelectedIndex = row;
            }
            reader.Close();

            command = new SqlCommand("SELECT * FROM Места_дислокации", sql);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = address.Items.Add(reader["Страна"].ToString() + ", " + reader["Город"].ToString());
                places[row] = Convert.ToInt16(reader["Номер_места"].ToString());
                if (cmesto == places[row]) address.SelectedIndex = row;
            }
        }
    }
}
