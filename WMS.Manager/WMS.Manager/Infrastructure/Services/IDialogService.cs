using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using WMS.Manager.Nomenclature;
using WMS.Manager.NomenclatureType;

namespace WMS.Manager.Infrastructure.Services
{
    public interface IDialogService
    {
        Task<INomenclatureSearchDialog> ShowNomenclatureSearchDialogAsync(IReadOnlyCollection<NomenclatureTypeViewModel> nomenclatureTypes);
        Task ShowExceptionDialogAsync(Exception ex);
    }
}