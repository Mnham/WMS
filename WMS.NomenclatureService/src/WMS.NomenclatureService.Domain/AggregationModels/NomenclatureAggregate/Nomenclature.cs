using System;

using WMS.ClassLibrary.Domain.Models;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Exceptions;

namespace WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate
{
    /// <summary>
    /// Представляет номенклатуру.
    /// </summary>
    public class Nomenclature : Entity
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="Nomenclature"/>.
        /// </summary>
        public Nomenclature(
            long id,
            string name,
            NomenclatureType type,
            long length,
            long width,
            long height,
            int weight)
        {
            Id = id;
            SetName(name);
            SetType(type);
            SetLength(length);
            SetWidth(width);
            SetHeight(height);
            SetWeight(weight);
        }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Тип номенклатуры.
        /// </summary>
        public NomenclatureType Type { get; private set; }

        /// <summary>
        /// Длина.
        /// </summary>
        public long Length { get; private set; }

        /// <summary>
        /// Ширина.
        /// </summary>
        public long Width { get; private set; }

        /// <summary>
        /// Высота.
        /// </summary>
        public long Height { get; private set; }

        /// <summary>
        /// Вес.
        /// </summary>
        public int Weight { get; private set; }

        /// <summary>
        /// Устанавливает наименование.
        /// </summary>
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть пустым или содержать только пробел.", nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Устанавливает тип номенклатуры.
        /// </summary>
        public void SetType(NomenclatureType type) => Type = type ?? throw new ArgumentNullException(nameof(type));

        /// <summary>
        /// Устанавливает длину.
        /// </summary>
        public void SetLength(long length)
        {
            if (length <= 0)
            {
                throw new NegativeOrZeroValueException($"{nameof(length)} value must be positive");
            }

            Length = length;
        }

        /// <summary>
        /// Устанавливает ширину.
        /// </summary>
        public void SetWidth(long width)
        {
            if (width <= 0)
            {
                throw new NegativeOrZeroValueException($"{nameof(width)} value must be positive");
            }

            Width = width;
        }

        /// <summary>
        /// Устанавливает высоту.
        /// </summary>
        public void SetHeight(long height)
        {
            if (height <= 0)
            {
                throw new NegativeOrZeroValueException($"{nameof(height)} value must be positive");
            }

            Height = height;
        }

        /// <summary>
        /// Устанавливает вес.
        /// </summary>
        public void SetWeight(int weight)
        {
            if (weight <= 0)
            {
                throw new NegativeOrZeroValueException($"{nameof(weight)} value must be positive");
            }

            Weight = weight;
        }
    }
}