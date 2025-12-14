using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Extensions.Extensions
{
    public static class AttributeExtensions
    {
        /// <summary>
        /// Lấy tên bảng từ attribute Table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetPropertyTableName<T>(this T obj)
        {
            var type = typeof(T);
            var tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), false)
                                     .FirstOrDefault() as TableAttribute;

            return tableAttribute != null ? tableAttribute.Name : string.Empty;
        }
    }
}
