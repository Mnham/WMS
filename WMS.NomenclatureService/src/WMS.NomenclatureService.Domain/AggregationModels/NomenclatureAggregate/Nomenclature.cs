using System;

using WMS.ClassLibrary.Domain.Models;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Exceptions;

namespace WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate
{
    public class Nomenclature : Entity
    {
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

        public string Name { get; private set; }
        public NomenclatureType Type { get; private set; }
        public long Length { get; private set; }
        public long Width { get; private set; }
        public long Height { get; private set; }
        public int Weight { get; private set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть пустым или содержать только пробел.", nameof(name));
            }

            Name = name;
        }

        public void SetType(NomenclatureType type) => Type = type ?? throw new ArgumentNullException(nameof(type));

        public void SetLength(long length)
        {
            if (length <= 0)
            {
                throw new NegativeOrZeroValueException($"{nameof(length)} value must be positive");
            }

            Length = length;
        }

        public void SetWidth(long width)
        {
            if (width <= 0)
            {
                throw new NegativeOrZeroValueException($"{nameof(width)} value must be positive");
            }

            Width = width;
        }

        public void SetHeight(long height)
        {
            if (height <= 0)
            {
                throw new NegativeOrZeroValueException($"{nameof(height)} value must be positive");
            }

            Height = height;
        }

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