using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Логика взаимодействия для SoldierWindow.xaml
    /// </summary>
    public partial class SoldierWindow : Window
    {
        int[] ranks = new int[200], chasti = new int[200];
        public class DataItem
        {
            public string ID { get; set; }
            public string Family{ get; set; }
            public string Chast { get; set; }
            public string Rota { get; set; }
            public string Rank { get; set; }
            public string Year { get; set; }
            public string Year2 { get; set; }
            public string Visluga { get; set; }
            public string Nagradi { get; set; }
            public string Uchastie { get; set; }
        }
        public SoldierWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
            loadChast();
            loadPriziv();
            loadRank();
        }
        private void loadChast()
        {
            chast.Items.Clear();
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Части", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int row = chast.Items.Add(reader["Наименование_части"].ToString());
                chasti[row] = Convert.ToInt16(reader["Номер_части"].ToString());
            }
        }
        private void loadPriziv()
        {
            priziv.Items.Clear();
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT Год_призыва FROM Личный_состав GROUP BY Год_призыва", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                priziv.Items.Add(reader["Год_призыва"].ToString());
            }
        }
        private void loadRank()
        {
            rank.Items.Clear();
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT Должность FROM Личный_состав GROUP BY Должность", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                rank.Items.Add(reader["Должность"].ToString());
            }
        }
        private void loadData()
        {
            dataGrid.Items.Clear();
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT Личный_состав.*, Части.*, Роты.* FROM Личный_состав, Роты, Части WHERE Личный_состав.Код_части = Части.Номер_части AND Личный_состав.Код_роты = Роты.Номер_роты", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DataItem item = new DataItem();
                item.ID = reader["Номер_служащего"].ToString();
                item.Family = reader["Фамилия"].ToString();
                item.Rota = reader["Наименование_роты"].ToString();
                item.Chast = reader["Наименование_части"].ToString();
                item.Rank = reader["Должность"].ToString();
                item.Year = reader["Год_рождения"].ToString();
                item.Year2 = reader["Год_призыва"].ToString();
                item.Visluga = reader["Выслуга"].ToString();
                item.Nagradi = reader["Награды"].ToString();
                item.Uchastie = reader["Участие_в_воен"].ToString();
                dataGrid.Items.Add(item);
            }
        }
        private void loadData(String str)
        {
            dataGrid.Items.Clear();
            SqlConnection sql = new SqlConnection(Properties.Settings.Default.sqlConnection);
            sql.Open();
            SqlCommand command = new SqlCommand("SELECT Личный_состав.*, Части.*, Роты.* FROM Личный_состав, Роты, Части WHERE Личный_состав.Код_части = Части.Номер_части AND Личный_состав.Код_роты = Роты.Номер_роты AND " + str, sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DataItem item = new DataItem();
                item.ID = reader["Номер_служащего"].ToString();
                item.Family = reader["Фамилия"].ToString();
                item.Rota = reader["Наименование_роты"].ToString();
                item.Chast = reader["Наименование_части"].ToString();
                item.Rank = reader["Должность"].ToString();
                item.Year = reader["Год_рождения"].ToString();
                item.Year2 = reader["Год_призыва"].ToString();
                item.Visluga = reader["Выслуга"].ToString();
                item.Nagradi = reader["Награды"].ToString();
                item.Uchastie = reader["Участие_в_воен"].ToString();
                dataGrid.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddSoldier form = new AddSoldier();
            form.ShowDialog();
            loadData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int selected = dataGrid.SelectedIndex;
            if (selected == -1)
            {
                MessageBox.Show("Для изменения данных выберите нужную строку в таблице.");
                return;
            }
            DataItem row = (DataItem)dataGrid.Items[selected];
            EditSoldier form = new EditSoldier(Convert.ToInt16(row.ID));
            form.ShowDialog();
            loadData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PrintDialog Printdlg = new PrintDialog();
            if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                dataGrid.Measure(pageSize);
                dataGrid.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(dataGrid, Title);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void rank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sel = rank.SelectedIndex;
            if (sel == -1) { return; }
            loadData("Должность = '" + rank.Items[sel].ToString() + "'");
        }

        private void priziv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sel = priziv.SelectedIndex;
            if (sel == -1) { return; }
            loadData("Год_призыва = '" + priziv.Items[sel].ToString() + "'");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            loadData();
            priziv.SelectedIndex = -1;
            rank.SelectedIndex = -1;
            chast.SelectedIndex = -1;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sel = chast.SelectedIndex;
            if(sel == -1) { return; }
            loadData("Личный_состав.Код_части = " + chasti[sel]);
        }
    }
}
