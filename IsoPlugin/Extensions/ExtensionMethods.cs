﻿using System.Collections.Generic;
using System.Globalization;
using AgGateway.ADAPT.ApplicationDataModel.Common;

namespace AgGateway.ADAPT.IsoPlugin
{
    internal static class ExtensionMethods
    {
        internal static TValue FindById<TKey, TValue>(this Dictionary<TKey, TValue> items, TKey id) where TValue : class
        {
            if (items == null || items.Count == 0 || id == null)
                return null;

            TValue value;
            if (items.TryGetValue(id, out value))
                return value;

            return null;
        }

        internal static bool ParseValue(this string value, out long result)
        {
            result = default(long);

            if (string.IsNullOrEmpty(value))
                return false;

            return long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
        }

        internal static bool ParseValue(this string value, out int result)
        {
            result = default(int);

            if (string.IsNullOrEmpty(value))
                return false;

            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
        }

        internal static bool ParseValue(this string value, out double result)
        {
            result = default(double);

            if (string.IsNullOrEmpty(value))
                return false;

            return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
        }

        internal static double ConvertFromIsoUnit(this IsoUnit unit, double value)
        {
            if (unit == null)
                return value;

            return (value + unit.Offset) * unit.Scale;
        }

        internal static double ConvertToIsoUnit(this IsoUnit unit, double value)
        {
            if (unit == null)
                return value;

            return value / unit.Scale - unit.Offset;
        }

        internal static UnitOfMeasure ToAdaptUnit(this IsoUnit isoUnit)
        {
            if (isoUnit == null)
                return null;

            var adaptUnuit = Representation.UnitSystem.UnitSystemManager.GetUnitOfMeasure(isoUnit.Code);

            return adaptUnuit;
        }

        internal static UnitOfMeasure ToAdaptUnit(this ValuePresentation unit)
        {
            if (unit == null)
                return null;

            return null;
        }
    }
}