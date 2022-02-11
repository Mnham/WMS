using Microsoft.Extensions.DependencyInjection;

using Windows.UI.Xaml.Controls;

using WMS.Manager.NomenclatureType;

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
        private NomenclatureTypePageViewModel VM { get; } = App.Current.Services.GetService<NomenclatureTypePageViewModel>();

        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureTypePage"/>.
        /// </summary>
        public NomenclatureTypePage() => InitializeComponent();
    }
}