using CarService.Server.Persistence.MsSql;
using CarService.Server.WebAPI.GraphQL.Tests.Extensions;
using CarService.Server.WebAPI.GraphQL.Tests.Fixtures;
using CarService.Server.WebAPI.GraphQL.Tests.Helpers;
using CarService.Server.WebAPI.GraphQL.Tests.Responses;
using FluentAssertions;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Diagnostics;

namespace CarService.Server.WebAPI.GraphQL.Tests
{
    public class GraphQLTests : IntegrationTests, IClassFixture<GraphQlClientFixture>
    {
        private readonly GraphQLHttpClient graphQLClient;

        public GraphQLTests(GraphQlClientFixture graphQlClientFixture)
        {
            this.graphQLClient = graphQlClientFixture.GraphQLClient;
        }

        [Fact]
        public async Task Creating_and_getting_a_procedure()
        {
            await graphQLClient.SendMutation("AddProcedure");

            string response = await graphQLClient.SendQuery("AllProcedures");

            response.Should().Be(ResponseProvider.GetExpectedResponse("AllProcedures"));
        }

        [Fact]
        public async Task Creating_and_assiging_a_warrant_to_a_technician()
        {
            string firstProcedureId = await graphQLClient.SendMutationWithIdFeedback("AddProcedureWithIdFeedback", new Dictionary<string, string> { { "name", "First" } });
            string secondProcedureId = await graphQLClient.SendMutationWithIdFeedback("AddProcedureWithIdFeedback", new Dictionary<string, string> { { "name", "Second" } });
            string warrantTypeId = await graphQLClient.SendMutationWithIdFeedback("AddWarrantTypeWithIdFeedback", new Dictionary<string, string>()
            {
                { "firstProcedureId", firstProcedureId },
                { "secondProcedureId", secondProcedureId }
            });
            string warrantId = await graphQLClient.SendMutationWithIdFeedback("AddWarrantWithIdFeedback", new Dictionary<string, string>()
            {
                { "warrantTypeId", warrantTypeId }
            });
            string technicianId = await graphQLClient.SendMutationWithIdFeedback("AddTechnicianWithIdFeedback");
            
            await graphQLClient.SendMutation("AssignToTechnician", new Dictionary<string, string> 
            {
                { "technicianId", technicianId },
                { "warrantId", warrantId }
            });

            string response = await graphQLClient.SendQuery("AllTechnicians");
            response.Should().Be(ResponseProvider.GetExpectedResponse("AllTechnicians"));
        }

        [Fact]
        public async Task Advancing_and_rolling_back_a_warrant()
        {
            string firstProcedureId = await graphQLClient.SendMutationWithIdFeedback("AddProcedureWithIdFeedback", new Dictionary<string, string> { { "name", "First" } });
            string secondProcedureId = await graphQLClient.SendMutationWithIdFeedback("AddProcedureWithIdFeedback", new Dictionary<string, string> { { "name", "Second" } });
            string thidProcedureId = await graphQLClient.SendMutationWithIdFeedback("AddProcedureWithIdFeedback", new Dictionary<string, string> { { "name", "Third" } });
            string warrantTypeId = await graphQLClient.SendMutationWithIdFeedback("AddWarrantTypeWithThreeSteps", new Dictionary<string, string>()
            {
                { "firstProcedureId", firstProcedureId },
                { "secondProcedureId", secondProcedureId },
                { "thirdProcedureId", thidProcedureId }
            });
            string warrantId = await graphQLClient.SendMutationWithIdFeedback("AddWarrantWithIdFeedback", new Dictionary<string, string>()
            {
                { "warrantTypeId", warrantTypeId }
            });

            await graphQLClient.SendMutation("AdvanceWarrantToNextStep", new Dictionary<string, string>() { { "warrantId", warrantId } });
            await graphQLClient.SendMutation("AdvanceWarrantToNextStep", new Dictionary<string, string>() { { "warrantId", warrantId } });
            await graphQLClient.SendMutation("RollbackWarrantToPreviousStep", new Dictionary<string, string>() { { "warrantId", warrantId } });

            string response = await graphQLClient.SendQuery("AdvancingAndRollingBackAWarrant", new Dictionary<string, string>() { { "warrantId", warrantId } });
            response.Should().Be(ResponseProvider.GetExpectedResponse("AdvancingAndRollingBackAWarrant"));
        }
    }
}