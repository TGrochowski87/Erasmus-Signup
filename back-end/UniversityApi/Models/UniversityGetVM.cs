using UniversityApi.DbModels;
using UniversityApi.Helpers;

namespace UniversityApi.Models
{
    public class UniversityGetVM
    {
        public string? UniversityName { get; set; }
        public string? ErasmusCode { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? FlagUrl { get; set; }
        public string? Link { get; set; }
        public string? Email { get; set; }
        public short? SelectedDestId { get; set; }
        public IEnumerable<DestinationGetVM> Destinations { get; set; }


        public UniversityGetVM(University dbModel, short destId)
        {
            SelectedDestId = destId;
            UniversityName = dbModel.Name;
            ErasmusCode = dbModel.ErasmusCode;
            Country = dbModel.Country;
            City = dbModel.City;
            Link = dbModel.Link;
            Email = dbModel.Email;
            if (dbModel.Country != null)
                FlagUrl = CountryDictionary.Flags.GetValueOrDefault(dbModel.Country);

            Destinations = dbModel.DestSpecialities.Select(x => new DestinationGetVM(x));
        }

    }
}
