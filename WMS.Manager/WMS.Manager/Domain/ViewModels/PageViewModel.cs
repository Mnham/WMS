using Google.Protobuf;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

using WMS.Manager.Domain.Interfaces;
using WMS.Manager.GrpcClient.Clients;

namespace WMS.Manager.Domain.ViewModels
{
    /// <summary>
    /// Представляет базовую ViewModel страницы редактивания сущностей.
    /// </summary>
    public abstract class PageViewModel<TGrpcModel, TGrpcViewModel, TGrpcModelEditor> : ObservableObject
        where TGrpcModel : IMessage<TGrpcModel>
        where TGrpcViewModel : GrpcViewModel<TGrpcModel>, new()
        where TGrpcModelEditor : IGrpcModelEditor<TGrpcModel, TGrpcViewModel>, new()
    {
        /// <summary>
        /// Клиент Grpc.
        /// </summary>
        protected readonly WmsGrpcClient GrpcClient;

        /// <summary>
        /// Команда для включения режима создания сущности.
        /// </summary>
        private RelayCommand _addCommand;

        /// <summary>
        /// Режим редактирования сущности.
        /// </summary>
        private EditorMode _editorMode;

        /// <summary>
        /// Команда сохранения изменений.
        /// </summary>
        private RelayCommand _saveCommand;

        /// <summary>
        /// Выбранная сущность.
        /// </summary>
        private TGrpcViewModel _selectedItem;

        /// <summary>
        /// Создает экземпляр класса <see cref="PageViewModel"/>.
        /// </summary>
        public PageViewModel(WmsGrpcClient grpcClient)
        {
            GrpcClient = grpcClient;
            Editor.PropertyChanged += EditorPropertyChangedHandler;
        }

        /// <summary>
        /// Команда для включения режима создания сущности.
        /// </summary>
        public RelayCommand AddCommand => _addCommand ??= new(() =>
        {
            EditorMode = EditorMode.Create;
            SelectedItem = null;
        });

        /// <summary>
        /// Редактор Grpc-модели.
        /// </summary>
        public IGrpcModelEditor<TGrpcModel, TGrpcViewModel> Editor { get; } = new TGrpcModelEditor();

        /// <summary>
        /// Режим редактирования сущности.
        /// </summary>
        public EditorMode EditorMode
        {
            get => _editorMode;
            set
            {
                _editorMode = value;
                OnPropertyChanged(nameof(IsCreateMode));
            }
        }

        /// <summary>
        ///  Указывает, что включен режим редактирования.
        /// </summary>
        public bool IsCreateMode => EditorMode == EditorMode.Create;

        /// <summary>
        /// Список сущностей.
        /// </summary>
        public ObservableCollection<TGrpcViewModel> Items { get; } = new();

        /// <summary>
        /// Команда сохранения изменений.
        /// </summary>
        public RelayCommand SaveCommand => _saveCommand ??= new(async () =>
        {
            switch (EditorMode)
            {
                case EditorMode.Edit:
                    RequestResult<TGrpcModel> updateResult = await UpdateAsync();
                    if (updateResult.IsSuccess)
                    {
                        SelectedItem.Update(updateResult.Response);
                    }

                    break;

                case EditorMode.Create:
                    RequestResult<TGrpcModel> insertResult = await InsertAsync();
                    if (insertResult.IsSuccess)
                    {
                        TGrpcViewModel viewModel = new();
                        viewModel.SetModel(insertResult.Response);
                        Items.Add(viewModel);
                        SelectedItem = viewModel;
                    }

                    break;
            }

            SaveCommand.NotifyCanExecuteChanged();
        }, () => EditorMode switch
        {
            EditorMode.Edit => Editor.CanSaveChange() && SelectedItem?.Model.Equals(Editor.GetNewGrpcModel()) == false,
            EditorMode.Create => Editor.CanSaveChange(),
            _ => false,
        });

        /// <summary>
        /// Выбранная сущность.
        /// </summary>
        public TGrpcViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                if (value is null)
                {
                    Editor.Reset();
                }
                else
                {
                    UpdateSelectedItem(value);
                    Editor.Update(value);
                    EditorMode = EditorMode.Edit;
                }
            }
        }

        /// <summary>
        /// Добавляет новую сущность.
        /// </summary>
        protected abstract Task<RequestResult<TGrpcModel>> InsertAsync();

        /// <summary>
        /// Обновляет сущность в базе данных.
        /// </summary>
        protected abstract Task<RequestResult<TGrpcModel>> UpdateAsync();

        /// <summary>
        /// Обновляет сущность во ViewModel.
        /// </summary>
        protected virtual void UpdateSelectedItem(TGrpcViewModel selectedItem)
        {
        }

        /// <summary>
        /// Обрабатывает событие изменения поля в редакторе сущности.
        /// </summary>
        private void EditorPropertyChangedHandler(object sender, PropertyChangedEventArgs e) =>
            SaveCommand.NotifyCanExecuteChanged();
    }
}