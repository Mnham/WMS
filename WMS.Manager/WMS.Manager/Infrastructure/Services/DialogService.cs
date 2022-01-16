﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;

using WMS.Manager.Nomenclature;

namespace WMS.Manager.Infrastructure.Services;

public class DialogService
{
    private ContentDialog _currentDialog;

    public async Task ShowExceptionDialogAsync(Exception ex)
    {
        _currentDialog?.Hide();
        _currentDialog = new ExceptionDialog(ex);
        await _currentDialog.ShowAsync();
    }

    public async Task<NomenclatureSearchDialog> ShowNomenclatureSearchDialogAsync(IReadOnlyCollection<NomenclatureTypeViewModel> types)
    {
        NomenclatureSearchDialog dialog = new(types);
        if (_currentDialog?.IsLoaded == true)
        {
            return dialog;
        }

        _currentDialog = dialog;
        await dialog.ShowAsync();

        return dialog;
    }
}
