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
using System.Windows.Shapes;

namespace ИС_Военный_округ
{
    /// <summary>
    /// Логика взаимодействия для IndexWindow.xaml
    /// </summary>
    public partial class IndexWindow : Window
    {
        public IndexWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VidWindow form = new VidWindow();
            form.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MestaWindow form = new MestaWindow();
            form.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ChastiWindow form = new ChastiWindow();
            form.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SoldierWindow form = new SoldierWindow();
            form.ShowDialog();
        }
    }
}
