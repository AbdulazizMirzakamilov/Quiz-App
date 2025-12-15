using BDQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BDQuiz.Pages
{
    public partial class EditorPage : Page
    {
        private List<Question> _questionsList = new List<Question>();
        private Question _currentQuestion = null;
        private DataBaseContext dbContext = new DataBaseContext();
        private Quiz _originalQuiz = null;

        public EditorPage()
        {
            InitializeComponent();
            LoadQuestionsToSidebar();
            ClearQuestionForm();
            TxtQuizTitle.Text = "Новая викторина";
            BtnDeleteQuestion.IsEnabled = false;
        }

        public EditorPage(Quiz quizToEdit)
        {
            InitializeComponent();

            _originalQuiz = quizToEdit;
            TxtQuizTitle.Text = quizToEdit.Title;
            _questionsList = quizToEdit.Questions.ToList();

            LoadQuestionsToSidebar();
            ClearQuestionForm();
            BtnDeleteQuestion.IsEnabled = false;
        }

        private void LoadQuestionsToSidebar()
        {
            QuestionsList.Items.Clear();
            if (_questionsList.Count == 0)
            {
                QuestionsList.Items.Add("Нет добавленных вопросов.");
                return;
            }

            for (int i = 0; i < _questionsList.Count; i++)
            {
                QuestionsList.Items.Add($"Вопрос {i + 1}: {_questionsList[i].Questiontext}");
            }
        }

        private void ClearQuestionForm()
        {
            TxtQuestionText.Text = "";
            TxtAnswer1.Text = "";
            TxtAnswer2.Text = "";
            TxtAnswer3.Text = "";
            TxtAnswer4.Text = "";

            ChkCorrect1.IsChecked = false;
            ChkCorrect2.IsChecked = false;
            ChkCorrect3.IsChecked = false;
            ChkCorrect4.IsChecked = false;

            _currentQuestion = null;
            QuestionsList.SelectedIndex = -1;
            BtnDeleteQuestion.IsEnabled = false;
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void BtnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            ClearQuestionForm();
        }

        private void QuestionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = QuestionsList.SelectedIndex;
            if (index == -1 || index >= _questionsList.Count)
            {
                ClearQuestionForm();
                return;
            }

            _currentQuestion = _questionsList[index];
            TxtQuestionText.Text = _currentQuestion.Questiontext;
            BtnDeleteQuestion.IsEnabled = true;

            TxtAnswer1.Text = TxtAnswer2.Text = TxtAnswer3.Text = TxtAnswer4.Text = "";
            ChkCorrect1.IsChecked = ChkCorrect2.IsChecked = ChkCorrect3.IsChecked = ChkCorrect4.IsChecked = false;

            var answers = _currentQuestion.AnswerOptions.ToList();

            if (answers.Count > 0)
            {
                if (answers.Count > 0)
                {
                    TxtAnswer1.Text = answers[0].Answertext;
                    ChkCorrect1.IsChecked = answers[0].Iscorrect;
                }
                if (answers.Count > 1)
                {
                    TxtAnswer2.Text = answers[1].Answertext;
                    ChkCorrect2.IsChecked = answers[1].Iscorrect;
                }
                if (answers.Count > 2)
                {
                    TxtAnswer3.Text = answers[2].Answertext;
                    ChkCorrect3.IsChecked = answers[2].Iscorrect;
                }
                if (answers.Count > 3)
                {
                    TxtAnswer4.Text = answers[3].Answertext;
                    ChkCorrect4.IsChecked = answers[3].Iscorrect;
                }
            }
        }

        private void BtnSaveQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtQuestionText.Text))
            {
                MessageBox.Show("Введите текст вопроса!", "Ошибка валидации");
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtAnswer1.Text) || string.IsNullOrWhiteSpace(TxtAnswer2.Text))
            {
                MessageBox.Show("Введите минимум два варианта ответа!", "Ошибка валидации");
                return;
            }

            if (_currentQuestion == null)
            {
                _currentQuestion = new Question();
            }
            _currentQuestion.Questiontext = TxtQuestionText.Text.Trim();

            var answers = _currentQuestion.AnswerOptions.ToList();

            _currentQuestion.AnswerOptions.Clear();

            AddAnswerOption(_currentQuestion, TxtAnswer1.Text, ChkCorrect1.IsChecked == true);
            AddAnswerOption(_currentQuestion, TxtAnswer2.Text, ChkCorrect2.IsChecked == true);
            AddAnswerOption(_currentQuestion, TxtAnswer3.Text, ChkCorrect3.IsChecked == true);
            AddAnswerOption(_currentQuestion, TxtAnswer4.Text, ChkCorrect4.IsChecked == true);

            if (!_currentQuestion.AnswerOptions.Any(a => a.Iscorrect))
            {
                MessageBox.Show("Должен быть хотя бы один правильный ответ!", "Ошибка");
                return;
            }

            if (!_questionsList.Contains(_currentQuestion))
            {
                _questionsList.Add(_currentQuestion);
            }

            LoadQuestionsToSidebar();

            MessageBox.Show("Вопрос успешно сохранен локально!", "Готово");
        }

        private void AddAnswerOption(Question question, string text, bool isCorrect)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                question.AnswerOptions.Add(new AnswerOption
                {
                    Answertext = text.Trim(),
                    Iscorrect = isCorrect
                });
            }
        }

        private void BtnDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (_currentQuestion != null && QuestionsList.SelectedIndex != -1)
            {
                _questionsList.Remove(_currentQuestion);
                ClearQuestionForm();
                LoadQuestionsToSidebar();
                MessageBox.Show("Вопрос удален из викторины.", "Удалено");
            }
        }

        private void BtnSaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            string quizTitle = TxtQuizTitle.Text.Trim();

            if (string.IsNullOrWhiteSpace(quizTitle) || _questionsList.Count == 0)
            {
                MessageBox.Show("Викторина должна иметь название и хотя бы один вопрос!", "Ошибка сохранения");
                return;
            }

            try
            {
                if (_originalQuiz == null)
                {
                    var newQuiz = new Quiz
                    {
                        Title = quizTitle,
                        Description = "Викторина создана пользователем через приложение WPF",
                    };

                    foreach (var q in _questionsList)
                    {
                        newQuiz.Questions.Add(q);
                    }

                    dbContext.Quizzes.Add(newQuiz);
                }
                else
                {
                    _originalQuiz.Title = quizTitle;
                    var existingQuiz = dbContext.Quizzes.Include(q => q.Questions).ThenInclude(qs => qs.AnswerOptions).FirstOrDefault(q => q.Quizid == _originalQuiz.Quizid);

                    if (existingQuiz != null)
                    {
                        existingQuiz.Title = quizTitle;
                        dbContext.Questions.RemoveRange(existingQuiz.Questions);

                        foreach (var q in _questionsList)
                        {
                            q.Questionid = 0;
                            foreach (var a in q.AnswerOptions) a.Answerid = 0;
                            existingQuiz.Questions.Add(q);
                        }

                        dbContext.Quizzes.Update(existingQuiz);
                    }
                }

                dbContext.SaveChanges();

                MessageBox.Show("Викторина успешно сохранена в PostgreSQL!", "Успех");

                _questionsList.Clear();
                ClearQuestionForm();
                NavigationService.GoBack();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении: {ex.Message}", "Ошибка БД");
            }
        }
    }
}