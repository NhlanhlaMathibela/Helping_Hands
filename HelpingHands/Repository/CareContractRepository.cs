using Dapper;
using HelpingHands.Models;
using System.Data;
using System.Data.SqlClient;

namespace HelpingHands.Repository
{
    public class CareContractRepository
    {

        //public async Task<IEnumerable<CareContractPatient>> GetNewContracts()
        //{
        //    using (var connection = new SqlConnection(@"Data Source=DESKTOP-8DP0U94; Initial Catalog=GRP-04-38-HelpingHandsDB; Integrated security= true;"))
        //    {
        //        connection.Open();
        //        var results = await connection.QueryAsync<CareContractPatient>("NewContracts", commandType: CommandType.StoredProcedure);
        //        return results;


        //    }
        
        public async Task<IEnumerable<CareContractPatient>> GetNewContracts()
        {
            var results = DapperORM.ReturnList<CareContractPatient>("GetNewContracts");
            return results;
        }


        //public static IEnumerable<T> ReturnList<T>(string procedureName, object parameters = null)
        //{
        //    using (IDbConnection db = new SqlConnection(@"Data Source=DESKTOP-8DP0U94; Initial Catalog=GRP-04-38-HelpingHandsDB; Integrated security= true;"))
        //    {
        //        db.Open();
        //        return db.Query<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
        //    }
        //}

    }
}
