using System.ComponentModel;

namespace WMS.Manager.Domain.Interfaces
{
    public interface IGrpcModelEditor<IGrpcModel, IViewModel> : INotifyPropertyChanged
    {
        bool CanSaveChange();
        IGrpcModel GetNewGrpcModel();
        void Reset();
        void Update(IViewModel viewModel);
    }
}