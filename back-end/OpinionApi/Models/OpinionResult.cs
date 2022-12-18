namespace OpinionApi.Models
{
    public class OpinionResult
    {
        public int TotalRows { get; set; }
        public IEnumerable<OpinionGetVM> Opinions { get; set; }

        public OpinionResult(IEnumerable<OpinionGetVM> opinions, int totalRows)
        {
            TotalRows = totalRows;
            Opinions = opinions;
        }
    }
}
