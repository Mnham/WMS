using Microsoft.Toolkit.Mvvm.Input;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using WMS.Manager.Domain.ViewModels;
using WMS.Manager.GrpcClient.Clients;
using WMS.Manager.Infrastructure.Services;
using WMS.Manager.NomenclatureType;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.Nomenclature
{
    public class NomenclaturePageViewModel : PageViewModel<
        NomenclatureGrpc,
        NomenclatureViewModel,
        NomenclatureEditorViewModel>
    {
        private readonly DialogService _serviceDialog;
        private RelayCommand _searchCommand;

        public NomenclaturePageViewModel(WmsGrpcClient grpcClient, DialogService serviceDialog) : base(grpcClient)
        {
            _serviceDialog = serviceDialog;
            LoadNomenclatureTypes();
        }

        public new NomenclatureEditorViewModel Editor => (NomenclatureEditorViewModel)base.Editor;
        public ObservableCollection<NomenclatureTypeViewModel> NomenclatureTypes { get; } = new();

        public RelayCommand SearchCommand => _searchCommand ??= new(async () =>
        {
            NomenclatureSearchDialog dialog = await _serviceDialog.ShowNomenclatureSearchDialogAsync(NomenclatureTypes);
            if (dialog.IsDone == false)
            {
                return;
            }

            Items.Clear();

            RequestResult<NomenclatureList> result = await GrpcClient.NomenclatureSearchAsync(new NomenclatureSearchFilter()
            {
                NomenclatureId = dialog.NomenclatureIdResult,
                NomenclatureName = dialog.NomenclatureNameResult,
                NomenclatureTypeId = dialog.NomenclatureTypeIdResult
            });

            if (result.IsSuccess)
            {
                foreach (NomenclatureGrpc item in result.Response.Nomenclatures)
                {
                    NomenclatureViewModel vm = new();
                    vm.SetModel(item);
                    Items.Add(vm);
                }
            }
        });

        protected override async Task<RequestResult<NomenclatureGrpc>> InsertAsync() =>
            await GrpcClient.NomenclatureInsertAsync(Editor.GetNewGrpcModel());
        protected override async Task<RequestResult<NomenclatureGrpc>> UpdateAsync() =>
            await GrpcClient.NomenclatureUpdateAsync(Editor.GetNewGrpcModel());

        protected override void UpdateSelectedItem(NomenclatureViewModel selectedItem)
        {
            NomenclatureTypeGrpc nomenclatureType = NomenclatureTypes.First(t => t.Id == selectedItem.Type.Id).Model;
            selectedItem.UpdateType(nomenclatureType);
        }

        private void EditorPropertyChangedHandler(object sender, PropertyChangedEventArgs e) =>
            SaveCommand.NotifyCanExecuteChanged();


        private async void LoadNomenclatureTypes()
        {
            RequestResult<NomenclatureTypeList> result = await GrpcClient.NomenclatureTypeGetAllAsync();
            if (result.IsSuccess)
            {
                foreach (NomenclatureTypeGrpc type in result.Response.NomenclatureTypes)
                {
                    NomenclatureTypeViewModel vm = new();
                    vm.SetModel(type);
                    NomenclatureTypes.Add(vm);
                }
            }
        }
    }
}