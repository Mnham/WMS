﻿using MediatR;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate
{
    /// <summary>
    /// Представляет команду для запроса сессии.
    /// </summary>
    public class SearchEmployeeSessionQuery : IRequest<SearchEmployeeSessionQueryResponse>
    {
        /// <summary>
        /// Сессия.
        /// </summary>
        public long SessionId { get; set; }
    }
}