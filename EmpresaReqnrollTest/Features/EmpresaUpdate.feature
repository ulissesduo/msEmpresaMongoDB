Feature: Editar empresa
  Testa o endpoint PUT /api/empresa/{id} para edição de uma empresa

  Scenario: Editar uma empresa com sucesso
    Given a API da empresa está rodando no docker para edição
    And uma empresa foi criada com os dados:
      """
      {
        "nome": "Empresa Teste PUT",
        "cnpj": "99887766000122",
        "setorAtividade": "Tecnologia"
      }
      """
    When enviar uma requisição PUT para "/api/empresa/{idDaEmpresaCriada}" com o corpo:
      """
      {
        "nome": "Empresa Atualizada",
        "cnpj": "99887766000122",
        "setorAtividade": "Finanças"
      }
      """
    Then a resposta será status de sucesso 200 

