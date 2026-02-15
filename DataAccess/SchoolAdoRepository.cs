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
        // - ShowTotalSalaryPerDepartment
        // - ShowAverageSalaryPerDepartment
        // - ShowStudentInfoById (SP)
        // - SetGradeWithTransaction
    }
}
