using WMS.Manager.Domain.ViewModels;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.NomenclatureType
{
    public class NomenclatureTypeViewModel : GrpcViewModel<NomenclatureTypeGrpc>
    {
        public long Id => Model.Id;
        public string Name => Model.Name;
    }
}