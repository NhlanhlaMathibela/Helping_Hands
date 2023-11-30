using System.ComponentModel.DataAnnotations;

namespace HelpingHands.Models
{
    public class PatientCareContract
    {

        [Display(Name = "Contract Date")]
        public DateTime ContractDate { get; set; }
        public int Patient { get; set; }

        [Display(Name ="Address Line 1")]
        public string AddLine1 { get; set; }
        [Display(Name = "Address Line 2 (optional)")]
        public string ?AddLine2 { get; set; }
        public int Suburb { get; set; }

        [Display(Name = "Suburb")]
        public string SuburbName { get;set; }

        [Display(Name ="Wound Description")]
        public string WoundDesc { get; set; }
        
    }
}
