using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebBarangBukti.Help
{

    public static class ListExtention
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> list, Func<T, string> dataField,
            Func<T, string> valueField, string defaultValue)
        {
            var result = new List<SelectListItem>();
            if (list.Any())
                result.AddRange(
                    list.Select(
                        resultItem => new SelectListItem
                        {
                            Value = valueField(resultItem),
                            Text = dataField(resultItem),
                            Selected = defaultValue == valueField(resultItem)
                        }));
            return result;
        }
    }
}
