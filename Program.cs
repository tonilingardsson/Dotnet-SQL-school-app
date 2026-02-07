using Microsoft.EntityFrameworkCore;
using Skola_ER_Application.Models;

namespace Skola_ER_Application
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            using var context = new ErSkolaContext();

            while (true)
            {
                Console.WriteLine("Please, choose one of the actions below");
                Console.WriteLine("1. Show all students");
                Console.WriteLine("2. Show students in a class");
                Console.WriteLine("3. Add new staff");
                Console.WriteLine("4. Show staff with a new role");
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

            /*using (var context = new ErSkolaContext())
            {
                // Show all students
                var allStudents = context.Students.ToList();

                foreach (var student in allStudents)
                {
                    Console.WriteLine($"{student.StudentFirstName} {student.StudentLastName} - {student.StudentPersonalNo}");
                }

                // Show a specific student                var studentToFind = context.Students.FirstOrDefault(student => student.StudentId == 1);

                if (studentToFind != null)
                {
                    Console.WriteLine($"Found student: {studentToFind.StudentFirstName} {studentToFind.StudentLastName} - {studentToFind.StudentPersonalNo}");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }*/

            /*using (var context = new ErSkolaContext())
            {
                // Create a new student
                var newStudent = new Student
                {
                    StudentFirstName = "Bat",
                    StudentLastName = "Man",
                    StudentPersonalNo = "198502022345",
                    ClassId = 1
                };

                *//*    // Save it on the database
                    context.Students.Add(newStudent);
                    context.SaveChanges();
                    Console.WriteLine("New student added.");*//*

                // Let's see if it can be read from the database
                var studentToFind = context.Students.FirstOrDefault(student => student.StudentFirstName == "Bat");

                if (studentToFind != null) 
                {
                    Console.WriteLine($"{studentToFind.StudentId}: {studentToFind.StudentFirstName} {studentToFind.StudentLastName}");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }*/

            /*// Update data/objects
            using (var context = new ErSkolaContext())
            {
                var studentToUpdate = context.Students.FirstOrDefault(s => s.StudentFirstName == "Bat");

                if (studentToUpdate != null)
                {
                    studentToUpdate.StudentFirstName = "Bruce";
                    studentToUpdate.StudentLastName = "Wayne";
                    studentToUpdate.StudentPersonalNo = "198502022345";
                    studentToUpdate.ClassId = 1;
                    // studentToUpdate.DateOfBirth = "1985-02-02"; wrong date format
                    studentToUpdate.Gender = "Man";
                    context.SaveChanges();
                    Console.WriteLine($"Student {studentToUpdate.StudentFirstName} updated!");

                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }*/

            // Delete data/objects
            // using (var context = new ErSkolaContext())
            
                /*var studentToDelete = context.Students.FirstOrDefault(s => s.StudentFirstName == "Bruce");

                if (studentToDelete != null)
                {
                    studentToDelete.StudentFirstName = "Bruce";
                    studentToDelete.StudentLastName = "Wayne";
                    studentToDelete.StudentPersonalNo = "198502022345";
                    studentToDelete.ClassId = 1;
                    context.SaveChanges();
                    Console.WriteLine($"Student {studentToDelete.StudentFirstName} deleted!");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }

                // Let's see if it can still be read from the database. It should be erased now.
                var studentToFind = context.Students.FirstOrDefault(student => student.StudentFirstName == "Bat");

                if (studentToFind != null)
                {
                    Console.WriteLine($"{studentToFind.StudentId}: {studentToFind.StudentFirstName} {studentToFind.StudentLastName}");
                }
                else
                {
                    Console.WriteLine("Student not found. It has been erased!");
                }*/


            

            // Add serveral students to the database
            /*using (var context = new ErSkolaContext())
            {
                var studentsToAdd = new List<Student>
                {
                    new Student
                    {
                        StudentFirstName = "Clark",
                        StudentLastName = "Kent",
                        StudentPersonalNo = "198503022345",
                        ClassId = 1
                    },
                    new Student
                    {
                        StudentFirstName = "Diana",
                        StudentLastName = "Prince",
                        StudentPersonalNo = "198504022345",
                        ClassId = 1
                    },
                    new Student
                    {
                        StudentFirstName = "Barry",
                        StudentLastName = "Allen",
                        StudentPersonalNo = "198505022345",
                        ClassId = 1
                    }
                };
                context.Students.AddRange(studentsToAdd);
                context.SaveChanges();
                Console.WriteLine("Several students added!");
            }*/

            /*using (var context = new ErSkolaContext()) 
            {
                // Fetch students with related data (include)
                var studentsWithSubjects = context.Students
                    .Include(student => student.Subject)
                    .ThenInclude(student => student.Grades)
                    .ToList();

                foreach (var student in studentsWithSubjects) 
                {
                    Console.WriteLine($"{student.StudentFirstName} {student.StudentLastName} - Subject: {student.Subject?.SubjectName}");
                    if (student.Subject != null) 
                    {
                        foreach (var grade in student.Subject.Grades) 
                        {
                            Console.WriteLine($"  Grade: {grade.GradeValue}");
                        }
                    }
                }
            }*/

            /*      // Build main menu
                  while (userIsLoggedIn)
              {

                  Console.WriteLine("1. Show all students");
                  Console.WriteLine("2. Show students in a class");
                  Console.WriteLine("3. Add new staff");
                  Console.WriteLine("4. Show staff with a role");
                  Console.WriteLine("5. Exit");
                  Console.Write("Choice: ");
                  var choice = Console.ReadLine();

                  switch(choice)
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
*/
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
