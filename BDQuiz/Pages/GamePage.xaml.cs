using BDQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BDQuiz.Pages
{
    public partial class GamePage : Page
    {
        private int _currentQuizId;
        private DataBaseContext dbContext = new DataBaseContext();
        private Quiz _quizData;
        private int _currentQuestionIndex = 0;
        private int _score = 0;

        public GamePage(int quizId, string quizTitle)
        {
            InitializeComponent();
            _currentQuizId = quizId;
            TxtQuizTitle.Text = quizTitle;
            this.Loaded += GamePage_Loaded;
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadQuizData(_currentQuizId, GetDbContext());
            DisplayCurrentQuestion();
        }

        private DataBaseContext GetDbContext()
        {
            return dbContext;
        }

        private void LoadQuizData(int quizId, DataBaseContext dbContext)
        {
            try
            {
                _quizData = dbContext.Quizzes
                    .Include(q => q.Questions)
                        .ThenInclude(qs => qs.AnswerOptions)
                    .AsNoTracking()
                    .FirstOrDefault(q => q.Quizid == quizId);

                if (_quizData == null || !_quizData.Questions.Any())
                {
                    MessageBox.Show("Викторина не найдена или не содержит вопросов!", "Ошибка");
                    NavigationService.GoBack();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных викторины: {ex.Message}", "Ошибка БД");
                NavigationService.GoBack();
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (_quizData == null || _currentQuestionIndex >= _quizData.Questions.Count)
            {
                SaveQuizAttempt();
                MessageBox.Show($"Викторина завершена! Финальный счет: {_score} из {_quizData.Questions.Count}!", "Конец игры");
                NavigationService.GoBack();
                return;
            }

            var currentQuestion = _quizData.Questions.ElementAt(_currentQuestionIndex);

            TxtQuestion.Text = currentQuestion.Questiontext;

            AnswersPanel.Children.Clear();

            foreach (var answer in currentQuestion.AnswerOptions.OrderBy(a => System.Guid.NewGuid()))
            {
                var rb = new RadioButton
                {
                    Content = answer.Answertext,
                    Tag = answer.Answerid
                };
                AnswersPanel.Children.Add(rb);
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRb = AnswersPanel.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);

            if (selectedRb == null)
            {
                MessageBox.Show("Выберите вариант ответа, чтобы продолжить!", "Внимание");
                return;
            }

            if (!int.TryParse(selectedRb.Tag?.ToString(), out int selectedAnswerId))
            {
                MessageBox.Show("Ошибка: Не удалось получить ID ответа.", "Критическая ошибка");
                return;
            }

            var currentQuestion = _quizData.Questions.ElementAt(_currentQuestionIndex);

            var selectedAnswer = currentQuestion.AnswerOptions.FirstOrDefault(a => a.Answerid == selectedAnswerId);

            bool isCorrect = selectedAnswer?.Iscorrect ?? false;

            if (isCorrect)
            {
                _score++;
                MessageBox.Show($"Верно! Твой счет: {_score}", "Правильно");
            }
            else
            {
                MessageBox.Show($"Неверно. Правильный ответ: {currentQuestion.AnswerOptions.FirstOrDefault(a => a.Iscorrect)?.Answertext}", "Ошибка!");
            }

            //TODO: Возможно добавлю сохранение ответа в AtteemptAnswer

            _currentQuestionIndex++;

            if (_currentQuestionIndex >= _quizData.Questions.Count)
            {
                SaveQuizAttempt();
                MessageBox.Show($"Викторина завершена! Финальный счет: {_score} из {_quizData.Questions.Count}!", "Конец игры");
                NavigationService.GoBack();
            }
            else
            {
                DisplayCurrentQuestion();
            }
        }

        private void SaveQuizAttempt()
        {
            try
            {
                var attempt = new QuizAttempt
                {
                    Quizid = _currentQuizId,
                    Score = _score,
                    Totalquestions = _quizData.Questions.Count,
                    Username = "Игрок (WPF)"
                };

                dbContext.QuizAttempts.Add(attempt);
                dbContext.SaveChanges();
            }

            catch (System.Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить результат игры: {ex.Message}", "Ошибка сохранения");
            }
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}