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
    /// Логика взаимодействия для AddVid.xaml
    /// </summary>
    public partial class AddVid : Window
    {
        public AddVid()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Виды_войск (Наименование_вида) VALUES ('" + vidname.Text + "')", sql);
            command.ExecuteNonQuery();
            Close();
        }
    }
}
