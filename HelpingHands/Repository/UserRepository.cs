using Dapper;
using System.Data.SqlClient;
using System.Data;
using HelpingHands.Models;
using static Dapper.SqlMapper;



namespace HelpingHands.Repository
{
    public class UserRepository
    {

        public UserModel Authenticate(string username, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Username", username);
            parameters.Add("@Password", password);

            var user = DapperORM.ReturnSingle<UserModel>("AuthenticateUser", parameters);

            return user;
        }


        public int CountUsers() 
        {
            var count = DapperORM.ReturnSingle<int>("CountUsers");
            return count;
        }


        public List<Conditions> GetConditions()
        {
            var conditions = DapperORM.ReturnList<Conditions>("GetConditions");
            return conditions.ToList();
        }



        public List<Nurse> GetNurses()
        {
            var nurses = DapperORM.ReturnList<Nurse>("GetNurses");
            return nurses.ToList();
        }


        public void SignOutUser(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            DapperORM.ExecuteWithoutReturn("SignOutUser", parameters);
        }







    }


}
