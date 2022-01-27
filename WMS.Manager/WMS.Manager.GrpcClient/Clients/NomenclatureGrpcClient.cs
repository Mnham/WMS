using System.Threading.Tasks;

using WMS.NomenclatureService.Grpc;

using static WMS.NomenclatureService.Grpc.NomenclatureGrpcService;

namespace WMS.Manager.GrpcClient.Clients
{
    /// <summary>
    /// Представляет клиент номенклатуры.
    /// </summary>
    public class NomenclatureGrpcClient : GrpcClientBase
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureGrpcClient"/>.
        /// </summary>
        public NomenclatureGrpcClient(string address) : base(address)
        {
        }

        /// <summary>
        /// Номенклатура.
        /// </summary>
        private NomenclatureGrpcServiceClient Nomenclature { get; }

        /// <summary>
        /// Добавляет номенклатуру.
        /// </summary>
        public async Task<RequestResult<NomenclatureGrpc>> InsertAsync(NomenclatureGrpc nomenclature)
            => await Execute(async () => await Nomenclature.InsertAsync(nomenclature));

        /// <summary>
        /// Выполняет поиск номенклатуры.
        /// </summary>
        public async Task<RequestResult<NomenclatureList>> SearchAsync(NomenclatureSearchFilter searchFilter)
            => await Execute(async () => await Nomenclature.SearchAsync(searchFilter));

        /// <summary>
        /// Обновляет номенклатуру.
        /// </summary>
        public async Task<RequestResult<NomenclatureGrpc>> UpdateAsync(NomenclatureGrpc nomenclature)
            => await Execute(async () => await Nomenclature.UpdateAsync(nomenclature));
    }
}
