using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ИС_Военный_округ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(code.Text != "JJDYRF")
            {
                MessageBox.Show("Введен неверный код доступа.");
                return;
            }
            Hide();
            IndexWindow form = new IndexWindow();
            form.ShowDialog();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
