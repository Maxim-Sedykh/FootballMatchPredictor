using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                ?.GetName() ?? "Неопределенный";
        }

        public static TEnum GetEnumFromDisplay<TEnum>(string displayName) where TEnum : Enum
        {
            var enumType = typeof(TEnum);

            if (int.TryParse(displayName, out int numericValue) && Enum.IsDefined(enumType, numericValue))
            {
                return (TEnum)Enum.ToObject(enumType, numericValue);
            }

            var members = enumType.GetMembers();

            foreach (var member in members)
            {
                var displayAttribute = member.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

                if (displayAttribute != null && displayAttribute.GetName() == displayName)
                {
                    return (TEnum)Enum.Parse(enumType, member.Name);
                }
            }

            throw new ArgumentException($"Enum value with display name '{displayName}' not found.");
        }
    }
}
