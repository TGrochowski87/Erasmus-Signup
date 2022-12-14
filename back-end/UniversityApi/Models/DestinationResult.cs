namespace UniversityApi.Models
{
    public class DestinationResult
    {
        public int TotalRows { get; set; }
        public IEnumerable<DestinationVM> Destinations { get; set; }

        public DestinationResult(IEnumerable<DestinationVM> destinations, int totalRows)
        {
            TotalRows = totalRows;
            Destinations = destinations;
        }
    }
}
