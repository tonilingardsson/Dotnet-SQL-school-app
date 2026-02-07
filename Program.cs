using Skola_ER_Application.Models;
using Skola_ER_Application.Services;

namespace Skola_ER_Application
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var context = new ErSkolaContext();

            while (true)
            {
                Console.WriteLine("1. Show all students");
                Console.WriteLine("2. Show students in a class");
                Console.WriteLine("3. Add new staff");
                Console.WriteLine("4. Show staff with a role");
                Console.WriteLine("0. Exit");
                Console.Write("Choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StudentService.ShowAllStudents(context);
                        break;
                    case "2":
                        StudentService.ShowStudentsInClass(context);
                        break;
                    case "3":
                        StaffService.AddNewStaff(context);
                        break;
                    case "4":
                        StaffService.ShowStaffWithRole(context);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("You must type a number 0–4");
                        break;
                }
            }
        }
    }
}
