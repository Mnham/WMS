using WMS.ClassLibrary.Infrastructure.Models;
using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses
{
    public class SearchEmployeeQueryResponse : IItemsModel<EmployeeDto>
    {
        public IReadOnlyList<EmployeeDto> Items { get; set; }
    }
}
