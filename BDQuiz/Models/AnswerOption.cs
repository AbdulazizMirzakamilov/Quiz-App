using System;
using System.Collections.Generic;

namespace BDQuiz.Models;

public partial class AnswerOption
{
    public int Answerid { get; set; }

    public int Questionid { get; set; }

    public string Answertext { get; set; } = null!;

    public bool Iscorrect { get; set; }

    public virtual ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();

    public virtual Question Question { get; set; } = null!;
}
