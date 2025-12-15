using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BDQuiz.Models;

public partial class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnswerOption> AnswerOptions { get; set; }

    public virtual DbSet<AttemptAnswer> AttemptAnswers { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizAttempt> QuizAttempts { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=QuizApp;Username=postgres;Password=Alikom.31");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnswerOption>(entity =>
        {
            entity.HasKey(e => e.Answerid).HasName("answeroption_pkey");

            entity.ToTable("AnswerOption");

            entity.Property(e => e.Answerid)
                .HasDefaultValueSql("nextval('answeroption_answerid_seq'::regclass)")
                .HasColumnName("answerid");
            entity.Property(e => e.Answertext).HasColumnName("answertext");
            entity.Property(e => e.Iscorrect).HasColumnName("iscorrect");
            entity.Property(e => e.Questionid).HasColumnName("questionid");

            entity.HasOne(d => d.Question).WithMany(p => p.AnswerOptions)
                .HasForeignKey(d => d.Questionid)
                .HasConstraintName("fk_answeroption_question");
        });

        modelBuilder.Entity<AttemptAnswer>(entity =>
        {
            entity.HasKey(e => e.Attemptanswerid).HasName("attemptanswer_pkey");

            entity.ToTable("AttemptAnswer");

            entity.Property(e => e.Attemptanswerid)
                .HasDefaultValueSql("nextval('attemptanswer_attemptanswerid_seq'::regclass)")
                .HasColumnName("attemptanswerid");
            entity.Property(e => e.Answerid).HasColumnName("answerid");
            entity.Property(e => e.Attemptid).HasColumnName("attemptid");
            entity.Property(e => e.Iscorrect).HasColumnName("iscorrect");
            entity.Property(e => e.Questionid).HasColumnName("questionid");

            entity.HasOne(d => d.Answer).WithMany(p => p.AttemptAnswers)
                .HasForeignKey(d => d.Answerid)
                .HasConstraintName("fk_attemptanswer_answer");

            entity.HasOne(d => d.Attempt).WithMany(p => p.AttemptAnswers)
                .HasForeignKey(d => d.Attemptid)
                .HasConstraintName("fk_attemptanswer_attempt");

            entity.HasOne(d => d.Question).WithMany(p => p.AttemptAnswers)
                .HasForeignKey(d => d.Questionid)
                .HasConstraintName("fk_attemptanswer_question");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Questionid).HasName("question_pkey");

            entity.ToTable("Question");

            entity.Property(e => e.Questionid)
                .HasDefaultValueSql("nextval('question_questionid_seq'::regclass)")
                .HasColumnName("questionid");
            entity.Property(e => e.Questiontext).HasColumnName("questiontext");
            entity.Property(e => e.Quizid).HasColumnName("quizid");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.Quizid)
                .HasConstraintName("fk_question_quiz");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Quizid).HasName("quiz_pkey");

            entity.ToTable("Quiz");

            entity.Property(e => e.Quizid)
                .HasDefaultValueSql("nextval('quiz_quizid_seq'::regclass)")
                .HasColumnName("quizid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
        });

        modelBuilder.Entity<QuizAttempt>(entity =>
        {
            entity.HasKey(e => e.Attemptid).HasName("quizattempt_pkey");

            entity.ToTable("QuizAttempt");

            entity.Property(e => e.Attemptid)
                .HasDefaultValueSql("nextval('quizattempt_attemptid_seq'::regclass)")
                .HasColumnName("attemptid");
            entity.Property(e => e.Quizid).HasColumnName("quizid");
            entity.Property(e => e.Score)
                .HasDefaultValue(0)
                .HasColumnName("score");
            entity.Property(e => e.Startedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startedat");
            entity.Property(e => e.Totalquestions).HasColumnName("totalquestions");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizAttempts)
                .HasForeignKey(d => d.Quizid)
                .HasConstraintName("fk_quizattempt_quiz");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("Users_pkey");

            entity.HasIndex(e => e.Email, "Users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "Users_username_key").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Isadmin)
                .HasDefaultValue(false)
                .HasColumnName("isadmin");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
