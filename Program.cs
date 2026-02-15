using Skola_ER_Application.Models;
using Skola_ER_Application.Services;
using Skola_ER_Application.DataAccess;
using System;

namespace Skola_ER_Application
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var context = new ErSkolaContext();

            var connectionString = "Server=.;Database=ER_Skola;Trusted_Connection=True;TrustServerCertificate=True;";
            var adoRepo = new SchoolAdoRepository(connectionString);

            while (true)
            {
                Console.WriteLine("1. Show all students");
                Console.WriteLine("2. Show students in a class");
                Console.WriteLine("3. Show grades for student");
                Console.WriteLine("4. Show all info of a student");
                Console.WriteLine("5. Show all staff");
                Console.WriteLine("6. Add new staff");
                Console.WriteLine("7. Total salary per department");
                Console.WriteLine("8. Average salary per department");
                Console.WriteLine("9. Set grade with Transaction");

                Console.WriteLine("6. Exit");
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
                        Console.WriteLine("Student ID: ");
                        int.TryParse(Console.ReadLine(), out var sid);
                        adoRepo.ShowGradesForStudent(sid);
                        break;
                    case "4":
                        Console.WriteLine("Student ID: ");
                        int.TryParse(Console.ReadLine(), out sid);
                        adoRepo.ShowStudentInfoById(sid);
                        break;
                    case "5":
                        StaffService.ShowStaffWithRole(context);
                        break;
                    case "6":
                        StaffService.AddNewStaff(context);
                        break;
                    case "7":
                        adoRepo.ShowTotalSalaryPerDepartment();
                        break;
                    case "8":
                        adoRepo.ShowAverageSalaryPerDepartment();
                        break;

                        // StaffService.ShowTeacherCountPerDepartment(context);
                        // break;
                    case "9":
                        // TODO after SetGradeWithTransaction exists
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("You must type a number 0-9");
                        break;
                }
            }
        }
    }
}
