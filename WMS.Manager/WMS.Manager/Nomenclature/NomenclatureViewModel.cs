using Microsoft.Toolkit.Mvvm.ComponentModel;

using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.Nomenclature
{
    public class NomenclatureViewModel : ObservableObject
    {
        private NomenclatureGrpc _model;

        public NomenclatureViewModel(NomenclatureGrpc model) => _model = model;

        public long Id => _model.Id;

        public string Name => _model.Name;

        public NomenclatureTypeGrpc Type => _model.Type;

        public long Length => _model.Length;

        public long Width => _model.Width;

        public long Height => _model.Height;

        public int Weight => _model.Weight;

        public void Update(NomenclatureGrpc model)
        {
            _model = model;
            OnPropertyChanged(string.Empty);
        }

        public void UpdateType(NomenclatureTypeGrpc type)
        {
            _model.Type = type;
        }
    }
}