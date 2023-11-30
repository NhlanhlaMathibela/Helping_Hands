using Dapper;
using HelpingHands.Models;
using HelpingHands.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HelpingHands.Controllers
{
    public class Dashboards : Controller
    {
        private readonly UserRepository _userRepository;

        public Dashboards(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }




        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Admin()
        {
            int userCount = _userRepository.CountUsers();
            return View();
        }
        [HttpGet]
        public IActionResult OfficeManager()
        {
            
            {
               var parameters = new DynamicParameters();
               var Contracts = DapperORM.ReturnList<OfficeManager>("NewContractsManager", parameters);
               return View(Contracts);
            }
            
        }
        [HttpGet]
        public IActionResult Nurse()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                var nurseDetails = DapperORM.ReturnList<UserModel>("ViewDetails", parameters).FirstOrDefault();
                return View(nurseDetails);
            }

            return View();
        }
        public IActionResult Patient()
        {
           
            return View();
        }
    }
}
