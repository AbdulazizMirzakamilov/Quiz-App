using BDQuiz.Models;
using BDQuiz.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BDQuiz.Pages
{
    public partial class RegisterPage : Page
    {
        private DataBaseContext db = new DataBaseContext();

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            string login = TxtLogin.Text.Trim();
            string email = TxtEmail.Text.Trim();
            string pass = PboxPassword.Password.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (db.Users.Any(u => u.Username == login))
            {
                MessageBox.Show("Такой логин уже занят!");
                return;
            }

            var newUser = new User
            {
                Username = login,
                Email = email,
                Passwordhash = Hash.HashPassword(pass),
                Isadmin = false,
                Createdat = System.DateTime.Now
            };

            db.Users.Add(newUser);
            db.SaveChanges();

            MessageBox.Show("Успешно! Теперь войдите.");
            NavigationService.GoBack();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}