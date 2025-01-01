using System.ComponentModel;
using System.Reflection;

namespace DevTest.Extensions;

public static class EnumExtension
{
    /*
     * Convert the enum value to an user-friendly format
     */
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}