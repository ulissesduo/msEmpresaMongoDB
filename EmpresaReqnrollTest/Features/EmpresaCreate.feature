Feature: Criar empresa
  Testa o endpoint POST /api/empresa para criação de uma nova empresa

  Scenario: Criar uma nova empresa com sucesso
    Given a API da empresa está rodando no docker
    When enviar uma requisição POST para "/api/empresa" com o corpo:
      """
      {
        "nome": "testse",
        "cnpj": "123456",
        "setorAtividade": "testes"
      }
      """
    Then a resposta deve conter status 200
   