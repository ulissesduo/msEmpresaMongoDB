Feature: Deletar empresa por ID
  Testa o comportamento do endpoint DELETE /api/empresa/{id} quando a empresa existe

  Scenario: Deletar empresa pelo ID
    Given a API da Empresa rodando no docker para delete
    When executar requisição DELETE para "/api/empresa/68fd5b1af936cb5e37dfcdcb"
    Then a resposta deve ser 204 No Content
