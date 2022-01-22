using System.Collections.Generic;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WMS.Manager.NomenclatureType;

namespace WMS.Manager.Nomenclature
{
    public sealed partial class NomenclatureSearchDialog : ContentDialog
    {
        public NomenclatureSearchDialog(IReadOnlyCollection<NomenclatureTypeViewModel> types)
        {
            NomenclatureTypeViewModels = types;
            InitializeComponent();
        }

        public bool IsDone { get; private set; }
        public long? NomenclatureIdResult => long.TryParse(NomenclatureId, out long value) ? value : null;
        public string NomenclatureNameResult => string.IsNullOrWhiteSpace(NomenclatureName) ? null : NomenclatureName;
        public long? NomenclatureTypeIdResult => NomenclatureTypeId == 0 ? null : NomenclatureTypeId;
        public IReadOnlyCollection<NomenclatureTypeViewModel> NomenclatureTypeViewModels { get; }
        private string NomenclatureId { get; set; }

        private string NomenclatureName { get; set; }

        private long NomenclatureTypeId { get; set; }
        private void CancelButtonClickHandler(object sender, RoutedEventArgs e) => Hide();

        private void FindButtonClickHandler(object sender, RoutedEventArgs e)
        {
            IsDone = true;
            Hide();
        }
    }
}