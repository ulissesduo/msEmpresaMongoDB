using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSchema = NJsonSchema.JsonSchema;

namespace EmpresaReqnrollTest.StepDefinitions
{
    [Binding]
    public class EmpresaUpdateStepDefinition
    {
        private readonly HttpClient _client = new();
        private HttpResponseMessage _response;
        private readonly string _baseUrl = "http://localhost:8081";
        private string _createdEmpresaId = string.Empty;

        [Given(@"a API da empresa está rodando no docker para edição")]
        public void GivenAApiDaEmpresaEstaRodandoNoDockerParaEdicao()
        {
            //_baseUrl = "http://localhost:8080/api/empresa";
            //_client = new HttpClient();
        }

        [Given(@"uma empresa foi criada com os dados:")]
        public async Task GivenUmaEmpresaFoiCriada(string jsonBody)
        {
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_baseUrl}/api/empresa", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            _createdEmpresaId = json.GetProperty("id").GetString()!;

            _createdEmpresaId.Should().NotBeNullOrEmpty("a empresa criada deve ter um ID válido");
        }

        [When(@"enviar uma requisição PUT para ""(.*)"" com o corpo:")]
        public async Task WhenEnviarUmaRequisicaoPUTParaComOCorpo(string endpoint, string body)
        {
            endpoint = endpoint.Replace("{idDaEmpresaCriada}", _createdEmpresaId);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            _response = await _client.PutAsync(_baseUrl + endpoint, content);
        }

        [Then(@"a resposta será status de sucesso (\d+)")]
        public void ThenARespostaSeraStatusDeSucesso(int expectedStatus)
        {
            ((int)_response.StatusCode).Should().Be(expectedStatus);
        }


    }
}
