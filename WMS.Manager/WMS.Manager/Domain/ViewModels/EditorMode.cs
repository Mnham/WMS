namespace WMS.Manager.Domain.ViewModels
{
    /// <summary>
    /// Представляет режим редактирования сущности.
    /// </summary>
    public enum EditorMode
    {
        /// <summary>
        /// Режим не выбран.
        /// </summary>
        None,

        /// <summary>
        /// Режим редактирования.
        /// </summary>
        Edit,

        /// <summary>
        /// Режим создания.
        /// </summary>
        Create
    }
}
