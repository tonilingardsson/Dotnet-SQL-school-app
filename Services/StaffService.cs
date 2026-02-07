using Skola_ER_Application.Models;
using Microsoft.EntityFrameworkCore;

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

        var roles = context.Roles.ToList();
        Console.WriteLine("Available roles: ");
        foreach (var r in roles)
            Console.WriteLine($"{r.RoleId}: {r.RoleName}");

        Console.Write("Choose role ID: ");
        if (!int.TryParse(Console.ReadLine(), out int roleId))
        {
            Console.WriteLine("Invalid role!");
            return;
        }

        var staff = new Staff
        {
            StaffFirstName = firstName ?? "",
            StaffLastName = lastName ?? "",
            StaffPersonalNo = personalNo ?? "",
            RoleId = roleId
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
            Console.WriteLine($"{staff.StaffFirstName} {staff.StaffLastName} - {staff.Role.RoleName}");
    }
}
