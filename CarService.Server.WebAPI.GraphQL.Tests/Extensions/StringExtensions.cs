using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.WebAPI.GraphQL.Tests.Extensions
{
    internal static class StringExtensions
    {
        public static string NormalizeQuery(this string query)
            => query.Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("\r", "");

        public static string ApplyParametersToQuery(this string query, Dictionary<string, string> parameters)
        {
            string result = query;

            foreach(KeyValuePair<string, string> param in parameters)
            {
                string toReplace = "{{" + param.Key + "}}";

                if (!result.Contains(toReplace)) throw new InvalidOperationException($"Query does not contain the tag {toReplace}");

                result = result.Replace(toReplace, param.Value);
            }

            return result;
        }
    }
}
