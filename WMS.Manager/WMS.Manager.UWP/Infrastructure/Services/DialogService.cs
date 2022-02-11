﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;

using WMS.Manager.Infrastructure.Services;
using WMS.Manager.Nomenclature;
using WMS.Manager.NomenclatureType;
using WMS.Manager.UWP.Infrastructure.Helpers;
using WMS.Manager.UWP.Nomenclature;

namespace WMS.Manager.UWP.Infrastructure.Services
{
    /// <summary>
    /// Сервис диалоговых окон.
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Диалоговое окно.
        /// </summary>
        private ContentDialog _currentDialog;

        /// <summary>
        /// Показывает диалоговое окно исключения.
        /// </summary>
        public async Task ShowExceptionDialogAsync(Exception ex)
        {
            _currentDialog?.Hide();
            _currentDialog = new ExceptionDialog(ex)
            {
                RequestedTheme = ThemeHelper.ActualTheme
            };
            await _currentDialog.ShowAsync();
        }

        /// <summary>
        /// Показывает диалоговое окно поиска номенклатуры.
        /// </summary>
        public async Task<INomenclatureSearchDialog> ShowNomenclatureSearchDialogAsync(IReadOnlyCollection<NomenclatureTypeViewModel> types)
        {
            NomenclatureSearchDialog dialog = new(types)
            {
                RequestedTheme = ThemeHelper.ActualTheme
            };

            if (_currentDialog?.IsLoaded == true)
            {
                return dialog;
            }

            _currentDialog = dialog;
            await dialog.ShowAsync();

            return dialog;
        }
    }
}