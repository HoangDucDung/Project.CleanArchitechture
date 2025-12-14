using Dapper;
using System.Reflection;

namespace Project.Extensions.Extensions
{
    public static class DapperExtensions
    {
        public static DynamicParameters ToDynamicParameters<T>(this T entity)
        {
            var parameters = new DynamicParameters();
            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                parameters.Add("@" + prop.Name, prop.GetValue(entity));
            }
            return parameters;
        }
    }
}
