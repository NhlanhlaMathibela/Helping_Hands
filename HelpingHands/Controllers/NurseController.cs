using Dapper;
using HelpingHands.Models;
using HelpingHands.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;

namespace HelpingHands.Controllers
{
    public class NurseController : Controller
    {

        private readonly SuburbRepository _suburbRepository;

        public NurseController(SuburbRepository suburbRepository)
        {
            _suburbRepository = suburbRepository;
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
        public ActionResult Index()
        {
            var suburbs = _suburbRepository.GetSuburbs();
            ViewBag.SuburbList = new SelectList(suburbs, "SuburbId", "SuburbName");
            return View();
        }
        [HttpPost]
        public ActionResult NurseSuburbs(Suburbs obj)
        {

           string userId = HttpContext.Session.GetString("UserId");
           if (userId != null)
           {
                var parameters = new DynamicParameters();
                parameters.Add("@Nurse", userId);
                parameters.Add("@PreferredSub", obj.PreferredSub);
                DapperORM.ExecuteWithoutReturn("AddPreferredSub", parameters);


                return RedirectToAction("SavePreferredSuburb");
           }

            
            return View();
        }

        [HttpGet]
        public ActionResult SavePreferredSuburb()
        {
            var suburbs = _suburbRepository.GetSuburbs();
            ViewBag.SuburbList = suburbs;
            return View();
        }
        public async Task<ActionResult> GetPreferredSuburbs()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nurse", userId);

                var preferredSuburbs = DapperORM.ReturnList<PreferredSuburb>("GetPreferredSuburbsForNurse", parameters);
                return View(preferredSuburbs);
            }
           return View();
        }


        [HttpGet]
        public ActionResult ViewContractsBySuburb()
        {
            {
                string userId = HttpContext.Session.GetString("UserId");
                if (userId != null)
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("NurseId", userId);
                    var Contracts = DapperORM.ReturnList<NurseContracts>("CareContractBySuburb", parameters);
                    return View(Contracts);

                }

                return View();
            }
        }
      
        [HttpGet]
        public ActionResult UpdateCareContract(int id)
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
                 
                var contract =DapperORM.ReturnList<NurseContracts>("ViewContractById", param).FirstOrDefault();
                var updateContract = new NurseContracts
                {
                       CareContractId = contract.CareContractId,
                };
             
                return View(updateContract);
            }
        }

        [HttpPost]
        public ActionResult UpdateCareContract(NurseContracts nurse)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                if (nurse.StartDate.Date == DateTime.Now.Date)
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@NurseId", userId);
                    param.Add("@StartDate", nurse.StartDate);
                    param.Add("@ContractStatus", nurse.ContractStatus);
                    param.Add("@CareContractId", nurse.CareContractId);

                    DapperORM.ExecuteWithoutReturn("UpdateCareContract", param);
                    TempData["SuccessMessage"] = "Contract Succefully Approved";

                    return RedirectToAction("ApprovedContracts");
                }
                
            }

            return View();
        }
        [HttpGet]
        public ActionResult ApprovedContracts(NurseContracts nurse)
        {
            string userId = HttpContext.Session.GetString("UserId");
            var parameters = new DynamicParameters();
            parameters.Add("NurseId", userId);
            var Contracts = DapperORM.ReturnList<NurseContracts>("ViewAssignedContracts", parameters);
            return View(Contracts);
       
        }


        [HttpGet]
        public IActionResult ViewProfile()
        {
                string userId = HttpContext.Session.GetString("UserId");
                if (userId != null)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@NurseId", userId);
                    var nurseDetails = DapperORM.ReturnList<Nurse>("ViewNurseDetails", parameters).FirstOrDefault();
                    return View(nurseDetails);
                }  
            
            return View();
        }

        [HttpGet]
        public ActionResult AddNurseDetails(int id = 0)
        {
            if (id == 0)

                return View();

            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@NurseId", id);
                return View(DapperORM.ReturnList<Nurse>("ViewNurseDetails", param).FirstOrDefault<Nurse>());
            }
        }

        [HttpPost]
        public ActionResult AddNurseDetails(Nurse nurse)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("NurseId", userId);
                param.Add("@FirstName", nurse.FirstName);
                param.Add("@LastName", nurse.LastName);
                param.Add("@IdNumber", nurse.IdNumber);


                DapperORM.ExecuteWithoutReturn("UpdateNurseDetails", param);
                return RedirectToAction("ViewProfile");
            }
         

            return RedirectToAction("ViewProfile");
        }

        [HttpGet]
        public ActionResult AddCareVisit([FromRoute]int id)
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

                var contract = DapperORM.ReturnList<CareVisit>("ViewContractById", param).FirstOrDefault();
                var updateContract = new CareVisit
                {
                    ContractId = contract.CareContractId,
                };

                return View(updateContract);
            }
        }
        [HttpPost]
        public ActionResult AddCareVisit(CareVisit visit)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {

                DynamicParameters param = new DynamicParameters();              
                param.Add("@VisitDate", visit.VisitDate);
                param.Add("@ApproxArrivalTime", visit.ApproxArrivalTime);
                param.Add("@CareContractId", visit.ContractId);
               

                DapperORM.ExecuteWithoutReturn("AddCareVisit", param);

                return RedirectToAction("ViewCareVisit", new { id = visit.ContractId});
            }

             return View();
        }
        [HttpGet]
        public async Task<ActionResult> ViewCareVisit([FromRoute] int id)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {

                if (id == 0)

                    return View();
                else
                {
                    ViewBag.Id = id;
                    var parameters = new DynamicParameters();
                    parameters.Add("ContractId",id);
                    parameters.Add("NurseId", userId);
                    


                    var visits = DapperORM.ReturnList<CareVisit>("ViewVisits", parameters);
                    return View(visits);
                }

                

            }
            return View();
        }
      
        [HttpGet]
        public ActionResult UpdateCareVisit(int id)
        {

            if (id == 0)
            {
                return View();
            }
            else
            {
                ViewBag.Id = id;
                DynamicParameters param = new DynamicParameters();
                param.Add("@CareVisitId", id);
               

                var visit = DapperORM.ReturnList<CareVisit>("ViewAllVisits", param).FirstOrDefault();
                var updateVisit = new CareVisit
                {
                    CareVisitId = visit.CareVisitId,
                    ContractId = visit.ContractId,
                };

                return View(updateVisit);
            }
        }
        
        [HttpPost]
        public ActionResult UpdateCareVisit(CareVisit visit)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@WoundDesc", visit.WoundDesc);
                param.Add("@Notes", visit.Notes);
                param.Add("@CareVisitId", visit.CareVisitId);

                param.Add("@ApproxArrivalTime", visit.CareVisitId);
                param.Add("@ArriveTime", visit.CareVisitId);
                param.Add("@DepartTime", visit.CareVisitId);
                DapperORM.ExecuteWithoutReturn("UpdateCareVisit", param);

                return RedirectToAction("ViewCareVisit", new { id = visit.ContractId });
            }

            return View();
        }

       




    }
}
