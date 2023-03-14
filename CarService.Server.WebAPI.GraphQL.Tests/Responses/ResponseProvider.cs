using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.WebAPI.GraphQL.Tests.Responses
{
    internal static class ResponseProvider
    {
        public static string GetExpectedResponse(string name)
            => JObject.Parse(File.ReadAllText(Path.Combine("Responses", name + ".json"))).ToString();
    }
}
