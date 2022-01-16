using System;

namespace WMS.Manager.Infrastructure.Helpers;

public static class EnumHelper
{
    public static TEnum GetEnum<TEnum>(string text) where TEnum : struct, Enum =>
        (TEnum)Enum.Parse(typeof(TEnum), text);
}
