using Microsoft.Extensions.DependencyInjection;

using Windows.UI.Xaml.Controls;

namespace WMS.Manager.UWP.NomenclatureType
{
    /// <summary>
    /// Представляет страницу типа номенклатуры.
    /// </summary>
    public sealed partial class NomenclatureTypePage : Page
    {
        /// <summary>
        /// ViewModel.
        /// </summary>
        private readonly NomenclatureTypePageViewModel VM = App.Current.Services.GetService<NomenclatureTypePageViewModel>();

        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureTypePage"/>.
        /// </summary>
        public NomenclatureTypePage() => InitializeComponent();
    }
}