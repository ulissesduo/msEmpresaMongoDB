using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using msEmpresaMongoDB.Controllers;
using msEmpresaMongoDB.DTO;
using msEmpresaMongoDB.Entity;
using msEmpresaMongoDB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectEmpresa
{
    internal class EmpresaControllerTest
    {
        private readonly Mock<IEmpresaService> _empresaServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly EmpresaController _controller;
        public EmpresaControllerTest()
        {
            _empresaServiceMock = new Mock<IEmpresaService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new EmpresaController(_empresaServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithList()
        {
            // Arrange
            var empresas = new List<Empresa> { new Empresa { Id = "1", Nome = "Empresa A" } };
            var empresasDto = new List<EmpresaResponseDTO> { new EmpresaResponseDTO { Id = "1", Nome = "Empresa A" } };

            _empresaServiceMock.Setup(s => s.GetAllEmpresa()).ReturnsAsync(empresas);
            _mapperMock.Setup(m => m.Map<List<EmpresaResponseDTO>>(empresas)).Returns(empresasDto);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<EmpresaResponseDTO>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Empresa A", returnValue[0].Nome);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenEmpresaDoesNotExist()
        {
            // Arrange
            _empresaServiceMock.Setup(s => s.GetEmpresaById("1")).ReturnsAsync((Empresa)null);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsEmpresa_WhenFound()
        {
            // Arrange
            var empresa = new Empresa { Id = "1", Nome = "Empresa A" };
            var dto = new EmpresaResponseDTO { Id = "1", Nome = "Empresa A" };

            _empresaServiceMock.Setup(s => s.GetEmpresaById("1")).ReturnsAsync(empresa);
            _mapperMock.Setup(m => m.Map<EmpresaResponseDTO>(empresa)).Returns(dto);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            var okResult = Assert.IsType<ActionResult<EmpresaResponseDTO>>(result);
            Assert.Equal("Empresa A", okResult.Value.Nome);
        }

        [Fact]
        public async Task Create_ReturnsOkWithCreatedEmpresa()
        {
            // Arrange
            var dto = new EmpresaRequestDTO { Nome = "Nova Empresa" };
            var entity = new Empresa { Id = "1", Nome = "Nova Empresa" };
            var response = new EmpresaResponseDTO { Id = "1", Nome = "Nova Empresa" };

            _mapperMock.Setup(m => m.Map<Empresa>(dto)).Returns(entity);
            _empresaServiceMock.Setup(s => s.CreateEmpresa(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<EmpresaResponseDTO>(entity)).Returns(response);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EmpresaResponseDTO>(okResult.Value);
            Assert.Equal("Nova Empresa", returnValue.Nome);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenEmpresaDoesNotExist()
        {
            // Arrange
            _empresaServiceMock.Setup(s => s.GetEmpresaById("1")).ReturnsAsync((Empresa)null);

            // Act
            var result = await _controller.Delete("1");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            var empresa = new Empresa { Id = "1", Nome = "Empresa A" };
            var dto = new EmpresaResponseDTO { Id = "1", Nome = "Empresa A" };

            _empresaServiceMock.Setup(s => s.GetEmpresaById("1")).ReturnsAsync(empresa);
            _mapperMock.Setup(m => m.Map<EmpresaResponseDTO>(empresa)).Returns(dto);
            _empresaServiceMock.Setup(s => s.DeleteEmpresa("1")).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete("1");

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task Update_ReturnsOkWithUpdatedEmpresa()
        {
            // Arrange
            var dto = new EmpresaRequestDTO { Nome = "Atualizada" };
            var empresa = new Empresa { Id = "1", Nome = "Empresa Antiga" };
            var updated = new Empresa { Id = "1", Nome = "Atualizada" };
            var response = new EmpresaResponseDTO { Id = "1", Nome = "Atualizada" };

            _empresaServiceMock.Setup(s => s.GetEmpresaById("1")).ReturnsAsync(empresa);
            _mapperMock.Setup(m => m.Map(dto, empresa)).Returns(updated);
            _empresaServiceMock.Setup(s => s.UpdateEmpresa("1", empresa)).ReturnsAsync(updated);
            _mapperMock.Setup(m => m.Map<EmpresaResponseDTO>(updated)).Returns(response);

            // Act
            var result = await _controller.Update("1", dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EmpresaResponseDTO>(okResult.Value);
            Assert.Equal("Atualizada", returnValue.Nome);
        }




    }
}
