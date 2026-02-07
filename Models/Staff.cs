using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Skola_ER_Application.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string StaffFirstName { get; set; } = null!;

    public string StaffLastName { get; set; } = null!;

    public string StaffPersonalNo { get; set; } = null!;

    // Foreign key to Roles table
    public int RoleId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    // Navigation property to Role entity (which is not set on the database side)
    public virtual Role Role { get; set; } = null!;

}
