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

        public void Update(NomenclatureViewModel model)
        {
            Id = model.Id.ToString();
            Name = model.Name;
            Type = model.Type;
            Length = model.Length.ToString();
            Width = model.Width.ToString();
            Height = model.Height.ToString();
            Weight = model.Weight.ToString();
        }

        public void Reset()
        {
            Id = default;
            Name = default;
            Type = default;
            Length = default;
            Width = default;
            Height = default;
            Weight = default;
        }
    }
}