using Dapper;
using HelpingHands.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpingHands.Views.Dashboards
{
    public class PatientDashboardController : Controller
    {

        public IActionResult Home()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                var patientDetails = DapperORM.ReturnList<UserModel>("ViewDetails", parameters).FirstOrDefault();
                return View(patientDetails);
            }

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
