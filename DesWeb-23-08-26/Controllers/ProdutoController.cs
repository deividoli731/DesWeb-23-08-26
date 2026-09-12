using Microsoft.AspNetCore.Mvc;
using DesWeb_23_08_26.Models;

namespace DesWeb_23_08_26.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private static readonly List<Produto> _produtos = new List<Produto>();

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Produto produto)
        {
            if (_produtos.Any(p => p.CodigoProduto.Equals(produto.CodigoProduto, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Já existe um produto cadastrado com este código.");
            }

            _produtos.Add(produto);
            return CreatedAtAction(nameof(BuscarPorCodigo), new { codigo = produto.CodigoProduto }, produto);
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok(_produtos);
        }

        [HttpGet("{codigo}")]
        public IActionResult BuscarPorCodigo(string codigo)
        {
            var produto = _produtos.FirstOrDefault(p => p.CodigoProduto.Equals(codigo, StringComparison.OrdinalIgnoreCase));
            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(produto);
        }

        [HttpPut("{codigo}")]
        public IActionResult Atualizar(string codigo, [FromBody] Produto produtoAtualizado)
        {
            var produto = _produtos.FirstOrDefault(p => p.CodigoProduto.Equals(codigo, StringComparison.OrdinalIgnoreCase));
            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            produto.Descricao = produtoAtualizado.Descricao;
            produto.Preco = produtoAtualizado.Preco;
            produto.Estoque = produtoAtualizado.Estoque;

            return Ok(produto);
        }

        [HttpDelete("{codigo}")]
        public IActionResult Remover(string codigo)
        {
            var produto = _produtos.FirstOrDefault(p => p.CodigoProduto.Equals(codigo, StringComparison.OrdinalIgnoreCase));
            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            _produtos.Remove(produto);
            return NoContent();
        }
    }
}