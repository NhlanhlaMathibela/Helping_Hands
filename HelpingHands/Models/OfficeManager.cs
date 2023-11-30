namespace HelpingHands.Models
{
    public class OfficeManager
    {
        public int CareContractId { get; set; }
        public DateTime ContractDate { get; set; }
        public string SuburbName { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public string  FirstName { get; set; }
        public string ContractStatus { get; set; }

        public int NurseId { get;set; }
    }
}
