using Microsoft.Extensions.DependencyInjection;

using Windows.UI.Xaml.Controls;

namespace WMS.Manager.Nomenclature
{
    public sealed partial class NomenclaturePage : Page
    {
        private readonly NomenclaturePageViewModel VM = App.Current.Services.GetService<NomenclaturePageViewModel>();

        public NomenclaturePage() => InitializeComponent();
    }
}