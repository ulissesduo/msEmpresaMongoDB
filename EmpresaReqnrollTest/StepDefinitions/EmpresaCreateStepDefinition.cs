using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSchema = NJsonSchema.JsonSchema;

namespace EmpresaReqnrollTest.StepDefinitions
{
    [Binding]
    public class EmpresaCreateStepDefinition
    {
        private HttpResponseMessage _response;

        [Given(@"a API da empresa está rodando no docker")]
        public void GivenAApiDaEmpresaEstaRodandoNoDocker()
        {
            // Just ensure the API is up (you can check manually before running)
        }

        [When(@"enviar uma requisição POST para ""(.*)"" com o corpo:")]
        public async Task WhenEnviarUmaRequisicaoPOSTParaComOCorpoAsync(string endpoint, string body)
        {
            var client = new HttpClient();
            var baseUrl = "http://localhost:8081"; // change if your API uses another port

            var content = new StringContent(body, Encoding.UTF8, "application/json");
            _response = await client.PostAsync(baseUrl + endpoint, content);
        }

        [Then(@"a resposta deve conter status (.*)")]
        public void ThenARespostaDeveConterStatus(int statusCode)
        {
            ((int)_response.StatusCode).Should().Be(statusCode);
        }

        [Then(@"o corpo da resposta deve seguir o schema ""(.*)""")]
        public async Task ThenOCorpoDaRespostaDeveSeguirOSchema(string schemaPath)
        {
            var jsonResponse = await _response.Content.ReadAsStringAsync();

            var schema = await JsonSchema.FromFileAsync(schemaPath);
            var errors = schema.Validate(jsonResponse);

            errors.Should().BeEmpty($"Response does not match schema: {string.Join(", ", errors.Select(e => e.ToString()))}");
        }
    }
}
