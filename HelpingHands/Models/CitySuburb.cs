namespace HelpingHands.Models
{
    public class CitySuburb
    {
            
        public string? CityName { get; set; }
        public string? SuburbName { get; set; }
       
     


    }

    public class Suburbs
    {
        public string? SuburbName { get; set; }
        public int SuburbId { get; set; }

        public int PreferredSub { get; set; }

        public int Nurse { get; set; }
    }
}
