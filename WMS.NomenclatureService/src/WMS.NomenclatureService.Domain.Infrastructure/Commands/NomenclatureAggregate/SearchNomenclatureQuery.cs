using MediatR;

using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate
{
    /// <summary>
    /// Предствляет команду поиска номенклатуры.
    /// </summary>
    public class SearchNomenclatureQuery : IRequest<SearchNomenclatureQueryResponse>
    {
        /// <summary>
        /// Идентификатор номенклатуры.
        /// </summary>
        public long? NomenclatureId { get; set; }

        /// <summary>
        /// Наименование номенклатуры.
        /// </summary>
        public string NomenclatureName { get; set; }

        /// <summary>
        /// Идентификатор типа номенклатуры.
        /// </summary>
        public long? NomenclatureTypeId { get; set; }
    }
}