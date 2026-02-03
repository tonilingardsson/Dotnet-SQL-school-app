using System;
using System.Collections.Generic;

namespace Skola_ER_Application.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentFirstName { get; set; } = null!;

    public string StudentLastName { get; set; } = null!;

    public string StudentPersonalNo { get; set; } = null!;

    public int ClassId { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public int? SubjectId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Subject? Subject { get; set; }
}
