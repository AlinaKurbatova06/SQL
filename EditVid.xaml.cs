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
    /// Логика взаимодействия для EditVid.xaml
    /// </summary>
    public partial class EditVid : Window
    {
        int ID;
        public EditVid(int iD)
        {
            InitializeComponent();
            ID = iD;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Виды_войск WHERE Номер_вида = " + ID, sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                vidname.Text = reader["Наименование_вида"].ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("UPDATE Виды_войск SET Наименование_вида = '" + vidname.Text + "' WHERE Номер_вида = " + ID, sql);
            command.ExecuteNonQuery();
            Close();
        }
    }
}
