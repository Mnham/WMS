using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WMS.ClassLibrary.Extensions;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;

namespace WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureTypeAggregate
{
    public class GetAllNomenclatureTypeQueryHandler : IRequestHandler<GetAllNomenclatureTypeQuery, GetAllNomenclatureTypeQueryResponse>
    {
        private readonly INomenclatureTypeRepository _nomenclatureTypeRepository;

        public GetAllNomenclatureTypeQueryHandler(INomenclatureTypeRepository nomenclatureTypeRepository) =>
            _nomenclatureTypeRepository = nomenclatureTypeRepository;

        public async Task<GetAllNomenclatureTypeQueryResponse> Handle(GetAllNomenclatureTypeQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<NomenclatureType> types = await _nomenclatureTypeRepository.GetAll(cancellationToken);

            return new GetAllNomenclatureTypeQueryResponse()
            {
                Items = types.Map(NomenclatureTypeMapper.EntityToDto).ToList()
            };
        }
    }
}