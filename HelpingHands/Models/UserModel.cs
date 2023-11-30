using System.ComponentModel.DataAnnotations;


namespace HelpingHands.Models
{
    public class UserModel
    {
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter your Email Address")]
        [Display(Name = "Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email address is not valid")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 6)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{6,15})$", ErrorMessage =
        "Password must contain: Minimum 6 characters atleast 1 Uppercase alphabet, 1 Lowercase alphabet, 1 Number and 1 Special Character")]
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm password")]
        [Compare("Password", ErrorMessage = "password doesn't match, Try again")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public Nullable<bool> Is_Deleted { get; set; }

        //[DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string? ContactNumber { get; set; }

        [Required(ErrorMessage = "Enter the user type")]
        [Display(Name = "User Type")]
        public string UserType { get; set; }

        public char? Status { get; set; }


        public virtual Patient Patient { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

       

    }

    public class UserModels
    {
        // Other properties

        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword",
            ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterPatients
    {
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter your Email Address")]
        [Display(Name = "Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email address is not valid")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 6)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{6,15})$", ErrorMessage =
        "Password must contain: Minimum 6 characters atleast 1 Uppercase alphabet, 1 Lowercase alphabet, 1 Number and 1 Special Character")]
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm password")]
        [Compare("Password", ErrorMessage = "password doesn't match, Try again")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }

        
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        
        public string? ContactNumber { get; set; }

        [Required(ErrorMessage = "Enter the user type")]
        [Display(Name = "User Type")]
        public string UserType { get; set; }

    }

}
