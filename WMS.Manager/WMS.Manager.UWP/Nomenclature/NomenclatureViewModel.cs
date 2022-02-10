using WMS.Manager.Domain.ViewModels;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.UWP.Nomenclature
{
    /// <summary>
    /// Представляет ViewModel номенклатуры.
    /// </summary>
    public class NomenclatureViewModel : GrpcViewModel<NomenclatureGrpc>
    {
        /// <summary>
        /// Высота.
        /// </summary>
        public long Height => Model.Height;

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id => Model.Id;

        /// <summary>
        /// Длина.
        /// </summary>
        public long Length => Model.Length;

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name => Model.Name;

        /// <summary>
        /// Тип номенклатуры.
        /// </summary>
        public NomenclatureTypeGrpc Type => Model.Type;

        /// <summary>
        /// Объем.
        /// </summary>
        public string Volume => (Length * Width * Height / 1000000000.0).ToString("F9");

        /// <summary>
        /// Вес.
        /// </summary>
        public int Weight => Model.Weight;

        /// <summary>
        /// Ширина.
        /// </summary>
        public long Width => Model.Width;

        /// <summary>
        /// Обновляет тип номенклатуры.
        /// </summary>
        public void UpdateType(NomenclatureTypeGrpc type) => Model.Type = type;
    }
}