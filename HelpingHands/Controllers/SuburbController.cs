using HelpingHands.Repository;
using Microsoft.AspNetCore.Mvc;


namespace HelpingHands.Controllers
{
    public class SuburbController : Controller
    {
        private readonly SuburbRepository _suburbRepository;

        public SuburbController(SuburbRepository suburbRepository)
        {
            _suburbRepository = suburbRepository;
        }

        public async Task<IActionResult> Index()
        {
            var suburbs = await _suburbRepository.GetSuburbsWithCities();
            return View(suburbs);
        }
    }

}
