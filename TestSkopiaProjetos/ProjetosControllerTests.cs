using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SkopiaProjetos.Interfaces;
using SkopiaProjetos.Models;
using SkopiaProjetos.Models.Dtos;
using Xunit;

namespace TestSkopiaProjetos
{
    public class ProjetosControllerTests
    {
        private readonly Mock<IProjetoService> _mockService;
        private readonly ProjetosController _controller;

        public ProjetosControllerTests()
        {
            _mockService = new Mock<IProjetoService>();
            _controller = new ProjetosController(_mockService.Object);
        }

        [Fact]
        public async Task ListarProjetos_ReturnsOkWithProjects()
        {
            // Arrange
            int usuarioId = 1;
            var projetos = new List<Projeto>
        {
            new Projeto { Id = 1, Nome = "Projeto 1", UsuarioId = usuarioId },
            new Projeto { Id = 2, Nome = "Projeto 2", UsuarioId = usuarioId }
        };
            _mockService.Setup(s => s.ListarProjetosAsync(usuarioId)).ReturnsAsync(projetos);

            // Act
            var result = await _controller.ListarProjetos(usuarioId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(projetos, okResult.Value);
        }

        [Fact]
        public async Task CriarProjeto_ReturnsCreatedAtActionWithProject()
        {
            // Arrange
            var dto = new ProjetoCreateDto { Nome = "Novo Projeto", UsuarioId = 2 };
            var projetoCriado = new Projeto { Id = 10, Nome = dto.Nome, UsuarioId = dto.UsuarioId };
            _mockService.Setup(s => s.CriarProjetoAsync(It.IsAny<Projeto>())).ReturnsAsync(projetoCriado);

            // Act
            var result = await _controller.CriarProjeto(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.ListarProjetos), createdResult.ActionName);
            Assert.Equal(projetoCriado, createdResult.Value);
            Assert.Equal(dto.UsuarioId, ((dynamic)createdResult.RouteValues)["usuarioId"]);
        }

        [Fact]
        public async Task RemoverProjeto_WhenRemovido_ReturnsNoContent()
        {
            // Arrange
            int projetoId = 5;
            _mockService.Setup(s => s.RemoverProjetoAsync(projetoId)).ReturnsAsync(true);

            // Act
            var result = await _controller.RemoverProjeto(projetoId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task RemoverProjeto_WhenNotRemovido_ReturnsBadRequest()
        {
            // Arrange
            int projetoId = 6;
            _mockService.Setup(s => s.RemoverProjetoAsync(projetoId)).ReturnsAsync(false);

            // Act
            var result = await _controller.RemoverProjeto(projetoId);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Não é possível remover o projeto enquanto houver tarefas pendentes ou projeto não encontrado.", badRequest.Value);
        }
    }
}