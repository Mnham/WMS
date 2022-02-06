using Grpc.Core;
using MediatR;
using WMS.EmployeeService.Grpc;

namespace WMS.EmployeeService.GrpcServices
{
    public class EmployeeSessionGrpcService : EmployeeSessionApiGrpc.EmployeeSessionApiGrpcBase
    {
        private readonly IMediator _mediator;

        public EmployeeSessionGrpcService(IMediator mediator) => _mediator = mediator;

        public override Task<EmployeeSessionGrpc> Insert(EmployeeSessionGrpc request, ServerCallContext context)
        {
            return base.Insert(request, context);
        }

        public override Task<EmployeeSessionGrpc> Update(EmployeeSessionGrpc request, ServerCallContext context)
        {
            return base.Update(request, context);
        }

        public override Task<EmployeeSessionGrpc> GetById(IntIdModel request, ServerCallContext context)
        {
            return base.GetById(request, context);
        }
    }
}