using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate
{
    /// <summary>
    /// Интерфейс репозитория типа номенклатуры.
    /// </summary>
    public interface INomenclatureTypeRepository
    {
        /// <summary>
        /// Возвращает все типы номенклатур.
        /// </summary>
        Task<IReadOnlyCollection<NomenclatureType>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет тип номенклатуры.
        /// </summary>
        Task<NomenclatureType> Insert(NomenclatureType nomenclatureType, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет тип номенклатуры.
        /// </summary>
        Task<NomenclatureType> Update(NomenclatureType nomenclatureType, CancellationToken cancellationToken);
    }
}