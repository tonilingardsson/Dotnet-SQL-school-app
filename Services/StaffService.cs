using Skola_ER_Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

namespace Skola_ER_Application.Services;

public static class StaffService
{
    public static void AddNewStaff(ErSkolaContext context)
    {
        Console.Write("First name: ");
        var firstName = Console.ReadLine()?.Trim();

        Console.Write("Last name: ");
        var lastName = Console.ReadLine()?.Trim();

        Console.Write("Personal number: ");
        var personalNo = Console.ReadLine()?.Trim();

        Console.Write("Contract start date (YYYY-MM-DD): ");
        var contractStartDate = Console.ReadLine()?.Trim();

        // Choose role
        var roles = context.Roles.ToList();
        Console.WriteLine("Available roles:");
        foreach (var r in roles)
            Console.WriteLine($"{r.RoleId}: {r.RoleName}");

        Console.Write("Choose role ID: ");
        if (!int.TryParse(Console.ReadLine(), out int roleId))
        {
            Console.WriteLine("Invalid role!");
            return;
        }

        // Choose department
        var departments = context.Departments.ToList();
        Console.WriteLine("Available departments:");
        foreach (var d in departments)
            Console.WriteLine($"{d.DepartmentId}: {d.DepartmentName}");

        Console.Write("Choose department ID: ");
        if (!int.TryParse(Console.ReadLine(), out int departmentId))
        {
            Console.WriteLine("Invalid department!");
            return;
        }

        var staff = new Staff
        {
            StaffFirstName = firstName ?? "",
            StaffLastName = lastName ?? "",
            StaffPersonalNo = personalNo ?? "",
            ContractStartDate = string.IsNullOrWhiteSpace(contractStartDate)
                ? null
                : DateOnly.Parse(contractStartDate),
            RoleId = roleId,
            DepartmentId = departmentId
        };

        context.Staff.Add(staff);
        context.SaveChanges();

        Console.WriteLine("Staff added!");
    }

    public static void ShowStaffWithRole(ErSkolaContext context)
    {
        var staffList = context.Staff
            .Include(s => s.Role)
            .ToList();

        foreach (var staff in staffList)
            Console.WriteLine($"{staff.StaffFirstName} {staff.StaffLastName} - {staff.Role.RoleName} since {staff.ContractStartDate}");
    }

    public static void ShowTeacherCountPerDepartment(ErSkolaContext context)
    {
        var query = context.Staff
            .Where(s => s.Role.RoleName == "Teacher")
            .GroupBy(s => s.Department)
            .Select(g => new { Department = g.Key, TeacherCount = g.Count() })
            .ToList();

        // Let's count how many teachers
        foreach (var assignation in query)
        {
            Console.WriteLine($"{assignation.Department}: {assignation.TeacherCount} teachers");
        }
    }
}
