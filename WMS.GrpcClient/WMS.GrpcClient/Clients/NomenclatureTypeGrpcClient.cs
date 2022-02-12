using Google.Protobuf.WellKnownTypes;

using System.Threading.Tasks;

using WMS.NomenclatureService.Grpc;

using static WMS.NomenclatureService.Grpc.NomenclatureTypeGrpcService;

namespace WMS.GrpcClient.Clients
{
    /// <summary>
    /// Представляет клиент типа номенклатуры.
    /// </summary>
    public class NomenclatureTypeGrpcClient : GrpcClientBase
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureTypeGrpcClient"/>.
        /// </summary>
        public NomenclatureTypeGrpcClient(string address) : base(address) =>
            NomenclatureType = new NomenclatureTypeGrpcServiceClient(Channel);

        /// <summary>
        /// Тип номенклатуры.
        /// </summary>
        private NomenclatureTypeGrpcServiceClient NomenclatureType { get; }

        /// <summary>
        /// Возвращает все типы номенклатур.
        /// </summary>
        public async Task<RequestResult<NomenclatureTypeList>> GetAllAsync()
            => await Execute(async () => await NomenclatureType.GetAllAsync(new Empty()));

        /// <summary>
        /// Добавляет тип номенклатуры.
        /// </summary>
        public async Task<RequestResult<NomenclatureTypeGrpc>> InsertAsync(NomenclatureTypeGrpc nomenclatureType)
            => await Execute(async () => await NomenclatureType.InsertAsync(nomenclatureType));

        /// <summary>
        /// Обновляет тип номенклатуры.
        /// </summary>
        public async Task<RequestResult<NomenclatureTypeGrpc>> UpdateAsync(NomenclatureTypeGrpc nomenclatureType)
            => await Execute(async () => await NomenclatureType.UpdateAsync(nomenclatureType));
    }
}