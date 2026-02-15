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
            JOIN Students AS s ON g.StudentId = s.StudentId
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
                    $"{reader["StudentFirstName"]} {reader["StudentLastName"]} got a " +
                    $"{reader["GradeValue"]}on {reader["SubjectName"]} the " + 
                    $"({date:yyyy-MM-dd}) " +
                    $"set by {reader["TeacherFirstName"]} {reader["TeacherLastName"]}.");
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
        public void ShowAverageSalaryPerDepartment()
        {
            const string sql = @"
                SELECT d.DepartmentName,
                AVG(s.Salary) AS AverageSalary
            FROM Staff s
            JOIN Departments d ON s.DepartmentId = d.DepartmentId
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
                    $"{reader["DepartmentName"]}: average {reader["AverageSalary"]} SEK/month");
            }
        }
        // - ShowStudentInfoById (SP)
        public void ShowStudentInfoById(int studentId)
        {
            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.sp_GetStudentInfo", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine(
                    $"{reader["StudentId"]}: {reader["StudentFirstName"]} {reader["StudentLastName"]} " +
                    $"- Class: {reader["ClassNAme"]}, PersonalNo: {reader["StudentPersonalNo"]}, Gender: {reader["Gender"]}"
                    );
            }
            else {
                Console.WriteLine("Student not found.");
            }
        }
        // - SetGradeWithTransaction
        public void SetGradeWithTransaction(int studentId, int subjectId, int teacherId, string gradeValue, DateTime gradeDate)
        {
            using var conn = CreateConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                const string sql = @"
INSERT INTO Grades (StudentId, SubjectId, TeacherId, GradeValue, GradeDate)
VALUES (@StudentId, @SubjectId, @TeacherId, @GradeValue, @GradeDate);";

                using var cmd = new SqlCommand(sql, conn, transaction);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                cmd.Parameters.AddWithValue("@GradeValue", gradeValue);
                cmd.Parameters.AddWithValue("@GradeDate", gradeDate);

                cmd.ExecuteNonQuery();

                // any additional checks could go here; if something fails, trow an exection

                transaction.Commit();
                Console.WriteLine("Grade saved.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Error while saving grade: " + ex.Message);
            }
        }
    }
}
