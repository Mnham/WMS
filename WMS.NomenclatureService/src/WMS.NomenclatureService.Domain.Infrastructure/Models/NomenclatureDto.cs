namespace WMS.NomenclatureService.Domain.Infrastructure.Models
{
    /// <summary>
    /// Представляет данные номенклатуры.
    /// </summary>
    public sealed class NomenclatureDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип номенклатуры.
        /// </summary>
        public NomenclatureTypeDto Type { get; set; }

        /// <summary>
        /// Длина.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Ширина.
        /// </summary>
        public long Width { get; set; }

        /// <summary>
        /// Высота.
        /// </summary>
        public long Height { get; set; }

        /// <summary>
        /// Вес.
        /// </summary>
        public int Weight { get; set; }
    }
}