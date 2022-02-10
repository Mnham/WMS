using System;

namespace WMS.Manager.Infrastructure.Helpers
{
    /// <summary>
    /// Представляет вспомогательные методы для перечислений.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Возвращает <see cref="Enum"/> из строкового значения.
        /// </summary>
        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct, Enum =>
            (TEnum)Enum.Parse(typeof(TEnum), text);
    }
}


