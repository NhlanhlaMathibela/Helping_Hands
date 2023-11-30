using Dapper;
using HelpingHands.Models;
using HelpingHands.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace HelpingHands.Controllers
{
    public class UserController : Controller
    {

       
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public ActionResult UserCount()
        {
            int count = _userRepository.CountUsers();
            return View(count);
        }
        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userRepository.Authenticate(username, password);

            if (user != null)
            {
                string UserId = user.UserId.ToString();
                HttpContext.Session.SetString("UserId", UserId);

                HttpContext.Session.SetString("UserType", user.UserType );
           

                if (user.UserType == "A")
                {
                   
                    return RedirectToAction( "Admin", "Dashboards");
                    
                }
                else if (user.UserType == "O")
                {
                    return RedirectToAction("OfficeManager", "Dashboards");
                }
                else if (user.UserType == "N")
                {
                    return RedirectToAction("Nurse", "Dashboards");
                }
                else
                { 
                    return RedirectToAction("Home", "PatientDashboard");
                }

                
            }
            else
            {
                TempData["ErrorMessage"] = "Password or Username incorrect";
            }
          
            return View();


        }

        public IActionResult Index()
        {
            return View(DapperORM.ReturnList<UserModel>("ViewAllUsers"));
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

            return RedirectToAction("Index");
        }
        public ActionResult RegisterSuccess()
        {
            return View(); 
        }
        [HttpGet]
        public ActionResult RegisterPatients()
        {
             return View();

        }
        [HttpPost]
        public ActionResult RegisterPatients(UserModel users)
        {
         
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", users.UserId);
            param.Add("@Username", users.Username);
            param.Add("@EmailAddress", users.EmailAddress);
            param.Add("@Password", users.Password);
            param.Add("@ContactNumber", users.ContactNumber);

           
            DapperORM.ExecuteWithoutReturn("RegisterPatient", param);
            TempData["SuccessMessage"] = "Successfully Registered";

            return View();
        }

       

       

    }
}
