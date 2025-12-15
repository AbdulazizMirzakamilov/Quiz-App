using System;
using System.Collections.Generic;

namespace BDQuiz.Models;

public partial class Quiz
{
    public int Quizid { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
}
