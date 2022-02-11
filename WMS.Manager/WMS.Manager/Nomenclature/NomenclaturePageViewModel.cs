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
    /// <summary>
    /// Представляет ViewModel страницы номенклатуры.
    /// </summary>
    public class NomenclaturePageViewModel : PageViewModel<
        NomenclatureGrpc,
        NomenclatureViewModel,
        NomenclatureEditorViewModel>
    {
        /// <summary>
        /// Сервис диалоговых окон.
        /// </summary>
        private readonly IDialogService _serviceDialog;

        /// <summary>
        /// Команда поиска.
        /// </summary>
        private RelayCommand _searchCommand;

        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclaturePageViewModel"/>.
        /// </summary>
        public NomenclaturePageViewModel(WmsGrpcClient grpcClient, IDialogService serviceDialog) : base(grpcClient)
        {
            _serviceDialog = serviceDialog;
            LoadNomenclatureTypes();
        }

        /// <summary>
        /// Редактор номенклатуры.
        /// </summary>
        public new NomenclatureEditorViewModel Editor => (NomenclatureEditorViewModel)base.Editor;

        /// <summary>
        /// Список типов номенклатуры.
        /// </summary>
        public ObservableCollection<NomenclatureTypeViewModel> NomenclatureTypes { get; } = new();

        /// <summary>
        /// Команда поиска.
        /// </summary>
        public RelayCommand SearchCommand => _searchCommand ??= new(async () =>
          {
              INomenclatureSearchDialog dialog = await _serviceDialog.ShowNomenclatureSearchDialogAsync(NomenclatureTypes);
              if (dialog.IsOK == false)
              {
                  return;
              }

              Items.Clear();

              RequestResult<NomenclatureList> result = await GrpcClient.Nomenclature.SearchAsync(new NomenclatureSearchFilter()
              {
                  NomenclatureId = dialog.NomenclatureIdValue,
                  NomenclatureName = dialog.NomenclatureNameValue,
                  NomenclatureTypeId = dialog.NomenclatureTypeIdValue
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

        /// <summary>
        /// Добавляет номенклатуру.
        /// </summary>
        protected override async Task<RequestResult<NomenclatureGrpc>> InsertAsync() =>
            await GrpcClient.Nomenclature.InsertAsync(Editor.GetNewGrpcModel());

        /// <summary>
        /// Обновляет номенклатуру.
        /// </summary>
        protected override async Task<RequestResult<NomenclatureGrpc>> UpdateAsync() =>
            await GrpcClient.Nomenclature.UpdateAsync(Editor.GetNewGrpcModel());

        /// <summary>
        /// Обвновля
        /// </summary>
        protected override void UpdateSelectedItem(NomenclatureViewModel selectedItem)
        {
            NomenclatureTypeGrpc nomenclatureType = NomenclatureTypes.First(t => t.Id == selectedItem.Type.Id).Model;
            selectedItem.UpdateType(nomenclatureType);
        }

        /// <summary>
        /// Обновляет сущность во ViewModel.
        /// </summary>
        private void EditorPropertyChangedHandler(object sender, PropertyChangedEventArgs e) =>
            SaveCommand.NotifyCanExecuteChanged();

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
                    NomenclatureTypes.Add(vm);
                }
            }
        }
    }
}