using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace HelpingHands.Models
{
    public class DapperORM
    {
        private static string connectionString = (@"Data Source=DESKTOP-8DP0U94; Initial Catalog=GRP-04-38-HelpingHandsDB; Integrated security= true;");

        public static void ExecuteWithoutReturn(string procedureName,DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                SqlCon.Execute(procedureName, param,commandType:CommandType.StoredProcedure);
            }
        }

        public static T ExecuteReturnScalar<T>(string procedureName,DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                return (T)Convert.ChangeType(SqlCon.ExecuteScalar(procedureName, param, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }
        public static IEnumerable <T> ReturnList<T> (string procedureName,DynamicParameters param = null)
        {
            using (SqlConnection SqlCon =new SqlConnection(connectionString))
            {
                SqlCon.Open();
                return SqlCon.Query<T>(procedureName, param, commandType:CommandType.StoredProcedure);
            }

        }

        public static T ReturnSingle<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                return SqlCon.QuerySingleOrDefault<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
        public static async Task<IEnumerable<T>> ReturnListAsync<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(connectionString))
            {
                SqlCon.Open();
                return await SqlCon.QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
