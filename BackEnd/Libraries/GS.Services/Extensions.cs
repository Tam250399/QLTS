using GS.Core;
using GS.Core.Infrastructure;
using GS.Services.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        /// Convert to select list
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="enumObj">Enum</param>
        /// <param name="markCurrentAsSelected">Mark current value as selected</param>
        /// <param name="valuesToExclude">Values to exclude</param>
        /// <param name="useLocalization">Localize</param>
        /// <returns>SelectList</returns>
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj,
           bool markCurrentAsSelected = true, int[] valuesToExclude = null, bool useLocalization = true) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("An Enumeration type is required.", nameof(enumObj));

            var _nhanHienThiService = EngineContext.Current.Resolve<INhanHienThiService>();
            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue))
                         select new { ID = Convert.ToInt32(enumValue), Name = useLocalization ? _nhanHienThiService.GetGiaTriEnum(enumValue) : CommonHelper.ConvertEnum(enumValue.ToString()) };
            object selectedValue = null;
            if (markCurrentAsSelected)
                selectedValue = Convert.ToInt32(enumObj);
            return new SelectList(values.OrderBy(p=>p.ID), "ID", "Name", selectedValue);
        }

        /// <summary>
        /// Chuyển selectlist thành List<SelectListItem>
        /// </summary>
        /// <param name="selectList"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToListSelectListItem(this SelectList selectList)
        {
            return selectList.Select(c => new SelectListItem { Value = c.Value, Selected = c.Selected, Text = c.Text }).ToList();
        }
        /// <summary>
        /// Convert to select list
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="objList">List of objects</param>
        /// <param name="selector">Selector for name</param>
        /// <returns>SelectList</returns>
        public static SelectList ToSelectList<T>(this T objList, Func<BaseEntity, string> selector) where T : IEnumerable<BaseEntity>
        {
            return new SelectList(objList.Select(p => new { ID = p.ID, Name = selector(p) }), "ID", "Name");
        }

        /// <summary>
        /// Conver từ dạng string  sang list int (ví dụ '1,2,3' => list<int>) -- HưngNT
        /// </summary>
        /// <param name="listString"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IList<int> ToListInt(this string listString)
        {
            var result = new List<int>() { };
            if (listString != null)
            {
                if (listString.Length > 0)
                {
                    var listDonVi = listString.Split(',').ToList();
                    foreach (var i in listDonVi)
                    {
                        if (!string.IsNullOrEmpty(i))
                        {
                            result.Add(Convert.ToInt32(i));
                        }
                    }
                }

            }
            return result;
        }
        /// <summary>
        /// Convert List<int> thành string (ví dụ list<int> {1,2,3} => "1,2,3")
        /// </summary>
        /// <param name="listInt"></param>
        /// <returns></returns>
        public static string ToListString(this IList<int> listInt)
        {
            string ListString = "";
            if (listInt.Count() > 0)
            {
                foreach (var item in listInt)
                {
                    ListString += item + ",";
                }
                ListString = ListString.Remove(ListString.Length - 1);


            }
            return ListString;

        }
    }
}
