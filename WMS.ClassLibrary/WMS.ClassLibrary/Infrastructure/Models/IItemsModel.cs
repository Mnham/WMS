using System.Collections.Generic;

namespace WMS.ClassLibrary.Infrastructure.Models
{
    public interface IItemsModel<TItemsModel> where TItemsModel : class
    {
        IReadOnlyList<TItemsModel> Items { get; set; }
    }
}