using FluentAssertions;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Reqnroll;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NJsonSchema;
using JsonSchema = NJsonSchema.JsonSchema;
namespace EmpresaReqnrollTest.StepDefinitions
{
    [Binding]
    public class EmpresaDeleteStepDefinition
    {
        private HttpResponseMessage _response;

        [Given(@"a API da Empresa rodando no docker para delete")]
        public void GivenApiDaEmpresaRodandoNoDocker()
        {
            // Nothing to do, just ensures API is running
        }

        [When(@"executar requisição DELETE para ""(.*)""")]
        public async Task WhenExecutarRequisicaoDELETEPara(string endpoint)
        {
            var client = new HttpClient();
            var baseUrl = "http://localhost:8081"; // adjust if your API runs elsewhere
            _response = await client.DeleteAsync(baseUrl + endpoint);
        }

        [Then(@"a resposta deve ser 204 No Content")]
        public async Task ThenARespostaDeveSer204NoContent()
        {
            // 1️⃣ Validate HTTP status
            ((int)_response.StatusCode).Should().Be(204, "DELETE should return 204 when successful");

            // 2️⃣ Validate (if any) JSON response body
            var content = await _response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();

            // 3️⃣ Validate JSON Schema (optional if your DELETE returns body)
            // Example schema file: "Schemas/EmpresaDeleteResponse.json"
            if (!string.IsNullOrEmpty(content))
            {
                var schema = await JsonSchema.FromFileAsync("Schemas/EmpresaDeleteResponse.json");
                var errors = schema.Validate(content);
                errors.Should().BeEmpty("Response should match the expected JSON schema");
            }
        }
    }
}
