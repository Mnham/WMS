using Google.Protobuf;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WMS.Manager.Domain.ViewModels
{
    /// <summary>
    /// Представляет ViewModel для класса Grpc.
    /// </summary>
    public abstract class GrpcViewModel<T> : ObservableObject where T : IMessage<T>
    {
        /// <summary>
        /// Модель.
        /// </summary>
        public T Model { get; private set; }

        /// <summary>
        /// Устанавливает модель.
        /// </summary>
        public void SetModel(T model) => Model = model;

        /// <summary>
        /// Обновляет модель.
        /// </summary>
        public void Update(T model)
        {
            SetModel(model);
            OnPropertyChanged(string.Empty);
        }
    }
}