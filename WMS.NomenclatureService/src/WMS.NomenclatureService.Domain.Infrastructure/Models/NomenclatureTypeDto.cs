namespace WMS.NomenclatureService.Domain.Infrastructure.Models
{
    /// <summary>
    /// Представляет данные типа номенклатуры.
    /// </summary>
    public sealed class NomenclatureTypeDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }
    }
}