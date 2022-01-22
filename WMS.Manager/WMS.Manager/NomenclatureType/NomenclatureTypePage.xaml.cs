using Microsoft.Extensions.DependencyInjection;

using Windows.UI.Xaml.Controls;

namespace WMS.Manager.NomenclatureType
{

    public sealed partial class NomenclatureTypePage : Page
    {
        private readonly NomenclatureTypePageViewModel VM = App.Current.Services.GetService<NomenclatureTypePageViewModel>();

        public NomenclatureTypePage() => InitializeComponent();
    }
}
