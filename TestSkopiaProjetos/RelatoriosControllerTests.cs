using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SkopiaProjetos.Interfaces;
using Xunit;

namespace TestSkopiaProjetos
{
    public class RelatoriosControllerTests
    {
        private readonly Mock<IRelatorioService> _mockService;
        private readonly RelatoriosController _controller;

        public RelatoriosControllerTests()
        {
            _mockService = new Mock<IRelatorioService>();
            _controller = new RelatoriosController(_mockService.Object);
        }

        [Fact]
        public async Task RelatorioDesempenho_ReturnsOk_WhenResultIsNotNull()
        {
            // Arrange
            int usuarioId = 1;
            var resultado = new List<object> { new { Propriedade = "Valor" } };
            _mockService
                .Setup(s => s.RelatorioDesempenhoAsync(usuarioId))
                .ReturnsAsync(resultado);

            // Act
            var result = await _controller.RelatorioDesempenho(usuarioId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(resultado, okResult.Value);
        }

        [Fact]
        public async Task RelatorioDesempenho_ReturnsNotFound_WhenResultIsNull()
        {
            // Arrange
            int usuarioId = 2;
            _mockService
                .Setup(s => s.RelatorioDesempenhoAsync(usuarioId))
                .ReturnsAsync((IEnumerable<object>)null);

            // Act
            var result = await _controller.RelatorioDesempenho(usuarioId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}