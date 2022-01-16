using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using System.Collections.ObjectModel;
using System.Linq;

using WMS.Manager.GrpcClient.Clients;
using WMS.Manager.Infrastructure.Services;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.Nomenclature
{
    public class NomenclaturePageViewModel : ObservableObject
    {
        private readonly WmsGrpcClient _grpcClient;
        private readonly DialogService _serviceDialog;
        private NomenclatureViewModel selectedNomenclature;

        public NomenclaturePageViewModel(WmsGrpcClient grpcClient, DialogService serviceDialog)
        {
            _grpcClient = grpcClient;
            _serviceDialog = serviceDialog;
            LoadNomenclatureTypes();
        }

        private async void LoadNomenclatureTypes()
        {
            RequestResult<NomenclatureTypeList> result = await _grpcClient.NomenclatureTypeGetAllAsync();
            if (result.IsSuccess)
            {
                foreach (NomenclatureTypeGrpc type in result.Response.NomenclatureTypes)
                {
                    NomenclatureTypes.Add(new NomenclatureTypeViewModel(type));
                }
            }
        }

        public RelayCommand SaveCommand => new(
            async () =>
            {
                RequestResult<NomenclatureGrpc> result = await _grpcClient.NomenclatureUpdateAsync(new NomenclatureGrpc());
                if (result.IsSuccess)
                {
                }
            });

        public RelayCommand AddCommand => new(
            () =>
            {
            });

        public RelayCommand SearchCommand => new(
            async () =>
            {
                NomenclatureSearchDialog dialog = await _serviceDialog.ShowNomenclatureSearchDialogAsync(NomenclatureTypes);
                if (dialog.IsDone == false)
                {
                    return;
                }

                Nomenclatures.Clear();

                RequestResult<NomenclatureList> result = await _grpcClient.NomenclatureSearchAsync(new NomenclatureSearchFilter()
                {
                    NomenclatureId = dialog.NomenclatureIdResult,
                    NomenclatureName = dialog.NomenclatureNameResult,
                    NomenclatureTypeId = dialog.NomenclatureTypeIdResult
                });

                if (result.IsSuccess)
                {
                    foreach (NomenclatureGrpc item in result.Response.Nomenclatures)
                    {
                        Nomenclatures.Add(new NomenclatureViewModel(item));
                    }
                }
            });

        public NomenclatureEditorViewModel Editor { get; } = new();

        public NomenclatureViewModel SelectedNomenclature
        {
            get => selectedNomenclature;
            set
            {
                selectedNomenclature = value;
                if (value is null)
                {
                    Editor.Reset();
                }
                else
                {
                    value.UpdateType(NomenclatureTypes.First(t => t.Id == value.Type.Id).Model);
                    Editor.Update(value);
                }
            }
        }

        public ObservableCollection<NomenclatureTypeViewModel> NomenclatureTypes { get; } = new();

        public ObservableCollection<NomenclatureViewModel> Nomenclatures { get; } = new();
    }
}