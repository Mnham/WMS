namespace WMS.NomenclatureService.Domain.Infrastructure.Models
{
    public sealed class NomenclatureDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public NomenclatureTypeDto Type { get; set; }
        public long Length { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
        public int Weight { get; set; }
    }
}