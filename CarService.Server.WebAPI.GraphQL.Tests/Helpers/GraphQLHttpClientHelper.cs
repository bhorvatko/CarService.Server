using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotChocolate.ErrorCodes;

namespace CarService.Server.WebAPI.GraphQL.Tests.Helpers
{
    internal static class GraphQLHttpClientHelper
    {
        public static GraphQLHttpClient CreateGraphQLClient(HttpClient httpClient)
        {
            GraphQLHttpClient graphQLclient = new GraphQLHttpClient(new GraphQLHttpClientOptions()
            {
                EndPoint = new Uri($"{httpClient.BaseAddress!.OriginalString}:{httpClient.BaseAddress!.Port}/graphql/")

            }, new NewtonsoftJsonSerializer(), httpClient);

            graphQLclient.HttpClient.DefaultRequestHeaders.Add("SessionId", Guid.NewGuid().ToString());

            return graphQLclient;
        }

    }
}
