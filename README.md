```
# Skola_ER_Application for my studies at Campus Varberg (Sweden) as a .NET developer

A console-based school administration system built in C# (.NET) with SQL Server.  
The project demonstrates using both Entity Framework Core (ORM) and ADO.NET (raw SQL, stored procedures, transactions) against the same database.

---

## Overview

The application manages a fictional school and allows staff/administrators to:

- Navigate a console **menu** to run different queries and operations.
- Use **Entity Framework Core** to read and update data in an object-oriented way.
- Use **ADO.NET** to execute SQL queries, stored procedures, and transactions.

This project is the final practical assignment in a database course.

---

## Technologies

- C# (.NET console application)
- SQL Server
- Entity Framework Core (database-first)
- ADO.NET (SqlConnection, SqlCommand, SqlDataReader, transactions)

---

## Database model (short description)

Main tables:

- **Students**: basic student info (name, personal number, date of birth, gender, ClassId, SubjectId)
- **Classes**: class name and mentor (StaffId)
- **Staff**: employees (name, personal number, role, department, contract start date, salary)
- **Roles**: staff roles (Teacher, Principal, Administrator, etc.)
- **Departments**: departments/avdelningar in the school
- **Subjects**: subjects/courses
- **Grades**: grades given to students in subjects, with teacher and date

There are foreign keys between:

- Students → Classes, Subjects  
- Grades → Students, Subjects, Staff (TeacherId)  
- Staff → Roles, Departments  
- Classes → Staff (MentorId)

This structure supports all required queries: grades per student, teachers per department, salary per department, etc.

---

## Features

### Entity Framework (EF) features

These use `ErSkolaContext` and the EF entity classes.

- **Main menu navigation**
  - A looped console menu where the user chooses functions by number.

- **Show all students**
  - Lists all students.
  - User can choose sorting:
    - by first name or last name
    - ascending or descending.

- **Show students in a specific class**
  - Lists available classes.
  - User selects a class.
  - Shows all students in that class, with optional sorting (as above).

- **Add new staff member**
  - Asks for:
    - First name
    - Last name
    - Personal number
    - Contract start date
    - Role (selected from Roles table)
    - Department (selected from Departments table)
  - Saves the new staff member to the database using EF.

- **Show all staff with roles**
  - Shows each staff member’s full name and their role name.

- **Show all active subjects**
  - Lists all subjects from the `Subjects` table (treated as active courses).

- **Update a student (extra challenge)**
  - Allows the user to select a student by ID.
  - Prompts for new values (first name, last name, etc.).
  - Updates the student via EF and saves changes.

---

### ADO.NET features

These use `SqlConnection`, `SqlCommand`, `SqlDataReader`, and sometimes transactions.

- **Staff overview and management**
  - Lists staff with:
    - Name
    - Role
    - Department
    - Number of years at the school (based on contract start date)
  - Adds new staff via INSERT with parameters.

- **Grades per student (with teacher and date)**
  - User enters a student ID.
  - Shows all grades for that student:
    - Subject
    - Grade value
    - Date
    - Teacher name
  - Handles the case where a student has no grades.

- **Total salary per department (monthly)**
  - Aggregates salary per department using `SUM(s.Salary)` and `GROUP BY DepartmentName`.
  - Shows how much each department pays out per month.

- **Average salary per department**
  - Uses `AVG(s.Salary)` grouped by department.
  - Shows the average salary in each department.

- **Stored procedure: get student info by Id**
  - Stored procedure `sp_GetStudentInfo` (SQL Server):
    - Input: `@StudentId`.
    - Output: student information (name, personal number, gender, date of birth, class).
  - Called from C# using ADO.NET and printed in the console.

- **Set grade using a transaction**
  - Asks for:
    - StudentId
    - SubjectId
    - TeacherId
    - Grade value
    - Grade date
  - Uses a `SqlTransaction` to insert into `Grades`.
  - On success: commit; on error: rollback and show error message.

---

### Extra challenges

Depending on how far you implement them, you may have:

- **Detailed student view (ADO.NET)**
  - For a given student:
    - Show basic info
    - Class
    - Subjects and teachers
    - Grades with dates
  - Implemented with a multi-join SELECT over Students, Classes, Grades, Subjects, Staff.

- **View: teachers and their subjects (ADO.NET)**
  - A SQL View (e.g. `v_TeachersWithSubjects`) that lists:
    - Teacher name
    - Subject taught
  - Read from the view via ADO.NET.

## How to run

1. **Database**
   - Create a SQL Server database named `ER_Skola`.
   - Run the provided SQL script (in the repo) to:
     - Create/alter tables
     - Add foreign keys
     - Seed example data
     - Create stored procedures and views.

2. **Application**
   - Open the solution in Visual Studio.
   - Ensure the connection string in `ErSkolaContext.OnConfiguring` (and the ADO.NET repository) points to your SQL Server instance.
   - Set the console project (`Skola_ER_Application`) as startup project.
   - Run the application.

3. **Usage**
   - Use the console menu to:
     - Browse students, staff, and subjects via EF.
     - Run administrative queries via ADO.NET (grades, salaries, stored procedures, transactions).

---

## Structure

- `Models/`
  - EF Core entity classes (Students, Staff, Roles, Departments, Subjects, Grades, Classes)
  - `ErSkolaContext` DbContext (database-first, scaffolded)
- `Services/`
  - `StudentService` – EF logic for student-related features.
  - `StaffService` – EF logic for staff-related features.
  - (Optional) `CourseService` – EF logic for subjects/courses.
- `DataAccess/`
  - `SchoolAdoRepository` – ADO.NET code for:
    - Staff overview
    - Grades per student
    - Salary aggregations
    - Stored procedure calls
    - Transactions when setting grades
- `Program.cs`
  - Main entry point.
  - Console menu.
  - Wires together EF services and ADO.NET repository.

---

## Notes for teachers/reviewers

- The project intentionally mixes **Entity Framework Core** and **ADO.NET** to show both ORM and lower-level data access.
- Complex SQL (aggregates, joins, stored procedures, transactions) is implemented on the ADO.NET side.
- The EF model is database-first and aligned with the SQL schema used in previous labs.
- The code is organized so Program.cs only contains menu/navigation; logic is in services and the ADO.NET repository.

---

## Future improvements (beyond the assignment)

- Move the connection string out of source code into configuration.
- Add more input validation and error handling in the console UI.
- Add unit tests for service and repository methods.
- Replace the console UI with a web or desktop UI while reusing the same data layer.
```