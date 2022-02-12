using System.Collections.Generic;

namespace WMS.Microservice.Infrastructure.Models
{
    /// <summary>
    /// Представляет интерфейс модели с коллекцией элементов.
    /// </summary>
    public interface IItemsModel<TItemsModel> where TItemsModel : class
    {
        /// <summary>
        /// Коллекция элементов.
        /// </summary>
        IReadOnlyList<TItemsModel> Items { get; set; }
    }
}