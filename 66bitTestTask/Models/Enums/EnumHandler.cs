using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace _66bitTestTask.Models.Enums
{
    public static class EnumHandler
    {
        public static string? GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()?
                            .GetName();
        }
    }
}
