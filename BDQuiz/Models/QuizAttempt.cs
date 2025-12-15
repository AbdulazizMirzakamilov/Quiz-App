using System;
using System.Collections.Generic;

namespace BDQuiz.Models;

public partial class QuizAttempt
{
    public int Attemptid { get; set; }

    public int Quizid { get; set; }

    public string? Username { get; set; }

    public DateTime? Startedat { get; set; }

    public int? Score { get; set; }

    public int? Totalquestions { get; set; }

    public virtual ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();

    public virtual Quiz Quiz { get; set; } = null!;
}
