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

namespace DiplomApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
            try
            {
                var checkConnect = new DiplomDBEntities();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                CloseApp();
            }
        }
        private void OpenDiagramAndStatisticWindow()
        {
            DiagramAndStatisticWindow statisticWindow = new DiagramAndStatisticWindow();
            this.Visibility = Visibility.Hidden;
            statisticWindow.ShowDialog();
            this.Visibility = Visibility.Visible;
        }
        private void OpenBanksProductWindow()
        {
            BanksProductWindow banksProductWindow = new BanksProductWindow();
            this.Visibility = Visibility.Hidden;
            banksProductWindow.ShowDialog();
            this.Visibility = Visibility.Visible;
        }
        private void CloseApp() => Close();
        private void OpenStatisticButton_Click(object sender, RoutedEventArgs e) => OpenDiagramAndStatisticWindow();
        private void OpenProductsButton_Click(object sender, RoutedEventArgs e) => OpenBanksProductWindow();
        private void CloseButton_Click(object sender, RoutedEventArgs e) => CloseApp();
    }
}
