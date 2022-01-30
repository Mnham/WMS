using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate
{
    /// <summary>
    /// Интерфейс репозитория номенклатуры.
    /// </summary>
    public interface INomenclatureRepository
    {
        /// <summary>
        /// Выполняет поиск.
        /// </summary>
        Task<IReadOnlyCollection<Nomenclature>> Search(NomenclatureFilter filter, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет номенклатуру.
        /// </summary>
        Task<Nomenclature> Insert(Nomenclature nomenclature, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет номенклатуру.
        /// </summary>
        Task<Nomenclature> Update(Nomenclature nomenclature, CancellationToken cancellationToken);
    }
}