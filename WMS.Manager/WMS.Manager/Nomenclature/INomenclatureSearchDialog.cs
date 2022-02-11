namespace WMS.Manager.Nomenclature
{
    public interface INomenclatureSearchDialog
    {
        bool IsOK { get; }
        long? NomenclatureIdValue { get; }
        string NomenclatureNameValue { get; }
        long? NomenclatureTypeIdValue { get; }
    }
}