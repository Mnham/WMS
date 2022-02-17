﻿using WMS.EmployeeService.Domain.Infrastructure.Models;

namespace WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses
{
    /// <summary>
    /// Представляет ответ на запрос сессии.
    /// </summary>
    public class GetByIdEmployeeSessionQueryResponse
    {
        /// <summary>
        /// Сессия.
        /// </summary>
        public EmployeeSessionDto Session { get; init; }
    }
}