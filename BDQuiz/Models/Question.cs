using System;
using System.Collections.Generic;

namespace BDQuiz.Models;

public partial class Question
{
    public int Questionid { get; set; }

    public int Quizid { get; set; }

    public string Questiontext { get; set; } = null!;

    public virtual ICollection<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();

    public virtual ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();

    public virtual Quiz Quiz { get; set; } = null!;
}
