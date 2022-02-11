using Microsoft.Toolkit.Mvvm.ComponentModel;

using WMS.Manager.Domain.Interfaces;
using WMS.NomenclatureService.Grpc;

namespace WMS.Manager.Nomenclature
{
    /// <summary>
    /// Представляет ViewModel редактора номенклатуры.
    /// </summary>
    public class NomenclatureEditorViewModel : ObservableObject, IGrpcModelEditor<NomenclatureGrpc, NomenclatureViewModel>
    {
        /// <summary>
        /// Высота
        /// </summary>
        private string _height;

        /// <summary>
        /// Идентификатор.
        /// </summary>
        private string _id;

        /// <summary>
        /// Длина.
        /// </summary>
        private string _length;

        /// <summary>
        /// Наименование.
        /// </summary>
        private string _name;

        /// <summary>
        /// Тип номенклатуры.
        /// </summary>
        private NomenclatureTypeGrpc _type;

        /// <summary>
        /// Вес.
        /// </summary>
        private string _weight;

        /// <summary>
        /// Ширина.
        /// </summary>
        private string _width;

        /// <summary>
        /// Высота.
        /// </summary>
        public string Height
        {
            get => _height;
            set
            {
                SetProperty(ref _height, value);
                OnPropertyChanged(nameof(Volume));
            }
        }

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// Длина.
        /// </summary>
        public string Length
        {
            get => _length;
            set
            {
                SetProperty(ref _length, value);
                OnPropertyChanged(nameof(Volume));
            }
        }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Тип номенклатуры.
        /// </summary>
        public NomenclatureTypeGrpc Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        /// <summary>
        /// Объем.
        /// </summary>
        public string Volume =>
            string.IsNullOrWhiteSpace(Length)
            && string.IsNullOrWhiteSpace(Width)
            && string.IsNullOrWhiteSpace(Height) ? null : GetVolume().ToString("F9");

        /// <summary>
        /// Вес.
        /// </summary>
        public string Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        /// <summary>
        /// Ширина.
        /// </summary>
        public string Width
        {
            get => _width;
            set
            {
                SetProperty(ref _width, value);
                OnPropertyChanged(nameof(Volume));
            }
        }

        /// <summary>
        /// Возвращает <see langword="true"> если возможно сохранить и изменения, иначе  <see langword="false">.
        /// </summary>
        public bool CanSaveChange() =>
            string.IsNullOrWhiteSpace(Name) == false
            && Type is not null
            && string.IsNullOrWhiteSpace(Length) == false
            && string.IsNullOrWhiteSpace(Width) == false
            && string.IsNullOrWhiteSpace(Height) == false
            && string.IsNullOrWhiteSpace(Weight) == false;

        /// <summary>
        /// Возвращает новую Grpc-модель.
        /// </summary>
        public NomenclatureGrpc GetNewGrpcModel() => new()
        {
            Id = ParseLong(Id),
            Name = Name,
            Type = Type,
            Length = ParseLong(Length),
            Width = ParseLong(Width),
            Height = ParseLong(Height),
            Weight = int.TryParse(Weight, out int weight) ? weight : 0
        };

        /// <summary>
        /// Сбрасывает все значения полей для редактирования.
        /// </summary>
        public void Reset()
        {
            Name = default;
            Type = default;
            Length = default;
            Width = default;
            Height = default;
            Weight = default;
            Id = default;
        }

        /// <summary>
        /// Обновляет ViewModel.
        /// </summary>
        public void Update(NomenclatureViewModel model)
        {
            Name = model.Name;
            Type = model.Type;
            Length = model.Length.ToString();
            Width = model.Width.ToString();
            Height = model.Height.ToString();
            Weight = model.Weight.ToString();
            Id = model.Id.ToString();
        }

        /// <summary>
        /// Парсит <see cref="string"/> в <see cref="long"/>.
        /// </summary>
        private long ParseLong(string text) => long.TryParse(text, out long value) ? value : 0;

        /// <summary>
        /// Возвращает объем.
        /// </summary>
        private double GetVolume() => ParseLong(Length) * ParseLong(Width) * ParseLong(Height) / 1000000000.0;
    }
}