
using GestaoPedidos.Application.DTO.Produtos;
using GestaoPedidos.Application.UseCases.Produtos.Commands;
using GestaoPedidos.Application.UseCases.Produtos.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.WebAPI.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ListarProdutoUseCase _listarProdutoUseCase;
        private readonly CadastrarProdutoUseCase _cadastrarProdutoUseCase;
        private readonly AtualizarProdutoUseCase _atualizarProdutoUseCase;

        public ProdutoController(
            CadastrarProdutoUseCase cadastrarProdutoUseCase,
            ListarProdutoUseCase listarProdutoUseCase,
            AtualizarProdutoUseCase atualizarProdutoUseCase

        )
        {
            _cadastrarProdutoUseCase = cadastrarProdutoUseCase;
            _listarProdutoUseCase = listarProdutoUseCase;
            _atualizarProdutoUseCase= atualizarProdutoUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
            => Ok(await _listarProdutoUseCase.Executar());

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] ProdutoCreateDTO dto)
        {
            var produto = await _cadastrarProdutoUseCase.Executar(dto);
            return Ok(produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ProdutoUpdateDTO dto)
        {
            dto.Id = id;
            return Ok(await _atualizarProdutoUseCase.Executar(dto));
        }
    }
}
