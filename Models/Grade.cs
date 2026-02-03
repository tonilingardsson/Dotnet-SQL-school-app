using System;
using System.Collections.Generic;

namespace Skola_ER_Application.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public int TeacherId { get; set; }

    public string GradeValue { get; set; } = null!;

    public DateTime GradeDate { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual Staff Teacher { get; set; } = null!;
}
