using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ExceptionServices;

namespace HelpingHands.Models
{
    public class Nurse
    {
        public int NurseId { get; set; }
        public int NurseCode { get; set; }
        public string? FirstName { get; set; }
        public string ?LastName { get; set; } 
        public  string? IdNumber { get; set; }
        public string ? ContactNumber { get; set; }
       public string ? EmailAddress { get; set; }
        public string Gender { get; set; }

    }
}
