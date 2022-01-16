using WMS.ClassLibrary.Domain.Models;

namespace WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate
{
    public class NomenclatureType : Entity
    {
        public string Name { get; private set; }

        public NomenclatureType(long id, string name)
        {
            Id = id;
            SetName(name);
        }

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