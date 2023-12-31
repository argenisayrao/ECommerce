﻿using System;

namespace Ecommerce.Core.Domain
{
    public static class CheckExtension
    {
        public static bool HasValue(this object value)
        {
            bool result;
            if (value is DateTime dateValue)
                result = dateValue != DateTime.MinValue;
            else if (value is Guid guidValue)
                result = guidValue != Guid.Empty;
            else if (value is Enum enumValue)
                result = enumValue.ToInt() != 0;
            else if (value is string stringValue)
                result = !string.IsNullOrWhiteSpace(stringValue);
            else
                result = value.IsNotNull();

            return result;
        }
        public static bool NoValue(this object value) => !value.HasValue();

        private static bool IsNull(this object value) => value == null || value == DBNull.Value;
        private static bool IsNotNull(this object value) => !value.IsNull();

        public static bool IsNumber(this object value)
        {
            try
            {
                Convert.ToDecimal(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}