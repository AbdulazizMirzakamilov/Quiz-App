using System;
using System.Collections.Generic;

namespace BDQuiz.Models;

public partial class AttemptAnswer
{
    public int Attemptanswerid { get; set; }

    public int Attemptid { get; set; }

    public int Questionid { get; set; }

    public int Answerid { get; set; }

    public bool Iscorrect { get; set; }

    public virtual AnswerOption Answer { get; set; } = null!;

    public virtual QuizAttempt Attempt { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
