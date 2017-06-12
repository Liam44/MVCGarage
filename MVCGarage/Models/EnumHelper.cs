using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace MVCGarage.Models
{
    public static class EnumHelper
    {
        public static string GetDescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static IEnumerable<SelectListItem> PopulateDropList()
        {
            return Enum.GetValues(typeof(ETypeVehicle))
                        .Cast<ETypeVehicle>()
                        .Where(e => e != ETypeVehicle.undefined)
                        .Select(e => new SelectListItem
                        {
                            Value = ((int)e).ToString(),
                            Text = GetDescriptionAttr(e)
                        });
        }
    }
}