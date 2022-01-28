using MediatR;

using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;

namespace WMS.EmployeeService.Domain.Infrastructure.Handlers.EmployeeAggregate
{
    public class InsertEmployeeQueryHandler : IRequestHandler<InsertEmployeeQuery, InsertEmployeeQueryResponse>
    {
        private readonly INomenclatureRepository _nomenclatureRepository;

        public InsertNomenclatureQueryHandler(INomenclatureRepository nomenclatureRepository) =>
            _nomenclatureRepository = nomenclatureRepository;

        public async Task<InsertEmployeeQueryResponse> Handle(InsertEmployeeQuery request, CancellationToken cancellationToken)
        {
            Nomenclature nomenclature = await _nomenclatureRepository.Insert(
                EmployeeMapper.DtoToEntity(request.Employee),
                cancellationToken);

            return new InsertNomenclatureQueryResponse()
            {
                Nomenclature = NomenclatureMapper.EntityToDto(nomenclature)
            };
        }
    }
}
