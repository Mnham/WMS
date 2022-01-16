using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate
{
    public interface INomenclatureRepository
    {
        Task<IReadOnlyCollection<Nomenclature>> Search(NomenclatureFilter filter, CancellationToken cancellationToken);

        Task<Nomenclature> Insert(Nomenclature nomenclature, CancellationToken cancellationToken);

        Task<Nomenclature> Update(Nomenclature nomenclature, CancellationToken cancellationToken);
    }
}