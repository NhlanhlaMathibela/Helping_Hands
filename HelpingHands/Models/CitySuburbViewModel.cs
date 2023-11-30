using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands.Models
{
    public class CitySuburbViewModel
    {
        public int CityId { set; get; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public string SelectedCityId { get; set; }

        public int SuburbId { set; get; }
        public IEnumerable<SelectListItem> Suburbs { get; set; }
        public string SelectedSuburbId { get; set; }
    }
}
