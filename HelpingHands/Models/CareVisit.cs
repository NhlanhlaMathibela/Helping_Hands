using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands.Models
{
    public class CareVisit
    {

        public int ContractId { get; set; }
        public int CareVisitId { get; set; }    
        public int CareContractId { get; set; }
        [Display(Name ="Visit Date")]
        public DateTime VisitDate { get; set; }
        [Display(Name = "Wound Progress")]
        public string ?WoundDesc { get; set; }
        public string? Notes { get; set; }
        [Display(Name ="Approximate Arrival Time")]
        public TimeSpan ApproxArrivalTime { get; set; }
        public TimeSpan ArriveTime { get; set; }
        public TimeSpan DepartTime { get; set; }
    }
}
