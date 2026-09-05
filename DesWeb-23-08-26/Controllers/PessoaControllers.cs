using Microsoft.AspNetCore.Mvc;
using DesWeb_23_08_26.Models;

namespace DesWeb_23_08_26.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private static readonly List<Pessoa> _pessoas = new List<Pessoa>();

        [HttpPost]
        public IActionResult Adicionar([FromBody] Pessoa pessoa)
        {
            if (_pessoas.Any(p => p.Cpf == pessoa.Cpf))
            {
                return BadRequest("Já existe uma pessoa cadastrada com este CPF.");
            }

            _pessoas.Add(pessoa);
            return CreatedAtAction(nameof(BuscarPorCpf), new { cpf = pessoa.Cpf }, pessoa);
        }

        [HttpPut("{cpf}")]
        public IActionResult Atualizar(string cpf, [FromBody] Pessoa pessoaAtualizada)
        {
            var pessoa = _pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            pessoa.Nome = pessoaAtualizada.Nome;
            pessoa.Peso = pessoaAtualizada.Peso;
            pessoa.Altura = pessoaAtualizada.Altura;

            return Ok(pessoa);
        }

        [HttpDelete("{cpf}")]
        public IActionResult Remover(string cpf)
        {
            var pessoa = _pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            _pessoas.Remove(pessoa);
            return NoContent();
        }

        [HttpGet]
        public IActionResult BuscarTodas()
        {
            return Ok(_pessoas);
        }

        [HttpGet("{cpf}")]
        public IActionResult BuscarPorCpf(string cpf)
        {
            var pessoa = _pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            return Ok(pessoa);
        }

        [HttpGet("imc-bom")]
        public IActionResult BuscarPorImcBom()
        {
            var pessoasImcBom = _pessoas.Where(p => p.Imc >= 18 && p.Imc <= 24).ToList();
            return Ok(pessoasImcBom);
        }

        [HttpGet("buscar-por-nome")]
        public IActionResult BuscarPorNome([FromQuery] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) return BadRequest("Informe um nome.");

            var resultado = _pessoas
                .Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(resultado);
        }
    }
}