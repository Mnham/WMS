using Microsoft.Toolkit.Mvvm.ComponentModel;

using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.Nomenclature
{
    public class NomenclatureViewModel : ObservableObject
    {
        public NomenclatureGrpc Model { get; private set; }

        public NomenclatureViewModel(NomenclatureGrpc model) => Model = model;

        public long Id => Model.Id;

        public string Name => Model.Name;

        public NomenclatureTypeGrpc Type => Model.Type;

        public long Length => Model.Length;

        public long Width => Model.Width;

        public long Height => Model.Height;

        public int Weight => Model.Weight;

        public void Update(NomenclatureGrpc model)
        {
            Model = model;
            OnPropertyChanged(string.Empty);
        }

        public void UpdateType(NomenclatureTypeGrpc type) => Model.Type = type;
    }
}