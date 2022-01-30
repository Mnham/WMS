using MediatR;

using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate
{
    /// <summary>
    /// Предствляет команду обновления номенклатуры.
    /// </summary>
    public class UpdateNomenclatureQuery : IRequest<UpdateNomenclatureQueryResponse>
    {
        /// <summary>
        /// Номенклатура.
        /// </summary>
        public NomenclatureDto Nomenclature { get; init; }
    }
}
