using FluentAssertions;
using FluentValidation.Results;
using GestaoPedidos.Application.UseCases.Produtos.Commands;
using GestaoPedidos.Domain.Abstractions;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Exceptions.Produtos;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPedidosTests.Application.UseCases.Produtos.Commands
{
    [TestClass]
    public class AtivarProdutoUseCaseTests
    {
        private Mock<IProdutoRepository> _repositoryMock;
        private AtivarProdutoUseCase _useCase;


        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IProdutoRepository>();
            _useCase = new AtivarProdutoUseCase(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task Deve_Ativar_Produto_Quanto_Esta_Inativo()
        {
            var produto = new Produto("Produto Teste", "Marca Teste", 10, 15);
            produto.Inativar();

            _repositoryMock.Setup(r => r.ObterPorId(It.IsAny<int>())).ReturnsAsync(produto);
            var resultado = await _useCase.Executar(1);
            resultado.Should().BeTrue();
            produto.Ativo.Should().BeTrue();

            _repositoryMock.Verify(r => r.ObterPorId(1), Times.Once());
            _repositoryMock.Verify(r => r.Atualizar(produto), Times.Once);

        }

        [TestMethod]
        public async Task Deve_Lancar_Excecao_Quando_Ja_Estiver_Ativo()
        {
            var produto = new Produto("Produto Teste", "Marca Teste", 10, 15);
            _repositoryMock.Setup(r => r.ObterPorId(It.IsAny<int>())).ReturnsAsync(produto);
            Func<Task> act = () => _useCase.Executar(1);
            var exception = await act.Should().ThrowAsync<BadHttpRequestException>();
            exception.Which.Message.Should().Be(ProdutoExceptions.Produto_jaAtivo);

            _repositoryMock.Verify(r => r.ObterPorId(1), Times.Once());
            _repositoryMock.Verify(r => r.Atualizar(produto), Times.Never());
        }

        [TestMethod]
        public async Task Deve_Lancar_Excecao_Quando_Nao_Localizar_O_Produto()
        {
            var produto = new Produto("Produto Teste", "Marca Teste", 10, 15);
            _repositoryMock.Setup(r => r.ObterPorId(It.IsAny<int>())).ReturnsAsync((Produto?)null);
            Func<Task> act = () => _useCase.Executar(1);
            var exception = await act.Should().ThrowAsync<BadHttpRequestException>();
            exception.Which.Message.Should().Be(ProdutoExceptions.Produto_NaoEncontrado);

            _repositoryMock.Verify(r => r.ObterPorId(1), Times.Once());
            _repositoryMock.Verify(r => r.Cadastrar(produto), Times.Never());
        }


    }
}
