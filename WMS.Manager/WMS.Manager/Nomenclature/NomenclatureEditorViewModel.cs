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
            set
            {
                SetProperty(ref _height, value);
                OnPropertyChanged(nameof(Volume));
            }
        }

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Length
        {
            get => _length;
            set
            {
                SetProperty(ref _length, value);
                OnPropertyChanged(nameof(Volume));
            }
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

        public string Volume =>
            string.IsNullOrWhiteSpace(Length)
            && string.IsNullOrWhiteSpace(Width)
            && string.IsNullOrWhiteSpace(Height) ? null : GetVolume().ToString("F9");

        public string Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        public string Width
        {
            get => _width;
            set
            {
                SetProperty(ref _width, value);
                OnPropertyChanged(nameof(Volume));
            }
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
            Id = GetValue(Id),
            Name = Name,
            Type = Type,
            Length = GetValue(Length),
            Width = GetValue(Width),
            Height = GetValue(Height),
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

        private long GetValue(string text) => long.TryParse(text, out long value) ? value : 0;
        private double GetVolume() => GetValue(Length) * GetValue(Width) * GetValue(Height) / 1000000000.0;
    }
}