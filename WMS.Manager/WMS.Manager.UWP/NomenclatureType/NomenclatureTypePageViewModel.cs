using System.Threading.Tasks;

using WMS.Manager.Domain.ViewModels;
using WMS.Manager.GrpcClient.Clients;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.NomenclatureType
{
    /// <summary>
    /// Представляет ViewModel страницы номенклатуры.
    /// </summary>
    public class NomenclatureTypePageViewModel : PageViewModel<
        NomenclatureTypeGrpc,
        NomenclatureTypeViewModel,
        NomenclatureTypeEditorViewModel>
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureTypePageViewModel"/>.
        /// </summary>
        public NomenclatureTypePageViewModel(WmsGrpcClient grpcClient) : base(grpcClient) =>
            LoadNomenclatureTypes();

        /// <summary>
        /// Редактор типа номенклатуры.
        /// </summary>
        public new NomenclatureTypeEditorViewModel Editor => (NomenclatureTypeEditorViewModel)base.Editor;

        /// <summary>
        /// Добавляет тип номенклатуры.
        /// </summary>
        protected override async Task<RequestResult<NomenclatureTypeGrpc>> InsertAsync() =>
            await GrpcClient.NomenclatureType.InsertAsync(Editor.GetNewGrpcModel());

        /// <summary>
        /// Обновляет тип номенклатуры.
        /// </summary>
        protected override async Task<RequestResult<NomenclatureTypeGrpc>> UpdateAsync() =>
            await GrpcClient.NomenclatureType.UpdateAsync(Editor.GetNewGrpcModel());

        /// <summary>
        /// Загружает тип номенклатуры.
        /// </summary>
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