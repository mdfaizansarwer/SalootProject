using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Core.Utilities
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get values from enum.
        /// </summary>
        /// <typeparam name="T">Struct</typeparam>
        /// <param name="input">The enum for getting its values.</param>
        /// <returns>IEnumerable contains all of enum values.</returns>
        /// <exception cref="NotSupportedException">Occured when input is not enum.</exception>
        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (typeof(T).IsEnum is false)
            {
                throw new NotSupportedException();
            }

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        /// <summary>
        /// Get flag values from enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The enum for getting its flags.</param>
        /// <returns>IEnumerable contains all of value flags.</returns>
        /// <exception cref="NotSupportedException">Occured when input is not enum.</exception>
        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException();
            }

            foreach (var value in Enum.GetValues(input.GetType()))
            {
                if ((input as Enum).HasFlag(value as Enum))
                {
                    yield return (T)value;
                }
            }
        }

        /// <summary>
        /// Get DisplayProperty of enum in string format.
        /// </summary>
        /// <param name="value">The enum for getting its display property.</param>
        /// <param name="displayProperty"></param>
        /// <returns>DisplayProperty of enum in string format.</returns>
        /// <exception cref="ArgumentNullException">Occured when value that passed to function null.</exception>
        public static string ToDisplay(this Enum value, DisplayProperty displayProperty = DisplayProperty.Name)
        {
            if (value is null)
            {
                throw new ArgumentNullException($"{nameof(value)} : {typeof(Enum)}");
            }

            var attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false)
                .FirstOrDefault();

            if (attribute == null)
            {
                return value.ToString();
            }

            var propValue = attribute.GetType().GetProperty(displayProperty.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        /// <summary>
        /// Get dictionary of enam values with their constant names.
        /// </summary>
        /// <param name="value">The enum for getting its values with their constant names.</param>
        /// <returns>Enam values with their constant names in Dictionary format</returns>
        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => ToDisplay(q));
        }
    }
}