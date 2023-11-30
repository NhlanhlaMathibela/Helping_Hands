using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
       
      
        public string UserType { get; set; }    
        public string? Username { get; set; }
        public string? Password { get;set; }
        public string ?FirstName { get; set; }
        public string ?LastName { get; set; }   
        public string ?Gender { get; set; }
        public string ?DOB { get; set; }

        public string? NextOfKin { get; set; }
        public string ?NextOfKinContact { get; set; }
        
        public string ?ContactNumber { get; set; }   
    }
}
