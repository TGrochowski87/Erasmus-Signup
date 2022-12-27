namespace UniversityApi.Models
{
    public class DestinationResult
    {
        public int TotalRows { get; set; }
        public IEnumerable<DestinationVM> Destinations { get; set; }
        public IEnumerable<DestinationVM> RecomendedDestinations { get; set; }
        public IEnumerable<DestinationVM> RecommendedByStudentsDestinations { get; set; }

        public DestinationResult(IEnumerable<DestinationVM> destinations,
            IEnumerable<DestinationVM> recomendedDestinations,
            IEnumerable<DestinationVM> recommendedByStudentsDestinations,
            int totalRows)
        {
            TotalRows = totalRows;
            Destinations = destinations;
            RecomendedDestinations = recomendedDestinations;
            RecommendedByStudentsDestinations = recommendedByStudentsDestinations;
        }
    }
}
