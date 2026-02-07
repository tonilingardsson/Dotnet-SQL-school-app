using Skola_ER_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Skola_ER_Application.Services;

public static class StudentService
{
    public static void ShowAllStudents(ErSkolaContext context)
    {
        Console.WriteLine("Sort by: 1 = First name, 2 = Last name");
        var sortField = Console.ReadLine();
        Console.WriteLine("Order: 1 = Ascending, 2 = Descending");
        var sortOrder = Console.ReadLine();

        var query = context.Students.AsQueryable();

        bool byFirst = sortField == "1";
        bool ascending = sortOrder == "1";

        if (byFirst && ascending)
            query = query.OrderBy(s => s.StudentFirstName);
        else if (byFirst && !ascending)
            query = query.OrderByDescending(s => s.StudentFirstName);
        else if (!byFirst && ascending)
            query = query.OrderBy(s => s.StudentLastName);
        else
            query = query.OrderByDescending(s => s.StudentLastName);

        var students = query.ToList();

        foreach (var s in students)
            Console.WriteLine($"{s.StudentFirstName} {s.StudentLastName}");
    }

    public static void ShowStudentsInClass(ErSkolaContext context)
    {
        var classes = context.Classes.ToList();
        foreach (var c in classes)
            Console.WriteLine($"{c.ClassId}: {c.ClassName}");

        Console.Write("Enter class ID: ");
        if (!int.TryParse(Console.ReadLine(), out int classId))
        {
            Console.WriteLine("Invalid ID!");
            return;
        }

        var query = context.Students.Where(s => s.ClassId == classId);

        Console.WriteLine("Sort by: 1 = First name, 2 = Last name");
        var sortField = Console.ReadLine();
        Console.WriteLine("Order: 1 = Ascending, 2 = Descending");
        var sortOrder = Console.ReadLine();

        bool byFirst = sortField == "1";
        bool ascending = sortOrder == "1";

        if (byFirst && ascending)
            query = query.OrderBy(s => s.StudentFirstName);
        else if (byFirst && !ascending)
            query = query.OrderByDescending(s => s.StudentFirstName);
        else if (!byFirst && ascending)
            query = query.OrderBy(s => s.StudentLastName);
        else
            query = query.OrderByDescending(s => s.StudentLastName);

        var students = query.ToList();

        foreach (var s in students)
            Console.WriteLine($"{s.StudentFirstName} {s.StudentLastName}");
    }
}
