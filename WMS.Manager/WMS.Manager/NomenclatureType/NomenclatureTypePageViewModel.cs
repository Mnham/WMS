using System.Threading.Tasks;

using WMS.Manager.Domain.ViewModels;
using WMS.Manager.GrpcClient.Clients;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.NomenclatureType
{
    public class NomenclatureTypePageViewModel : PageViewModel<
        NomenclatureTypeGrpc,
        NomenclatureTypeViewModel,
        NomenclatureTypeEditorViewModel>
    {
        public NomenclatureTypePageViewModel(WmsGrpcClient grpcClient) : base(grpcClient) =>
            LoadNomenclatureTypes();

        public new NomenclatureTypeEditorViewModel Editor => (NomenclatureTypeEditorViewModel)base.Editor;

        protected override async Task<RequestResult<NomenclatureTypeGrpc>> InsertAsync() =>
            await GrpcClient.NomenclatureType.InsertAsync(Editor.GetNewGrpcModel());

        protected override async Task<RequestResult<NomenclatureTypeGrpc>> UpdateAsync() =>
            await GrpcClient.NomenclatureType.UpdateAsync(Editor.GetNewGrpcModel());
        private async void LoadNomenclatureTypes()
        {
            RequestResult<NomenclatureTypeList> result = await GrpcClient.NomenclatureType.GetAllAsync();
            if (result.IsSuccess)
            {
                foreach (NomenclatureTypeGrpc type in result.Response.NomenclatureTypes)
                {
                    NomenclatureTypeViewModel vm = new();
                    vm.SetModel(type);
                    Items.Add(vm);
                }
            }
        }
    }
}