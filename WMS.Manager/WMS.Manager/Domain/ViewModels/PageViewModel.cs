using Google.Protobuf;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

using WMS.Manager.Domain.Interfaces;
using WMS.Manager.GrpcClient.Clients;
using WMS.Manager.Nomenclature;

namespace WMS.Manager.Domain.ViewModels
{
    public abstract class PageViewModel<TGrpcModel, TGrpcViewModel, TGrpcModelEditor> : ObservableObject
        where TGrpcModel : IMessage<TGrpcModel>
        where TGrpcViewModel : GrpcViewModel<TGrpcModel>, new()
        where TGrpcModelEditor : IGrpcModelEditor<TGrpcModel, TGrpcViewModel>, new()
    {
        protected readonly WmsGrpcClient GrpcClient;
        private RelayCommand _addCommand;
        private EditorMode _editorMode;
        private RelayCommand _saveCommand;
        private TGrpcViewModel _selectedItem;

        public PageViewModel(WmsGrpcClient grpcClient)
        {
            GrpcClient = grpcClient;
            Editor.PropertyChanged += EditorPropertyChangedHandler;
        }

        public RelayCommand AddCommand => _addCommand ??= new(() =>
        {
            EditorMode = EditorMode.Create;
            SelectedItem = null;
        });

        public IGrpcModelEditor<TGrpcModel, TGrpcViewModel> Editor { get; } = new TGrpcModelEditor();

        public EditorMode EditorMode
        {
            get => _editorMode;
            set
            {
                _editorMode = value;
                OnPropertyChanged(nameof(IsCreateMode));
            }
        }

        public bool IsCreateMode => EditorMode == EditorMode.Create;

        public ObservableCollection<TGrpcViewModel> Items { get; } = new();

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

        protected abstract Task<RequestResult<TGrpcModel>> InsertAsync();

        protected abstract Task<RequestResult<TGrpcModel>> UpdateAsync();

        protected virtual void UpdateSelectedItem(TGrpcViewModel selectedItem)
        {
        }

        private void EditorPropertyChangedHandler(object sender, PropertyChangedEventArgs e) =>
            SaveCommand.NotifyCanExecuteChanged();
    }
}