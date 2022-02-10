using Microsoft.Extensions.DependencyInjection;

using Windows.UI.Xaml.Controls;

namespace WMS.Manager.UWP.Nomenclature
{
    /// <summary>
    /// Представляет страницу номенклатуры.
    /// </summary>
    public sealed partial class NomenclaturePage : Page
    {
        /// <summary>
        /// ViewModel.
        /// </summary>
        private readonly NomenclaturePageViewModel VM = App.Current.Services.GetService<NomenclaturePageViewModel>();

        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclaturePage"/>.
        /// </summary>
        public NomenclaturePage() => InitializeComponent();
    }
}