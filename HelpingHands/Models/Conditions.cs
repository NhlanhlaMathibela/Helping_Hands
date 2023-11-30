namespace HelpingHands.Models
{
    public class Conditions
    {
        public int ChronicConId { get; set; }
        public string Name { get; set; }
        public int Patient { get; set; }
        public int Condition { get; set; }
        public bool IsSelected { get; set; }
        public string Description { get; set; }
    }



    public class PatientConditions
    {
        public int Patient { get; set; }
        public int Condition { get; set; }
        public string Name { get; set; }
    }
}
