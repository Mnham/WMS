using Microsoft.Extensions.DependencyInjection;

using Windows.UI.Xaml.Controls;

using WMS.Manager.Nomenclature;

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
        private NomenclaturePageViewModel VM { get; } = App.Current.Services.GetService<NomenclaturePageViewModel>();

        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclaturePage"/>.
        /// </summary>
        public NomenclaturePage() => InitializeComponent();
    }
}