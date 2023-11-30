using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands.Models
{
    public class PatientContractSuburb
    {
        public DateTime ContractDate { get; set; }
        public int Patient { get; set; }
        public string AddLine1 { get; set; }
        public string? AddLine2 { get; set; }
        public string? Suburb { get; set; }
        public string? WoundDesc { get; set; }

        public int SuburbId { get; set; }
        public string ?SuburbName { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public string CityName { get; set; }

    }
}
