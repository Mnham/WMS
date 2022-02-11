using Microsoft.Toolkit.Mvvm.ComponentModel;

using WMS.Manager.Domain.Interfaces;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.NomenclatureType
{
    /// <summary>
    /// Представляет ViewModel редактора типа номенклатуры.
    /// </summary>
    public class NomenclatureTypeEditorViewModel : ObservableObject, IGrpcModelEditor<NomenclatureTypeGrpc, NomenclatureTypeViewModel>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        private string id;

        /// <summary>
        /// Наименование.
        /// </summary>
        private string name;

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        /// <summary>
        /// Возвращает <see langword="true"> если возможно сохранить и изменения, иначе  <see langword="false">.
        /// </summary>
        public bool CanSaveChange() => string.IsNullOrWhiteSpace(Name) == false;

        /// <summary>
        /// Возвращает новую Grpc-модель.
        /// </summary>
        public NomenclatureTypeGrpc GetNewGrpcModel() => new()
        {
            Id = long.TryParse(Id, out long id) ? id : 0,
            Name = Name
        };

        /// <summary>
        /// Сбрасывает все значения полей для редактирования.
        /// </summary>
        public void Reset()
        {
            Name = default;
            Id = default;
        }

        /// <summary>
        /// Обновляет ViewModel.
        /// </summary>
        public void Update(NomenclatureTypeViewModel viewModel)
        {
            Name = viewModel.Name;
            Id = viewModel.Id.ToString();
        }
    }
}