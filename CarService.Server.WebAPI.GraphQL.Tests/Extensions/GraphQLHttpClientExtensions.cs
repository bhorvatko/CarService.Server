using GraphQL;
using GraphQL.Client.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.WebAPI.GraphQL.Tests.Extensions
{
    internal static class GraphQLHttpClientExtensions
    {
        public static async Task SendMutation(this GraphQLHttpClient client, string mutationName, Dictionary<string, string>? parameters = null)
        {
            GraphQLResponse<JObject> response = 
                await client.SendMutationAsync<JObject>(new GraphQLRequest(GetMutation(mutationName, parameters ?? new Dictionary<string, string>())));

            if (response.Errors?.Any() == true)
            {
                throw new Exception($"GraphQL error when executing mutation {mutationName} {GetFlattenedErrorMessage(response.Errors)}");
            }
        }

        public static async Task<string> SendMutationWithIdFeedback(this GraphQLHttpClient client, string mutationName, Dictionary<string, string>? parameters = null)
        {
            GraphQLResponse<JObject> response =
                await client.SendMutationAsync<JObject>(new GraphQLRequest(GetMutation(mutationName, parameters ?? new Dictionary<string, string>())));

            if (response.Errors?.Any() == true)
            {
                throw new Exception($"GraphQL error when executing mutation {mutationName} {GetFlattenedErrorMessage(response.Errors)}");
            }

            string? id = response.Data
                .Descendants()
                .Where(j => j is JProperty)
                .FirstOrDefault(j => ((JProperty)j).Name == "id")?
                .Values()
                .FirstOrDefault()?
                .ToString();

            if (id == null)
            {
                throw new Exception($"ID field was not present in response to the {mutationName} mutation");
            }

            return id;
        }

        public static async Task<string> SendQuery(this GraphQLHttpClient client, string queryName, Dictionary<string, string>? parameters = null)
        {
            GraphQLResponse<dynamic> response = 
                await client.SendQueryAsync<dynamic>(new GraphQLRequest(GetQuery(queryName, parameters ?? new Dictionary<string, string>())));

            if (response.Errors?.Any() == true)
            {
                throw new Exception($"GraphQL error when executing query {queryName} {GetFlattenedErrorMessage(response.Errors)}");
            }

            return JObject.Parse(JsonConvert.SerializeObject(response.Data)).ToString();
        }

        private static string GetMutation(string name, Dictionary<string, string> parameters)
            => File.ReadAllText(Path.Combine("Mutations", name + ".gql")).ApplyParametersToQuery(parameters);

        public static string GetQuery(string name, Dictionary<string, string> parameters)
            => File.ReadAllText(Path.Combine("Queries", name + ".gql")).ApplyParametersToQuery(parameters);

        private static string GetFlattenedErrorMessage(IEnumerable<GraphQLError> errors)
        {
            string message = string.Empty;

            foreach (var error in errors)
            {
                string path = string.Empty;

                if (error.Path != null)
                {
                    foreach (string pathSegment in error.Path)
                    {
                        path += pathSegment + "/";
                    }
                }

                message += path + ": " + error.Message + Environment.NewLine;
            }

            return message;
        }
    }
}
