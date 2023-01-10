using OpinionApi.Models;
using OpinionApi.Repository;

namespace OpinionApi.Service
{
    public class OpinionService : IOpinionService
    {
        private readonly IOpinionRepository _optionRepository;

        public OpinionService(IOpinionRepository optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public async Task<long> CreateAsync(OpinionCreateVM opinion, long userId)
        {
            var model = opinion.ToModel(userId);
            return await _optionRepository.CreateAsync(model);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _optionRepository.DeleteAsync(id);
        }

        public async Task EditAsync(OpinionEditVM opinion, long id)
        {
            var model = opinion.ToModel(id);
            await _optionRepository.EditAsync(model);
        }

        public async Task<OpinionResult> GetListAsync(OpinionCriteria criteria, long? userId)
        {
            var page = criteria.Page ?? 1;
            var pageSize = criteria.PageSize ?? 10;
            var totalRows = 0;

            var list = await _optionRepository.GetListAsync(criteria.SpecId);

            totalRows = list.Count();
            var filterList = list.Skip((page - 1) * pageSize).Take(pageSize);

            return new OpinionResult(filterList.Select(x => new OpinionGetVM(x, userId)), totalRows);
        }

    }
}
