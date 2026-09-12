using Microsoft.AspNetCore.Mvc;
using DesWeb_23_08_26.Models;

namespace DesWeb_23_08_26.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private static readonly List<Aluno> _alunos = new List<Aluno>();

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Aluno aluno)
        {
            if (_alunos.Any(a => a.Ra.Equals(aluno.Ra, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Já existe um aluno cadastrado com este RA.");
            }

            _alunos.Add(aluno);
            return CreatedAtAction(nameof(BuscarPorRa), new { ra = aluno.Ra }, aluno);
        }

        [HttpGet("{ra}")]
        public IActionResult BuscarPorRa(string ra)
        {
            var aluno = _alunos.FirstOrDefault(a => a.Ra.Equals(ra, StringComparison.OrdinalIgnoreCase));
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            return Ok(aluno);
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok(_alunos);
        }
    }
}