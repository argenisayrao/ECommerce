using System;
using System.ComponentModel;
using System.Globalization;

namespace Ecommerce.Core.Domain
{
    public static class ConvertExtension
    {
        public static int? ToIntNull(this object value) => value.NoValue() ? null : value.ToInt();
        public static long? ToLongNull(this object value) => value.NoValue() ? null : value.ToLong();
        public static bool? ToBoolNull(this object value) => value.NoValue() ? null : value.ToBool();
        public static decimal? ToDecimalNull(this object value) => value.NoValue() ? null : value.ToDecimal();
        public static DateTime? ToDateTimeNull(this object value) => value.NoValue() ? null : value.ToDateTime();
        public static Guid? ToGuidNull(this object value) => value.NoValue() ? null : value.ToGuid();

        public static string ToStr(this object value)
        {
            if (value is null)
                return string.Empty;

            if (value is Enum enumValue)
                return ToDescriptionValue(enumValue);

            return Convert.ToString(value)!;
        }

        public static int ToInt(this object value) => Convert.ToInt32(value);
        public static int ToInt(this Enum value) => Convert.ToInt32(value);
        public static long ToLong(this object value) => Convert.ToInt64(value);
        public static bool ToBool(this object value) => value.IsNumber() ? Convert.ToBoolean(Convert.ToInt32(value)) : Convert.ToBoolean(value);
        public static decimal ToDecimal(this object value) => ToDecimalCulture(value.ToString()!);
        public static DateTime ToDateTime(this object value) => Convert.ToDateTime(value);
        public static Guid ToGuid(this object value) => Guid.Parse(value.ToString()!);

        public static char ToCharFromEnum(this Enum value) => Convert.ToChar(value);
        public static string ToCharString(this Enum value) => Convert.ToChar(value).ToString();


        private static decimal ToDecimalCulture(string value)
        {
            string currencyDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            if (value.Contains('.') && currencyDecimalSeparator == ",")
                value = value.Replace('.', ',');
            else if (value.Contains(',') && currencyDecimalSeparator == ".")
                value = value.Replace(',', '.');

            decimal varDecimal;
            if (!value.ToUpper().Contains('E'))
                varDecimal = Convert.ToDecimal(value);
            else
            {
                // Convert.ToDecimal não consegue converter notação exponencial (ToDouble consegue)...
                double varDouble = Convert.ToDouble(value);
                varDecimal = Convert.ToDecimal(varDouble);
            }
            return varDecimal;
        }

        private static string ToDescriptionValue(Enum enumerador)
        {
            var enumInfo = enumerador.GetType().GetField(enumerador.ToString());
            var enumAttributes = (DescriptionAttribute[])enumInfo!.GetCustomAttributes(typeof(DescriptionAttribute), false);

            var result = enumAttributes.Length > 0 ? enumAttributes[0].Description : enumerador.ToString();
            return result;
        }
    }
}