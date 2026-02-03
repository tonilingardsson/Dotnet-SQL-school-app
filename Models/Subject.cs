using System;
using System.Collections.Generic;

namespace Skola_ER_Application.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
