using UniversityApi.Models;

namespace UniversityApi.Service
{
    public interface IUniversityService
    {
        IEnumerable<DestinationVM> DestSpecialityGetList();
    }
}
