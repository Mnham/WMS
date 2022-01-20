using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private NomenclatureViewModel _selectedNomenclature;

        public NomenclaturePageViewModel(WmsGrpcClient grpcClient, DialogService serviceDialog)
        {
            _grpcClient = grpcClient;
            _serviceDialog = serviceDialog;
            LoadNomenclatureTypes();
            Editor.PropertyChanged += EditorPropertyChangedHandler;
        }

        private void EditorPropertyChangedHandler(object sender, PropertyChangedEventArgs e) =>
            SaveCommand.NotifyCanExecuteChanged();

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

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ??= new(async () =>
        {
            switch (EditorMode)
            {
                case EditorMode.Edit:
                    RequestResult<NomenclatureGrpc> updateResult = await _grpcClient.NomenclatureUpdateAsync(Editor.GetNewNomenclatureGrpc());
                    if (updateResult.IsSuccess)
                    {
                        SelectedNomenclature.Update(updateResult.Response);
                    }

                    break;
                case EditorMode.Create:
                    RequestResult<NomenclatureGrpc> insertResult = await _grpcClient.NomenclatureInsertAsync(Editor.GetNewNomenclatureGrpc());
                    if (insertResult.IsSuccess)
                    {
                        NomenclatureViewModel viewModel = new(insertResult.Response);
                        Nomenclatures.Add(viewModel);
                        SelectedNomenclature = viewModel;
                    }

                    break;
            }

            SaveCommand.NotifyCanExecuteChanged();
        }, () => EditorMode switch
        {
            EditorMode.Edit => Editor.CanSaveChange() && SelectedNomenclature?.Model.Equals(Editor.GetNewNomenclatureGrpc()) == false,
            EditorMode.Create => Editor.CanSaveChange(),
            _ => false,
        });

        public bool IsCreateMode => EditorMode == EditorMode.Create;

        public EditorMode EditorMode
        {
            get => _editorMode;
            set
            {
                _editorMode = value;
                OnPropertyChanged(nameof(IsCreateMode));
            }
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand => _addCommand ??= new(() =>
        {
            EditorMode = EditorMode.Create;
            SelectedNomenclature = null;
        });

        private RelayCommand _searchCommand;
        private EditorMode _editorMode;

        public RelayCommand SearchCommand => _searchCommand ??= new(async () =>
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
            get => _selectedNomenclature;
            set
            {
                SetProperty(ref _selectedNomenclature, value);
                if (value is null)
                {
                    Editor.Reset();
                }
                else
                {
                    value.UpdateType(NomenclatureTypes.First(t => t.Id == value.Type.Id).Model);
                    Editor.Update(value);
                    EditorMode = EditorMode.Edit;
                }
            }
        }

        public ObservableCollection<NomenclatureTypeViewModel> NomenclatureTypes { get; } = new();

        public ObservableCollection<NomenclatureViewModel> Nomenclatures { get; } = new();
    }
}