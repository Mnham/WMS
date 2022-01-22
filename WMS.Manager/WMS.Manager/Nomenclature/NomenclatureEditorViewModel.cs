using Microsoft.Toolkit.Mvvm.ComponentModel;

using WMS.Manager.Domain.Interfaces;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.Nomenclature
{
    public class NomenclatureEditorViewModel : ObservableObject, IGrpcModelEditor<NomenclatureGrpc, NomenclatureViewModel>
    {
        private string _height;
        private string _id;
        private string _length;
        private string _name;
        private NomenclatureTypeGrpc _type;
        private string _weight;
        private string _width;

        public string Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Length
        {
            get => _length;
            set => SetProperty(ref _length, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public NomenclatureTypeGrpc Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }
        public string Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        public string Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        public bool CanSaveChange() =>
            string.IsNullOrWhiteSpace(Name) == false
            && Type is not null
            && string.IsNullOrWhiteSpace(Length) == false
            && string.IsNullOrWhiteSpace(Width) == false
            && string.IsNullOrWhiteSpace(Height) == false
            && string.IsNullOrWhiteSpace(Weight) == false;

        public NomenclatureGrpc GetNewGrpcModel() => new()
        {
            Id = long.TryParse(Id, out long id) ? id : 0,
            Name = Name,
            Type = Type,
            Length = long.TryParse(Length, out long length) ? length : 0,
            Width = long.TryParse(Width, out long width) ? width : 0,
            Height = long.TryParse(Height, out long height) ? height : 0,
            Weight = int.TryParse(Weight, out int weight) ? weight : 0
        };

        public void Reset()
        {
            Name = default;
            Type = default;
            Length = default;
            Width = default;
            Height = default;
            Weight = default;
            Id = default;
        }

        public void Update(NomenclatureViewModel model)
        {
            Name = model.Name;
            Type = model.Type;
            Length = model.Length.ToString();
            Width = model.Width.ToString();
            Height = model.Height.ToString();
            Weight = model.Weight.ToString();
            Id = model.Id.ToString();
        }
    }
}