using Microsoft.Toolkit.Mvvm.ComponentModel;

using WMS.Manager.Domain.Interfaces;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.NomenclatureType
{
    public class NomenclatureTypeEditorViewModel : ObservableObject, IGrpcModelEditor<NomenclatureTypeGrpc, NomenclatureTypeViewModel>
    {
        private string id;
        private string name;

        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public bool CanSaveChange() => string.IsNullOrWhiteSpace(Name) == false;

        public NomenclatureTypeGrpc GetNewGrpcModel() => new()
        {
            Id = long.TryParse(Id, out long id) ? id : 0,
            Name = Name
        };

        public void Reset()
        {
            Name = default;
            Id = default;
        }

        public void Update(NomenclatureTypeViewModel viewModel)
        {
            Name = viewModel.Name;
            Id = viewModel.Id.ToString();
        }
    }
}