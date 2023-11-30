using Dapper;
using HelpingHands.Models;
using HelpingHands.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace HelpingHands.Controllers
{
    public class PatientController : Controller
    {

        private readonly UserRepository _userRepository;
		private readonly CareContractRepository _careContractRepository;
        private readonly SuburbRepository _suburbRepository;
        
	

		public PatientController(UserRepository userRepository, CareContractRepository careContractRepository, SuburbRepository suburbRepository)
        {
            _userRepository = userRepository;
			_careContractRepository = careContractRepository;
            _suburbRepository = suburbRepository;
          
			
        }

        public ActionResult GetSuburbs()
        {
            var suburbs = _suburbRepository.GetSuburbs();
            ViewBag.SuburbList = new SelectList(suburbs, "SuburbId", "SuburbName");
            return View();
        }
        [HttpGet]
        public IActionResult  UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdatePassword(UserModels model)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", userId);
                UserModel user = DapperORM.ReturnList<UserModel>("UsersViewById", param).FirstOrDefault();

                // Verify the old password
                if (user.Password == model.OldPassword)
                {
                   
                    param = new DynamicParameters();
                    param.Add("@UserId", userId);
                    param.Add("@NewPassword", model.NewPassword); 
                    DapperORM.ExecuteWithoutReturn("UpdateUserPassword", param);
                    TempData["SuccessMessage"] = "Password successfully  updated  (Please keep it safe)";
                    return RedirectToAction("UpdatePassword");

                }
             
            }

            ModelState.AddModelError(string.Empty, "Old password is incorrect");
            return View(model);
        }



        [HttpGet]
        public IActionResult AddCareContract()
        {
            var suburbs = _suburbRepository.GetSuburbs();
           
            ViewBag.SuburbList = suburbs;
            return View();
        }
        [HttpPost]
        public IActionResult AddCareContract(PatientCareContract obj)
        {
            string userId = HttpContext.Session.GetString("UserId");

            if (userId != null)
            {

                var parameters = new DynamicParameters();
                parameters.Add("@Patient", userId);
                parameters.Add("@ContractDate", obj.ContractDate);
                parameters.Add("@AddLine1", obj.AddLine1);
                parameters.Add("@AddLine2", obj.AddLine2);
                parameters.Add("Suburb", obj.Suburb);
                parameters.Add("@WoundDesc", obj.WoundDesc);


                TempData["SuccessMessage"] = "Successfully requested for a care contract,a nurse will be in touch soon :)";

                DapperORM.ExecuteWithoutReturn("CreateCareContract", parameters);


                return View();
            }

            return View();
        }




        public IActionResult PatientContract()
        {
            string userId = HttpContext.Session.GetString("UserId");

            var parameters = new DynamicParameters();
            parameters.Add("Patient", userId);

            var patientContracts = DapperORM.ReturnList<PatientCareContract>("ViewContractPatient", parameters);

            return View(patientContracts);
        }
        [HttpGet]
        public IActionResult AddPatientDetails() 
        { 
            return View();
        }
        [HttpPost]
        public IActionResult AddPatientDetails(PatientDetails patient)
        {
            string userId = HttpContext.Session.GetString("UserId");

            if (userId != null)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PatientId", userId);
                parameters.Add("@FirstName", patient.FirstName);
                parameters.Add("@LastName", patient.LastName);
                parameters.Add("@Gender", patient.Gender);
                parameters.Add("DOB", patient.DOB);
                parameters.Add("@NextOfKin", patient.NextOfKin);
                parameters.Add("NextOfKinContact", patient.NextOfKinContact);
                TempData["SuccessMessage"] = "Now we know you better";

                DapperORM.ExecuteWithoutReturn("AddPatientDetails", parameters);

                return RedirectToAction("ViewProfile");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ViewProfile()
        {
            string userId = HttpContext.Session.GetString("UserId");

            if (userId != null)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PatientId", userId);

                var patientDetails = DapperORM.ReturnList<Patient>("ViewPatientDetails", parameters);

                return View(patientDetails.FirstOrDefault());
            }

            return View();
        }

        [HttpGet]
        public IActionResult UpdateDetails()
        {
            string userId = HttpContext.Session.GetString("UserId");

            if (userId != null)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PatientId", userId);

                var patientDetails = DapperORM.ReturnList<PatientDetails>("ViewPatientDetails", parameters);

                return View(patientDetails.FirstOrDefault());
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateDetails(PatientDetails patient)
        {
            string userId = HttpContext.Session.GetString("UserId");

            if (userId != null)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PatientId", userId);
                parameters.Add("@FirstName", patient.FirstName);
                parameters.Add("@LastName", patient.LastName);
                parameters.Add("@Gender", patient.Gender);
                parameters.Add("DOB", patient.DOB);
                parameters.Add("@NextOfKin", patient.NextOfKin);
                parameters.Add("NextOfKinContact", patient.NextOfKinContact);

                DapperORM.ExecuteWithoutReturn("UpdatePatientDetails", parameters);

                return RedirectToAction("ViewProfile");
            }

            return View(patient);
        }




        [HttpGet]
        public IActionResult ManagePatientConditions()
        {
            var conditions = _userRepository.GetConditions();
            var viewModel = conditions.Select(c => new Conditions
            {
                ChronicConId = c.ChronicConId,
                Name = c.Name,
                IsSelected = false
            }).ToList();
            return View(viewModel);
        }

        public IActionResult ManagePatientConditions(List<Conditions> conditions)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                foreach (var condition in conditions)
                {
                    if (condition.IsSelected)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@Patient", userId);
                        parameters.Add("@Condition", condition.ChronicConId);
                        DapperORM.ExecuteWithoutReturn("AddPatientCondition", parameters);
                    }
                }
            }
            return RedirectToAction("GetConditions");
        }

        [HttpGet]   
        public async Task<ActionResult> GetConditions()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Patient", userId);


                var patientConditions = DapperORM.ReturnList<PatientConditions>("ViewPatientCondition", parameters);
                return View(patientConditions);

            }
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> GetVisits()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Patient", userId);


                var patientConditions = DapperORM.ReturnList<PatientConditions>("ViewMyVisits", parameters);
                return View(patientConditions);

            }
            return View();
        }

    }
}
