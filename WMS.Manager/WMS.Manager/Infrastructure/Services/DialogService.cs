using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;

using WMS.Manager.Infrastructure.Helpers;
using WMS.Manager.Nomenclature;
using WMS.Manager.NomenclatureType;

namespace WMS.Manager.Infrastructure.Services;

public class DialogService
{
    private ContentDialog _currentDialog;

    public async Task ShowExceptionDialogAsync(Exception ex)
    {
        _currentDialog?.Hide();
        _currentDialog = new ExceptionDialog(ex)
        {
            RequestedTheme = ThemeHelper.ActualTheme
        };
        await _currentDialog.ShowAsync();
    }

    public async Task<NomenclatureSearchDialog> ShowNomenclatureSearchDialogAsync(IReadOnlyCollection<NomenclatureTypeViewModel> types)
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
