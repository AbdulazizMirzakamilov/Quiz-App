using BDQuiz.Models;
using BDQuiz.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BDQuiz.Pages
{
    public partial class LoginPage : Page
    {
        private DataBaseContext db = new DataBaseContext();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = TxtLogin.Text.Trim();
            string pass = PboxPassword.Password.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            string passHash = Hash.HashPassword(pass);

            var user = db.Users.FirstOrDefault(u => u.Username == login && u.Passwordhash == passHash);

            if (user != null)
            {
                MessageBox.Show($"Добро пожаловать, {user.Username}!");

                NavigationService.Navigate(new QuizListPage());
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}