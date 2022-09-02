using Dapper;
using System.Data.SqlClient;

namespace Uniom.MedicationManager.Infrastructure.Migrations
{
    public class Database
    {
        public static void CreateDatabase(string connectionWithoutDataBase, string nameDatabase)
        {
            using var conn = new SqlConnection(connectionWithoutDataBase);

            conn.Open();

            var query = "SELECT COUNT(*) FROM sys.databases WHERE name = @Name";
            var param = new { Name = nameDatabase };
            var hasDatabase = conn.ExecuteScalar<int>(query, param) != 0;
            //teste
            if (!hasDatabase)
            {
                conn.Close();
                using var conn2 = new SqlConnection(connectionWithoutDataBase);
                conn2.Open();
                var databaseQuery = $"CREATE DATABASE {nameDatabase}";
                conn2.Execute(databaseQuery);
            }

        }
    }
}
