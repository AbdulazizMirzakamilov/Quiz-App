using BDQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BDQuiz.Pages
{
    public partial class QuizListPage : Page
    {
        private DataBaseContext dbContext = new DataBaseContext();
        private User _currentUser;

        public QuizListPage()
        {
            InitializeComponent();
            this.Loaded += QuizListPage_Loaded;
            BtnStart.IsEnabled = false;
            BtnEdit.IsEnabled = false;
            BtnDelete.IsEnabled = false;
        }

        private void QuizListPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadQuizzes();
        }

        private void LoadQuizzes()
        {
            try
            {
                var quizzes = dbContext.Quizzes.AsNoTracking().OrderByDescending(q => q.Createdat).ToList();

                QuizzesList.ItemsSource = quizzes;
                QuizzesList.DisplayMemberPath = "Title";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки викторин из БД: {ex.Message}", "Ошибка");
            }
        }

        private void QuizzesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isSelected = QuizzesList.SelectedItem != null;
            BtnStart.IsEnabled = isSelected;
            BtnEdit.IsEnabled = isSelected;
            BtnDelete.IsEnabled = isSelected;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (QuizzesList.SelectedItem is Quiz selectedQuiz)
            {
                NavigationService.Navigate(new GamePage(selectedQuiz.Quizid, selectedQuiz.Title));
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditorPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (QuizzesList.SelectedItem is Quiz selectedQuizHeader)
            {
                try
                {
                    using (var editDbContext = new DataBaseContext())
                    {
                        var quizToEdit = editDbContext.Quizzes.Include(q => q.Questions).ThenInclude(qs => qs.AnswerOptions).FirstOrDefault(q => q.Quizid == selectedQuizHeader.Quizid);

                        if (quizToEdit != null)
                        {
                            NavigationService.Navigate(new EditorPage(quizToEdit));
                        }
                        else
                        {
                            MessageBox.Show("Не удалось найти полную информацию о викторине.", "Ошибка загрузки");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки викторины для редактирования: {ex.Message}", "Ошибка БД");
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (QuizzesList.SelectedItem is Quiz selectedQuiz)
            {
                MessageBoxResult result = MessageBox.Show($"Вы точно хотите удалить викторину? '{selectedQuiz.Title}'? Это необратимо!", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        dbContext.Quizzes.Attach(selectedQuiz);
                        dbContext.Quizzes.Remove(selectedQuiz);
                        dbContext.SaveChanges();
                        MessageBox.Show($"Викторина '{selectedQuiz.Title}' успешно удалена!", "Готово");
                        LoadQuizzes();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"Произошла ошибка при удалении: {ex.Message}", "Ошибка БД");
                    }
                }
            }
        }
    }
}