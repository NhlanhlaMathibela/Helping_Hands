using System.ComponentModel.DataAnnotations;

namespace HelpingHands.Models
{
    public class PatientDetails
    {
        public string PatientId { get; set; }
        [Required(ErrorMessage ="Please enter your first Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Display(Name = "Date Of Birth")]
        public string DOB { get; set; }
        [Required]
        [Display(Name ="Next Of Kin")]
        public string NextOfKin { get; set; }
        [Required]
        [Display(Name = "Next Of Kin Phone Number")]
        [RegularExpression(@"^0\d{9}$")]
        public string NextOfKinContact { get; set; }

    }
}
