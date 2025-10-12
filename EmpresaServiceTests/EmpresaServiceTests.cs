using Moq;
using msEmpresaMongoDB.Entity;
using msEmpresaMongoDB.Repository;
using msEmpresaMongoDB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaServiceTests
{
    public class EmpresaServiceTests
    {
        private readonly Mock<IEmpresaRepository> _mockRepo;
        private readonly EmpresaService _service;

        public EmpresaServiceTests()
        {
            _mockRepo = new Mock<IEmpresaRepository>();
            _service = new EmpresaService(_mockRepo.Object);
        }

        private Empresa CreateSampleEmpresa() => new Empresa
        {
            Id = "652f6f4f9b1e8a3c7a1a23c4",
            Nome = "Tech Solutions",
            CNPJ = "12.345.678/0001-99",
            SetorAtividade = "Tecnologia"
        };

        // -----------------------------
        // CREATE
        // -----------------------------
        [Fact]
        public async Task CreateEmpresa_ShouldReturnCreatedEmpresa()
        {
            // Arrange
            var empresa = CreateSampleEmpresa();
            _mockRepo.Setup(r => r.criarEmpresa(It.IsAny<Empresa>())).ReturnsAsync(empresa);

            // Act
            var result = await _service.CreateEmpresa(empresa);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tech Solutions", result.Nome);
            _mockRepo.Verify(r => r.criarEmpresa(It.IsAny<Empresa>()), Times.Once);
        }

        [Fact]
        public async Task CreateEmpresa_ShouldThrowException_WhenEmpresaIsNull()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateEmpresa(null));
            Assert.Contains("Empresa não pode ser nula", exception.Message);
        }

        [Fact]
        public async Task CreateEmpresa_ShouldThrowException_WhenNomeIsEmpty()
        {
            // Arrange
            var empresa = new Empresa
            {
                Id = "abc123",
                Nome = "", // empty name
                CNPJ = "12.345.678/0001-99",
                SetorAtividade = "Comércio"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateEmpresa(empresa));
            Assert.Contains("Nome da empresa é obrigatório", exception.Message);
        }

        // -----------------------------
        // DELETE
        // -----------------------------
        [Fact]
        public async Task DeleteEmpresa_ShouldReturnTrue_WhenEmpresaExists()
        {
            // Arrange
            var empresa = CreateSampleEmpresa();
            _mockRepo.Setup(r => r.buscaEmpresa("123")).ReturnsAsync(empresa);
            _mockRepo.Setup(r => r.deleteEmpresa("123")).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteEmpresa("123");

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.deleteEmpresa("123"), Times.Once);
        }

        [Fact]
        public async Task DeleteEmpresa_ShouldReturnFalse_WhenEmpresaDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(r => r.buscaEmpresa("notfound")).ReturnsAsync((Empresa)null);

            // Act
            var result = await _service.DeleteEmpresa("notfound");

            // Assert
            Assert.False(result);
            _mockRepo.Verify(r => r.deleteEmpresa(It.IsAny<string>()), Times.Never);
        }

        // -----------------------------
        // GET ALL
        // -----------------------------
        [Fact]
        public async Task GetAllEmpresa_ShouldReturnListOfEmpresas()
        {
            // Arrange
            var empresas = new List<Empresa> { CreateSampleEmpresa() };
            _mockRepo.Setup(r => r.listaEmpresas()).ReturnsAsync(empresas);

            // Act
            var result = await _service.GetAllEmpresa();

            // Assert
            Assert.Single(result);
            Assert.Equal("Tech Solutions", result[0].Nome);
            _mockRepo.Verify(r => r.listaEmpresas(), Times.Once);
        }

        // -----------------------------
        // GET BY ID
        // -----------------------------
        [Fact]
        public async Task GetEmpresaById_ShouldReturnEmpresa()
        {
            // Arrange
            var empresa = CreateSampleEmpresa();
            _mockRepo.Setup(r => r.buscaEmpresa("abc123")).ReturnsAsync(empresa);

            // Act
            var result = await _service.GetEmpresaById("abc123");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tech Solutions", result.Nome);
            _mockRepo.Verify(r => r.buscaEmpresa("abc123"), Times.Once);
        }

        // -----------------------------
        // UPDATE
        // -----------------------------
        [Fact]
        public async Task UpdateEmpresa_ShouldReturnUpdatedEmpresa()
        {
            // Arrange
            var empresa = CreateSampleEmpresa();
            _mockRepo.Setup(r => r.buscaEmpresa("abc123")).ReturnsAsync(empresa);
            _mockRepo.Setup(r => r.atualizarEmpresa("abc123", empresa)).ReturnsAsync(empresa);

            // Act
            var result = await _service.UpdateEmpresa("abc123", empresa);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tech Solutions", result.Nome);
            _mockRepo.Verify(r => r.atualizarEmpresa("abc123", empresa), Times.Once);
        }

        [Fact]
        public async Task UpdateEmpresa_ShouldReturnNull_WhenEmpresaNotFound()
        {
            // Arrange
            var empresa = CreateSampleEmpresa();
            _mockRepo.Setup(r => r.buscaEmpresa("abc123")).ReturnsAsync((Empresa)null);

            // Act
            var result = await _service.UpdateEmpresa("abc123", empresa);

            // Assert
            Assert.Null(result);
            _mockRepo.Verify(r => r.atualizarEmpresa(It.IsAny<string>(), It.IsAny<Empresa>()), Times.Never);
        }

        [Fact]
        public async Task UpdateEmpresa_ShouldThrowArgumentException_WhenEmpresaIsNull()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateEmpresa("abc123", null));
            Assert.Contains("empresa", exception.Message);
        }
    }
}
