using BDQuiz.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BDQuiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Fr_content_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void FrContent_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}