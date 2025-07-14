using System;
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
    public class TarefasControllerTests
    {
        private readonly Mock<ITarefaService> _mockService;
        private readonly TarefasController _controller;

        public TarefasControllerTests()
        {
            _mockService = new Mock<ITarefaService>();
            _controller = new TarefasController(_mockService.Object);
        }

        [Fact]
        public async Task ListarTarefas_ReturnsOkWithTarefas()
        {
            // Arrange
            int projetoId = 1;
            var tarefas = new List<Tarefa>
        {
            new Tarefa { Id = 1, ProjetoId = projetoId, Titulo = "Tarefa 1", Descricao = "Desc", DataVencimento = DateTime.Now.AddDays(1), Status = StatusTarefa.Pendente, Prioridade = PrioridadeTarefa.Baixa },
            new Tarefa { Id = 2, ProjetoId = projetoId, Titulo = "Tarefa 2", Descricao = "Desc", DataVencimento = DateTime.Now.AddDays(2), Status = StatusTarefa.EmAndamento, Prioridade = PrioridadeTarefa.Media }
        };
            _mockService.Setup(s => s.ListarTarefasAsync(projetoId)).ReturnsAsync(tarefas);

            // Act
            var result = await _controller.ListarTarefas(projetoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(tarefas, okResult.Value);
        }

        [Fact]
        public async Task CriarTarefa_ReturnsCreatedAtActionWithTarefa()
        {
            // Arrange
            var dto = new TarefaCreateDto
            {
                ProjetoId = 1,
                Titulo = "Nova Tarefa",
                Descricao = "Descricao",
                DataVencimento = DateTime.Now.AddDays(5),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Alta
            };
            var tarefaCriada = new Tarefa
            {
                Id = 10,
                ProjetoId = dto.ProjetoId,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataVencimento = dto.DataVencimento,
                Status = dto.Status,
                Prioridade = dto.Prioridade
            };
            _mockService.Setup(s => s.CriarTarefaAsync(dto.ProjetoId, It.IsAny<Tarefa>())).ReturnsAsync(tarefaCriada);

            // Act
            var result = await _controller.CriarTarefa(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.ListarTarefas), createdResult.ActionName);
            Assert.Equal(tarefaCriada, createdResult.Value);
            Assert.Equal(dto.ProjetoId, ((dynamic)createdResult.RouteValues)["projetoId"]);
        }

        [Fact]
        public async Task CriarTarefa_WhenServiceReturnsNull_ReturnsBadRequest()
        {
            // Arrange
            var dto = new TarefaCreateDto
            {
                ProjetoId = 1,
                Titulo = "Nova Tarefa",
                Descricao = "Descricao",
                DataVencimento = DateTime.Now.AddDays(5),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Alta
            };
            _mockService.Setup(s => s.CriarTarefaAsync(dto.ProjetoId, It.IsAny<Tarefa>())).ReturnsAsync((Tarefa)null);

            // Act
            var result = await _controller.CriarTarefa(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("O projeto não existe ou atingiu o limite máximo de 20 tarefas.", badRequest.Value);
        }

        [Fact]
        public async Task AtualizarTarefa_WhenIdMismatch_ReturnsBadRequest()
        {
            // Arrange
            int id = 2;
            var dto = new TarefaUpdateDto
            {
                TarefaId = 3,
                Titulo = "Atualizada",
                Descricao = "Desc",
                DataVencimento = DateTime.Now.AddDays(2),
                Status = StatusTarefa.EmAndamento
            };

            // Act
            var result = await _controller.AtualizarTarefa(id, dto, usuarioId: 1);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("O id da URL não corresponde ao id da tarefa.", badRequest.Value);
        }

        [Fact]
        public async Task AtualizarTarefa_WhenTarefaNotFound_ReturnsNotFound()
        {
            // Arrange
            int id = 1;
            var dto = new TarefaUpdateDto
            {
                TarefaId = id,
                Titulo = "Atualizada",
                Descricao = "Desc",
                DataVencimento = DateTime.Now.AddDays(2),
                Status = StatusTarefa.EmAndamento
            };
            _mockService.Setup(s => s.ObterTarefaPorIdAsync(id)).ReturnsAsync((Tarefa)null);

            // Act
            var result = await _controller.AtualizarTarefa(id, dto, usuarioId: 1);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Tarefa não encontrada.", notFound.Value);
        }

        [Fact]
        public async Task AtualizarTarefa_WhenUpdateFails_ReturnsBadRequest()
        {
            // Arrange
            int id = 1;
            var tarefaAtual = new Tarefa
            {
                Id = id,
                ProjetoId = 1,
                Titulo = "Tarefa",
                Descricao = "Desc",
                DataVencimento = DateTime.Now.AddDays(2),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Baixa
            };
            var dto = new TarefaUpdateDto
            {
                TarefaId = id,
                Titulo = "Atualizada",
                Descricao = "Desc",
                DataVencimento = DateTime.Now.AddDays(3),
                Status = StatusTarefa.EmAndamento
            };
            _mockService.Setup(s => s.ObterTarefaPorIdAsync(id)).ReturnsAsync(tarefaAtual);
            _mockService.Setup(s => s.AtualizarTarefaAsync(id, It.IsAny<Tarefa>(), It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _controller.AtualizarTarefa(id, dto, usuarioId: 1);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Não é permitido alterar a prioridade da tarefa após a criação ou tarefa não encontrada.", badRequest.Value);
        }

        [Fact]
        public async Task AtualizarTarefa_WhenUpdateSucceeds_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            var tarefaAtual = new Tarefa
            {
                Id = id,
                ProjetoId = 1,
                Titulo = "Tarefa",
                Descricao = "Desc",
                DataVencimento = DateTime.Now.AddDays(2),
                Status = StatusTarefa.Pendente,
                Prioridade = PrioridadeTarefa.Baixa
            };
            var dto = new TarefaUpdateDto
            {
                TarefaId = id,
                Titulo = "Atualizada",
                Descricao = "Desc",
                DataVencimento = DateTime.Now.AddDays(3),
                Status = StatusTarefa.EmAndamento
            };
            _mockService.Setup(s => s.ObterTarefaPorIdAsync(id)).ReturnsAsync(tarefaAtual);
            _mockService.Setup(s => s.AtualizarTarefaAsync(id, It.IsAny<Tarefa>(), It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _controller.AtualizarTarefa(id, dto, usuarioId: 1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task RemoverTarefa_WhenRemovido_ReturnsNoContent()
        {
            // Arrange
            int id = 5;
            _mockService.Setup(s => s.RemoverTarefaAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.RemoverTarefa(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task RemoverTarefa_WhenNotRemovido_ReturnsNotFound()
        {
            // Arrange
            int id = 6;
            _mockService.Setup(s => s.RemoverTarefaAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _controller.RemoverTarefa(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}