using System;
using System.Collections.Generic;

namespace BDQuiz.Models;

public partial class User
{
    public int Userid { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public bool? Isadmin { get; set; }
}
