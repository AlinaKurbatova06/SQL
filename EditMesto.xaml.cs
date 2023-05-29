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
    /// Логика взаимодействия для EditMesto.xaml
    /// </summary>
    public partial class EditMesto : Window
    {
        int ID;
        public EditMesto(int iD)
        {
            InitializeComponent();
            ID = iD;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Места_дислокации WHERE Номер_места = " + ID, sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                country.Text = reader["Страна"].ToString();
                city.Text = reader["Город"].ToString();
                address.Text = reader["Адрес"].ToString();
                square.Text = reader["Площадь"].ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("UPDATE Места_дислокации SET Страна = '" + country.Text + "', Город = '" + city.Text + "', Адрес = '" + address.Text + "', Площадь = '" + square.Text + "' WHERE Номер_места = " + ID, sql);
            command.ExecuteNonQuery();
            Close();
        }
    }
}
