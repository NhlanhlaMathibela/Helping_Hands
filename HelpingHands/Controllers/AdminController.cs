 using Dapper;
using HelpingHands.Models;
using HelpingHands.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace HelpingHands.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserRepository _userRepository;

        public AdminController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult AddUsers(int id = 0)
        {
            if (id == 0)

                return View();

            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", id);
                return View(DapperORM.ReturnList<UserModel>("UsersViewById", param).FirstOrDefault<UserModel>());
            }
        }

        [HttpPost]
        public ActionResult AddUsers(UserModel user)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", user.UserId);
            param.Add("@Username", user.Username);
            param.Add("@EmailAddress", user.EmailAddress);
            param.Add("@Password", user.Password);
            param.Add("@ContactNumber", user.ContactNumber);
            param.Add("@UserType", user.UserType);

            DapperORM.ExecuteWithoutReturn("AddOrEditUsers", param);

            TempData["SuccessMessage"] = "Successfully added a new Employee";

            return View();
        }
        public IActionResult AllUsers(string userTypeFilter = null)
        {
            var users = DapperORM.ReturnList<UserModel>("ViewAllUsers");

            if (!string.IsNullOrEmpty(userTypeFilter))
            {
                users = users.Where(u => u.UserType == userTypeFilter).ToList();
            }

            return View(users);
        }

        public IActionResult ViewNurses()
        {
            return View(DapperORM.ReturnList<UserModel>("ViewNurses"));
        }
        public IActionResult ViewOfficeManager()
        {
            return View(DapperORM.ReturnList<UserModel>("ViewOfficeManager"));
        }
        public IActionResult Index()
        {
            return View(DapperORM.ReturnList<City>("ViewCities"));
        }
        [HttpGet]
        public ActionResult AddCities(int id = 0)
        {
           

                return View();
        }


        [HttpPost]
        public ActionResult AddCities(City city)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@CityName", city.CityName);
            param.Add("@Abbrev", city.Abbrev);
           

            DapperORM.ExecuteWithoutReturn("AddCities", param);

            return RedirectToAction("Index");
        }

    }
}
