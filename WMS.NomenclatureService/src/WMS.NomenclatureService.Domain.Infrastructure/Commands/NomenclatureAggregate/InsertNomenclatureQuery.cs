using MediatR;

using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate
{
    /// <summary>
    /// Предствляет команду добавления номенклатуры.
    /// </summary>
    public class InsertNomenclatureQuery : IRequest<InsertNomenclatureQueryResponse>
    {
        /// <summary>
        /// Номенклатура.
        /// </summary>
        public NomenclatureDto Nomenclature { get; init; }
    }
}
