using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate
{
    public interface INomenclatureTypeRepository
    {
        Task<IReadOnlyCollection<NomenclatureType>> GetAll(CancellationToken cancellationToken);

        Task<NomenclatureType> Insert(NomenclatureType nomenclatureType, CancellationToken cancellationToken);

        Task<NomenclatureType> Update(NomenclatureType nomenclatureType, CancellationToken cancellationToken);
    }
}