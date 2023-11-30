using Dapper;
using HelpingHands.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace HelpingHands.Repository
{
    public class SuburbRepository
    {


        public async Task<IEnumerable<CitySuburb>> GetSuburbsWithCities()
        {
            var suburbsWithCities = await DapperORM.ReturnListAsync<CitySuburb>("ViewCitiesWithSuburbs");
            return suburbsWithCities;
        }

        public List<Suburbs> GetSuburbs()
        {
            var suburbs = DapperORM.ReturnList<Suburbs>("GetSuburbs");
            return suburbs.ToList();
        }



    }

}
