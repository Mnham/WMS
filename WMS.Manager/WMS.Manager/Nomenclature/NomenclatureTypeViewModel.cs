using Microsoft.Toolkit.Mvvm.ComponentModel;

using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.Nomenclature
{
    public class NomenclatureTypeViewModel : ObservableObject
    {
        private readonly NomenclatureTypeGrpc _model;

        public NomenclatureTypeViewModel(NomenclatureTypeGrpc model) => _model = model;

        public NomenclatureTypeGrpc Model => _model;

        public long Id => _model.Id;

        public string Name
        {
            get => _model.Name;
            set => _model.Name = value;
        }
    }
}