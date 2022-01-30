using System.ComponentModel;

namespace WMS.Manager.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс редактирования Grpc-модели.
    /// </summary>
    public interface IGrpcModelEditor<IGrpcModel, IViewModel> : INotifyPropertyChanged
    {
        /// <summary>
        /// Возвращает <see langword="true"> если возможно сохранить и изменения, иначе  <see langword="false">.
        /// </summary>
        bool CanSaveChange();

        /// <summary>
        /// Возвращает новую Grpc-модель. 
        /// </summary>
        IGrpcModel GetNewGrpcModel();

        /// <summary>
        /// Сбрасывает все значения полей для редактирования.
        /// </summary>
        void Reset();

        /// <summary>
        /// Обновляет ViewModel.
        /// </summary>
        void Update(IViewModel viewModel);
    }
}