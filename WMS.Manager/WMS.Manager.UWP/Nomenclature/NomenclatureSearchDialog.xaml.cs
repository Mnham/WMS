using System.Collections.Generic;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WMS.Manager.Nomenclature;
using WMS.Manager.NomenclatureType;

namespace WMS.Manager.UWP.Nomenclature
{
    /// <summary>
    /// Представляет диалоговое окно поиска номенклатуры.
    /// </summary>
    public sealed partial class NomenclatureSearchDialog : ContentDialog, INomenclatureSearchDialog
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureSearchDialog"/>.
        /// </summary>
        public NomenclatureSearchDialog(IReadOnlyCollection<NomenclatureTypeViewModel> types)
        {
            NomenclatureTypes = types;
            InitializeComponent();
        }

        /// <summary>
        /// Указывает, что диалоговое окно закрыто кнопкой "ОК".
        /// </summary>
        public bool IsOK { get; private set; }

        /// <summary>
        /// Значение идентификатора номенклатуры.
        /// </summary>
        public long? NomenclatureIdValue => long.TryParse(NomenclatureId, out long value) ? value : null;

        /// <summary>
        /// Значение имени номенклатуры.
        /// </summary>
        public string NomenclatureNameValue => string.IsNullOrWhiteSpace(NomenclatureName) ? null : NomenclatureName;

        /// <summary>
        /// Значение типа номенклатуры.
        /// </summary>
        public long? NomenclatureTypeIdValue => NomenclatureTypeId == 0 ? null : NomenclatureTypeId;

        /// <summary>
        /// Типы номенклатур.
        /// </summary>
        private IReadOnlyCollection<NomenclatureTypeViewModel> NomenclatureTypes { get; }

        /// <summary>
        /// Идентификатор номенклатуры.
        /// </summary>
        private string NomenclatureId { get; set; }

        /// <summary>
        /// Имя номенклатуры.
        /// </summary>
        private string NomenclatureName { get; set; }

        /// <summary>
        /// Идентификатор типа оменклатуры.
        /// </summary>
        private long NomenclatureTypeId { get; set; }

        /// <summary>
        /// Обрабатывает нажатие кнопки отмены.
        /// </summary>
        private void CancelButtonClickHandler(object sender, RoutedEventArgs e) => Hide();

        /// <summary>
        /// Обрабатывает нажатие кнопки поиска.
        /// </summary>
        private void FindButtonClickHandler(object sender, RoutedEventArgs e)
        {
            IsOK = true;
            Hide();
        }
    }
}