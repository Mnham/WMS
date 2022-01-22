using Google.Protobuf;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WMS.Manager.Domain.ViewModels
{
    public abstract class GrpcViewModel<T> : ObservableObject where T : IMessage<T>
    {
        public T Model { get; private set; }

        public void SetModel(T model) => Model = model;

        public void Update(T model)
        {
            SetModel(model);
            OnPropertyChanged(string.Empty);
        }
    }
}