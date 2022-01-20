using Microsoft.Toolkit.Mvvm.ComponentModel;

using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.Nomenclature
{
    public class NomenclatureEditorViewModel : ObservableObject
    {
        private string id;
        private string name;
        private NomenclatureTypeGrpc type;
        private string length;
        private string width;
        private string height;
        private string weight;

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

        public NomenclatureTypeGrpc Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        public string Length
        {
            get => length;
            set => SetProperty(ref length, value);
        }

        public string Width
        {
            get => width;
            set => SetProperty(ref width, value);
        }

        public string Height
        {
            get => height;
            set => SetProperty(ref height, value);
        }

        public string Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }

        public bool CanSaveChange() =>
            string.IsNullOrWhiteSpace(Name) == false
            && Type is not null
            && string.IsNullOrWhiteSpace(Length) == false
            && string.IsNullOrWhiteSpace(Width) == false
            && string.IsNullOrWhiteSpace(Height) == false
            && string.IsNullOrWhiteSpace(Weight) == false;

        public NomenclatureGrpc GetNewNomenclatureGrpc() => new()
        {
            Id = long.TryParse(Id, out long id) ? id : 0,
            Name = Name,
            Type = Type,
            Length = long.TryParse(Length, out long length) ? length : 0,
            Width = long.TryParse(Width, out long width) ? width : 0,
            Height = long.TryParse(Height, out long height) ? height : 0,
            Weight = int.TryParse(Weight, out int weight) ? weight : 0
        };

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
    }
}