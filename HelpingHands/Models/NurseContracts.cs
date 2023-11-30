using System.ComponentModel.DataAnnotations;


namespace HelpingHands.Models
{
    public class NurseContracts
    {
        public int Nurse { get; set; }
        [Display(Name = "Contract Date")]
        public DateTime ContractDate {get;set;}

        public DateTime AssignedDate  { get;set;} 
        public DateTime StartDate { get; set; }

        [Display(Name = "Wound Description")]
        public string WoundDesc { get; set;}
        [Display(Name ="Address Line 1")]
        public string AddLine1 { get; set;}
        [Display(Name = "Address Line 2 ")]
        public string AddLine2 { get; set;}
        [Display(Name ="Suburb Name")]
        public string SuburbName { get; set;}
        public int Suburb { get;set;}
        [Display(Name ="First Name")]
        public string Name { get; set;}
        [Display(Name ="Name")]
        public string LastName { get; set;}
        public int NurseId { get; set;}
       
        public string ContractStatus { get; set;}   
        public int CareContractId { get; set;}
        public string Patient { get; set; }
        public string Address{ get; set; }



    }


    public class UpdateContracts
    {
        public DateTime StartDate { get; set; }
        public string ContractStatus { get; set; }
    }
}
