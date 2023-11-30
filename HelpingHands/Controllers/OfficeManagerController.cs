using Dapper;
using HelpingHands.Models;
using HelpingHands.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace HelpingHands.Controllers
{
    public class OfficeManagerController : Controller
    {

        private readonly UserRepository _userRepository;
        private readonly SuburbRepository _suburbRepository;
        public OfficeManagerController(UserRepository userRepository,SuburbRepository suburbRepository)
        {
            _userRepository = userRepository;
            _suburbRepository = suburbRepository;
        }

        public IActionResult ViewConditions()
        {
            return View(DapperORM.ReturnList<Conditions>("ViewConditions"));
        }

        [HttpGet]
        public ActionResult AddCondition()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCondition(Conditions conditions)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", conditions.Name);
            param.Add("@Description", conditions.Description);
            DapperORM.ExecuteWithoutReturn("AddOrEditCondition", param);
            return RedirectToAction("ViewConditions");
        }

        public ActionResult AddNurses(int id = 0)
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

        public IActionResult ViewNurses()
        {
            return View(DapperORM.ReturnList<UserModel>("ViewNurses"));
        }
        [HttpGet]
        public ActionResult GetSuburbs()
        {
            var suburbs = _suburbRepository.GetSuburbs();
            ViewBag.SuburbList = suburbs;
            return View();
        }
        public IActionResult ViewNursesWithSuburbs()
        {
            return View(DapperORM.ReturnList<PreferredSuburb>("GetAllNurses"));
        }
        [HttpPost]
        public ActionResult AddNurses(UserModel user)
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
        public IActionResult Index()
        {
            var model = new CitySuburbViewModel();
            model.Cities = DapperORM.ReturnList<City>("ViewCities").Select(c => new SelectListItem { Value = c.CityId.ToString(), Text = c.CityName }).ToList();
            model.Suburbs = new List<SelectListItem>();
            return View(model);
        }
      
        [HttpGet]
        public ActionResult ViewNewContracts()
        {
            {

                return View(DapperORM.ReturnList<OfficeManager>("NewContractsManager"));

            }
        }
        [HttpGet]
        public ActionResult UpdateCareContract([FromRoute]int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                ViewBag.Id = id;
                DynamicParameters param = new DynamicParameters();
                param.Add("@CareContractId", id);
                var nurses = _userRepository.GetNurses();
                ViewBag.NurseList = nurses;
                var contract = DapperORM.ReturnList<OfficeManager>("ViewContractById", param).FirstOrDefault();
                var updateContract = new OfficeManager
                {
                    CareContractId = contract.CareContractId,
                };

                return View(updateContract);
            }
        }


        [HttpPost]
        public ActionResult UpdateCareContract(OfficeManager manager)
        {
          
            
                if (manager.StartDate.Date == DateTime.Now.Date)
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@NurseId", manager.NurseId);
                    param.Add("@StartDate", manager.StartDate);
                    param.Add("@ContractStatus", manager.ContractStatus);
                    param.Add("@CareContractId", manager.CareContractId);

                    DapperORM.ExecuteWithoutReturn("UpdateCareContract", param);


                    return RedirectToAction("ViewNewContracts");
                }  
                    
            return View();
        }
        public IActionResult CountNurses()
        {
            int nurseCount = DapperORM.ExecuteReturnScalar<int>("CountNurses");
            ViewBag.NurseCount = nurseCount;


            return View();
        }
        [HttpGet]
        public ActionResult GetNurses()
        {
            var nurses = _userRepository.GetNurses();
            ViewBag.NurseList = nurses;
            return View();
        }
        
    }
}
