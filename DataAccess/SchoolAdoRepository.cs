using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Skola_ER_Application.DataAccess
{
    public class SchoolAdoRepository
    {
        private readonly string _connectionString; //_tableName

        public SchoolAdoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection CreateConnection() // _connection
        {
            return new SqlConnection(_connectionString);
        }

        // - ShowStaffOverview
        // - ShowGradesForStudent
        public void ShowGradesForStudent(int studentId)
        {
            const string sql = @"
            SELECT
                s.StudentFirstName,
                s.StudentLastName,
                subj.SubjectName,
                g.GradeValue,
                g.GradeDate,
                t.StaffFirstName AS TeacherFirstName,
                t.StaffLastName AS TeacherLastName
            FROM Grades AS g
            JOIN Student AS s ON g.SubjectId = s.StudentId
            JOIN Subjects AS subj ON g.SubjectId = subj.SubjectId
            JOIN Staff AS t ON g.TeacherId = t.StaffId
            WHERE s.StudentId = @StudentId
ORDER BY subj.SubjectName, g.GradeDate;";

            using var conn = CreateConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (!reader.HasRows) 
            {
                Console.WriteLine("No grades found for this student.");
                return;
            }

            while (reader.Read())
            {
                var date = (DateTime)reader["GradeDate"];
                Console.WriteLine(
                    $"{reader["StudentFirstName"]} {reader["StudentLastName"]} - " +
                    $"{reader["SubjectName"]} : {reader["GradeValue"]}" + 
                    $"({date:yyyy-MM-dd}) " +
                    $"by {reader["TeacherFirstName"]} + {reader["TeacherLastName"]}");
            }
        }
        // - ShowTotalSalaryPerDepartment
        public void ShowTotalSalaryPerDepartment()
        {
            const string sql = @"
                SELECT d.DepartmentName,
                    SUM(s.Salary) AS TotalMonthlySalary
                FROM Staff s
                JOIN Departments d ON  s.DepartmentId = d.DepartmentId
                GROUP BY d.DepartmentName;";

            using var conn = CreateConnection();
            using var cmd = new SqlCommand(sql, conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine("No salary data found.");
                return;
            }

            while (reader.Read())
            {
                Console.WriteLine(
                    $"{reader["DepartmentName"]}: {reader["TotalMonthlySalary"]} SEK/month");
            }
        }
        // - ShowAverageSalaryPerDepartment
        // - ShowStudentInfoById (SP)
        // - SetGradeWithTransaction
    }
}
