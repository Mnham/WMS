using WMS.Manager.Domain.ViewModels;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.Nomenclature
{
    public class NomenclatureViewModel : GrpcViewModel<NomenclatureGrpc>
    {
        public long Height => Model.Height;
        public long Id => Model.Id;

        public long Length => Model.Length;
        public string Name => Model.Name;

        public NomenclatureTypeGrpc Type => Model.Type;
        public int Weight => Model.Weight;
        public long Width => Model.Width;
        public void UpdateType(NomenclatureTypeGrpc type) => Model.Type = type;
    }
}