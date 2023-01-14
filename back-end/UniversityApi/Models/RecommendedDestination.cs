namespace UniversityApi.Models
{
    public class RecommendedDestination
    {
        public bool IsCompletedProfile { get; set; }
        public IEnumerable<DestinationVM> Destinations { get; set; }
    }
}
