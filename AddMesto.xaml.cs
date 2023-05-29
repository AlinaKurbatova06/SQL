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
    /// Логика взаимодействия для AddMesto.xaml
    /// </summary>
    public partial class AddMesto : Window
    {
        public AddMesto()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Места_дислокации (Страна, Город, Адрес, Площадь) VALUES ('" + country.Text + "', '" + city.Text + "', '" + address.Text + "', '" + square.Text + "')", sql);
            command.ExecuteNonQuery();
            Close();
        }
    }
}
