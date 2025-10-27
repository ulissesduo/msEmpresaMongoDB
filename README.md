# 🏢 msEmpresaMongoDB

A simple **ASP.NET Core Web API** that manages companies (**Empresas**) using **MongoDB** as the database.  
This project demonstrates clean architecture with **Repository → Service → Controller** layers, DTO mapping with **AutoMapper**, and async operations with the official **MongoDB.Driver**.  

---

## ✨ Features
- 📦 **MongoDB integration** with async CRUD operations  
- 🔄 **Repository + Service pattern** for clean separation of concerns  
- 🛠️ **AutoMapper** for DTO ↔ Entity mapping  
- 📑 **Swagger UI** for API documentation and testing  
- ✅ Input validation and error handling  


## 🛠️ Stack Tecnológica
**Categoria**                  **Tecnologia**          

**Linguagem / Framework**      .NET 8 (ASP.NET WebAPI) 
**Banco de Dados**             MongoDB                
**Testes Automatizados**       xUnit / Reqnroll        
**Mapeamento DTO ↔ Entidade**  AutoMapper              
**Documentação**               Swagger                 
**Containerização**            Docker / Docker Compose 
**CI/CD**                      GitHub Actions          
**Deploy**                     Azure App Service       



## msEmpresaMongoDB/
├── Controllers/           # Endpoints da API (ex: EmpresaController.cs)
├── DTO/                   # Objetos de Transferência de Dados
├── Entity/                # Entidades de domínio (ex: Empresa.cs)
├── Repository/            # Lógica de persistência (MongoDB)
├── Service/               # Camada de regras de negócio
├── msEmpresaMongoDBTest/  # Testes automatizados (xUnit e Reqnroll)
├── Program.cs             # Ponto de entrada da aplicação
├── appsettings.json       # Configurações (MongoDB, Logging, etc)
└── Dockerfile



## 🧪 Testes Automatizados
## 🧩 Testes Unitários (xUnit)

Validam a lógica dos serviços e repositórios de forma isolada.
São responsáveis por garantir que:

O serviço EmpresaService trata corretamente as operações CRUD;

O repositório do MongoDB é chamado com os parâmetros esperados.

Execução:
dotnet test



## 🧩 Testes de Integração BDD com Reqnroll

Os testes Reqnroll validam o comportamento da API de forma integrada, garantindo que os endpoints REST se comportam conforme esperado.

Foram criados cenários Reqnroll para:

✅ Criar empresa (POST /api/empresa)

📝 Editar empresa (PUT /api/empresa/{id})

❌ Deletar empresa (DELETE /api/empresa/{id})

Esses testes não rodam na pipeline do Azure, sendo executados somente localmente via Docker, simulando as interações reais entre API e banco.


## 📘 Exemplo de Cenário Reqnroll
## Criar Empresa
Feature: Criar empresa
  Testa o endpoint POST /api/empresa para criação de uma nova empresa

  Scenario: Criar uma nova empresa com sucesso
    Given a API da empresa está rodando no docker
    When enviar uma requisição POST para "/api/empresa" com o corpo:
      """
      {
        "nome": "Empresa Teste",
        "cnpj": "12345678900123",
        "setorAtividade": "Serviços"
      }
      """
    Then a resposta deve conter status 200

## Editar Empresa
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

## Excluir Empresa
Feature: Deletar empresa por ID
  Testa o comportamento do endpoint DELETE /api/empresa/{id} quando a empresa existe

  Scenario: Deletar empresa pelo ID
    Given a API da Empresa rodando no docker para delete
    When executar requisição DELETE para "/api/empresa/68fd5b1af936cb5e37dfcdcb"
    Then a resposta deve ser 204 No Content



## ▶️ Executando os testes Reqnroll localmente

1️⃣ Subir os containers (API + MongoDB):

docker-compose up --build


2️⃣ Executar os testes:

dotnet test msEmpresaMongoDBTest
