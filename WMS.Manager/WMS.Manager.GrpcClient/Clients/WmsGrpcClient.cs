using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

using Grpc.Net.Client;
using Grpc.Net.Client.Web;

using System;
using System.Net.Http;
using System.Threading.Tasks;

using WMS.NomenclatureService.Grpc;

using static WMS.NomenclatureService.Grpc.NomenclatureGrpcService;
using static WMS.NomenclatureService.Grpc.NomenclatureTypeGrpcService;

namespace WMS.Manager.GrpcClient.Clients
{
    public class WmsGrpcClient
    {
        public WmsGrpcClient()
        {
            Nomenclature = new NomenclatureGrpcServiceClient(GrpcChannel.ForAddress("http://localhost:8010", new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            }));

            NomenclatureType = new NomenclatureTypeGrpcServiceClient(GrpcChannel.ForAddress("http://localhost:8010", new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            }));
        }

        public Action<Exception> ExceptionHandler { get; set; }

        private NomenclatureGrpcServiceClient Nomenclature { get; }

        private NomenclatureTypeGrpcServiceClient NomenclatureType { get; }
        public async Task<RequestResult<NomenclatureGrpc>> NomenclatureInsertAsync(NomenclatureGrpc nomenclature)
            => await Execute(async () => await Nomenclature.InsertAsync(nomenclature));

        public async Task<RequestResult<NomenclatureList>> NomenclatureSearchAsync(NomenclatureSearchFilter searchFilter)
            => await Execute(async () => await Nomenclature.SearchAsync(searchFilter));

        public async Task<RequestResult<NomenclatureTypeList>> NomenclatureTypeGetAllAsync()
            => await Execute(async () => await NomenclatureType.GetAllAsync(new Empty()));

        public async Task<RequestResult<NomenclatureTypeGrpc>> NomenclatureTypeInsertAsync(NomenclatureTypeGrpc nomenclatureType)
            => await Execute(async () => await NomenclatureType.InsertAsync(nomenclatureType));

        public async Task<RequestResult<NomenclatureTypeGrpc>> NomenclatureTypeUpdateAsync(NomenclatureTypeGrpc nomenclatureType)
            => await Execute(async () => await NomenclatureType.UpdateAsync(nomenclatureType));

        public async Task<RequestResult<NomenclatureGrpc>> NomenclatureUpdateAsync(NomenclatureGrpc nomenclature)
            => await Execute(async () => await Nomenclature.UpdateAsync(nomenclature));

        private async Task<RequestResult<T>> Execute<T>(Func<Task<T>> func) where T : IMessage<T>
        {
            try
            {
                return new RequestResult<T>(await func(), true);
            }
            catch (Exception ex)
            {
                ExceptionHandler?.Invoke(ex);
                return new RequestResult<T>(default, false);
            }
        }
    }
}