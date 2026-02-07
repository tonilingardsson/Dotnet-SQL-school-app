using Microsoft.EntityFrameworkCore;
using Skola_ER_Application.Models;
using System;

namespace Skola_ER_Application
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            while (true)
            {
                using var context = new ErSkolaContext();

                Console.WriteLine("Please, choose one of the actions below");
                Console.WriteLine("1. Show all students");
                Console.WriteLine("2. Show students in a class");
                Console.WriteLine("3. Add new staff");
                Console.WriteLine("4. Show staff with their role");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllStudents(context);
                        break;
                    case "2":
                        ShowStudentsInClass(context);
                        break;
                    case "3":
                        AddNewStaff(context);
                        break;
                    case "4":
                        ShowStaffWithRole(context);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("You must type a number 1-5");
                        break;
                }
            }
        }
                      private static void ShowAllStudents(ErSkolaContext context)
          {
              Console.WriteLine("Sort by: 1 = First name, 2 = Last name");
              var sortField = Console.ReadLine();
              Console.WriteLine("Order: 1 = Ascending, 2 = Descending");
              var sortOrder = Console.ReadLine();

              var query = context.Students.AsQueryable();

              bool byFirst = sortField == "1";
              bool ascending = sortOrder == "1";

              if (byFirst && ascending)
              {
                  query = query.OrderByDescending(s => s.StudentFirstName);
              }
              else if (byFirst && !ascending)
              {
                  query = query.OrderByDescending(s => s.StudentFirstName);
              }
              else if (!byFirst && ascending)
              {
                  query = query.OrderBy(s => s.StudentLastName);
              }
              else
              {
                  query = query.OrderByDescending(s => s.StudentLastName);
              }

              var students = query.ToList();

              foreach (var s in students)
                  Console.WriteLine($"{s.StudentFirstName} {s.StudentLastName}");
          }

          private static void ShowStudentsInClass(ErSkolaContext context)
          {
              // 1 List classes
              var classes = context.Classes.ToList();
              foreach (var c in classes)
              {
                  Console.WriteLine($"{c.ClassId}: {c.ClassName}");
              }

              Console.WriteLine("Enter class ID: ");
              if (!int.TryParse(Console.ReadLine(), out int classId))
              {
                  Console.WriteLine("Invalid ID!");
                  return;
              }

              var query = context.Students
                  .Where(s => s.ClassId == classId);

              // Sorting the query results
              Console.WriteLine("Sort by: 1 = First name, 2 = Last name");
              var sortField = Console.ReadLine();
              Console.WriteLine("Order: 1 = Ascending, 2 = Descending");
              var sortOrder = Console.ReadLine();

              bool byFirst = sortField == "1";
              bool ascending = sortOrder == "1";

              if (byFirst && ascending)
              {
                  query = query.OrderBy(s => s.StudentFirstName);
              }
              else if (byFirst && !ascending)
              {
                  query = query.OrderByDescending(s => s.StudentFirstName);
              }
              else if (!byFirst && ascending)
              {
                  query = query.OrderBy(s => s.StudentLastName);
              }
              else
              {
                  query = query.OrderByDescending(s => s.StudentLastName);
              }

              var students = query.ToList();

              foreach (var s in students)
              {
                  Console.WriteLine($"{s.StudentFirstName} {s.StudentLastName}");
              }
          }

          // Add a new staff member
          private static void AddNewStaff(ErSkolaContext context)
          {
              Console.WriteLine("First name: ");
              var firstName = Console.ReadLine()?.Trim();
              Console.WriteLine("Last name: ");
              var lastName = Console.ReadLine()?.Trim();
              Console.WriteLine("Personal number: ");
              var personalNo = Console.ReadLine()?.Trim();

              // List roles
              var roles = context.Roles.ToList();
              Console.WriteLine("Available roles: ");
              foreach (var r in roles)
              {
                  Console.WriteLine($"{r.RoleId}: {r.RoleName}");
              }

              Console.WriteLine("Choose role ID: ");
              if (!int.TryParse(Console.ReadLine(), out int roleId))
              {
                  Console.WriteLine("Invalid role!");
                  return;
              }

              // Create new staff object
              var staff = new Staff
              {
                  StaffFirstName = firstName ?? "",
                  StaffLastName = lastName ?? "",
                  StaffPersonalNo = personalNo ?? "",
                  RoleId = roleId 
              };

              // Add to database
              context.Staff.Add(staff);
              context.SaveChanges();

              Console.WriteLine("Staff added!");
          }

          private static void ShowStaffWithRole(ErSkolaContext context)
          {
              var staffList = context.Staff.
                  Include(s => s.Role)
                  .ToList();

              foreach (var staff in staffList)
              {
                  Console.WriteLine($"{staff.StaffFirstName} {staff.StaffLastName} - {staff.Role.RoleName}");
              }
          
           // Console.ReadKey();
        }
    }
}
