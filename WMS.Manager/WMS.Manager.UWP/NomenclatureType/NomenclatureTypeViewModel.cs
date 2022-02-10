using WMS.Manager.Domain.ViewModels;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.UWP.NomenclatureType
{
    /// <summary>
    /// Представляет ViewModel типа номенклатуры.
    /// </summary>
    public class NomenclatureTypeViewModel : GrpcViewModel<NomenclatureTypeGrpc>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id => Model.Id;

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name => Model.Name;
    }
}