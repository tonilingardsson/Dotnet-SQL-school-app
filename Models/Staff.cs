using System;
using System.Collections.Generic;

namespace Skola_ER_Application.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string StaffFirstName { get; set; } = null!;

    public string StaffLastName { get; set; } = null!;

    public string StaffPersonalNo { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Department { get; set; }

    public decimal? Salary { get; set; }

    public DateOnly? ContractStartDate { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Role Role { get; set; } = null!;
}
