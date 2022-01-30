using WMS.ClassLibrary.Domain.Models;

namespace WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate
{
    /// <summary>
    /// Представляет тип номенклатуры.
    /// </summary>
    public class NomenclatureType : Entity
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureType"/>.
        /// </summary>
        public NomenclatureType(long id, string name)
        {
            Id = id;
            SetName(name);
        }

        /// <summary>
        /// Устанавливает наименование.
        /// </summary>
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException($"\"{nameof(name)}\" не может быть пустым или содержать только пробел.", nameof(name));
            }

            Name = name;
        }
    }
}