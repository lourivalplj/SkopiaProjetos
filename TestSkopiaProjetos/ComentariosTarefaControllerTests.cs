using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SkopiaProjetos.Interfaces;
using SkopiaProjetos.Models;
using SkopiaProjetos.Models.Dtos;
using Xunit;

namespace TestSkopiaProjetos
{
    public class ComentariosTarefaControllerTests
    {
        private readonly Mock<IComentarioTarefaService> _mockService;
        private readonly ComentariosTarefaController _controller;

        public ComentariosTarefaControllerTests()
        {
            _mockService = new Mock<IComentarioTarefaService>();
            _controller = new ComentariosTarefaController(_mockService.Object);
        }

        [Fact]
        public async Task AdicionarComentario_ReturnsOk_WhenComentarioIsAdded()
        {
            // Arrange
            var dto = new ComentarioTarefaCreateDto
            {
                TarefaId = 1,
                Conteudo = "Novo comentário"
            };
            int usuarioId = 2;
            var comentarioCriado = new ComentarioTarefa
            {
                Id = 10,
                TarefaId = dto.TarefaId,
                Conteudo = dto.Conteudo,
                UsuarioId = usuarioId
            };
            _mockService
                .Setup(s => s.AdicionarComentarioAsync(dto.TarefaId, It.IsAny<ComentarioTarefa>()))
                .ReturnsAsync(comentarioCriado);

            // Act
            var result = await _controller.AdicionarComentario(dto, usuarioId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(comentarioCriado, okResult.Value);
        }

        [Fact]
        public async Task AdicionarComentario_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            var dto = new ComentarioTarefaCreateDto
            {
                TarefaId = 1,
                Conteudo = "Comentário"
            };
            int usuarioId = 2;
            _mockService
                .Setup(s => s.AdicionarComentarioAsync(dto.TarefaId, It.IsAny<ComentarioTarefa>()))
                .ReturnsAsync((ComentarioTarefa)null);

            // Act
            var result = await _controller.AdicionarComentario(dto, usuarioId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}