using HelpingHands.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HelpingHands.Controllers
{
    public class CareContractController : Controller
    {
        private readonly CareContractRepository _careContractRepository;

        public CareContractController(CareContractRepository careContractRepository)
        {
            _careContractRepository = careContractRepository;
        }

        public async Task<IActionResult> Index()
        {
            var careContracts = await _careContractRepository.GetNewContracts();
            return View(careContracts);
        }
    }
}
