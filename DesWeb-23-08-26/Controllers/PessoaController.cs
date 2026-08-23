using Microsoft.AspNetCore.Mvc;

namespace DWeb_23_08_26.Controllers
{
    public class PessoaData
    {
        public string Nome { get; set; } = string.Empty;
        public double Peso { get; set; }
        public double Altura { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        [HttpPost("calcular-imc")]
        public IActionResult CalcularImc([FromBody] PessoaData dados)
        {
            if (dados.Altura <= 0 || dados.Peso <= 0)
                return BadRequest("Peso e altura devem ser maiores que zero.");

            double imc = dados.Peso / (dados.Altura * dados.Altura);

            return Ok(new
            {
                dados.Nome,
                dados.Peso,
                dados.Altura,
                IMC = Math.Round(imc, 2)
            });
        }

        [HttpPost("consulta-tabela-imc")]
        public IActionResult ConsultaTabelaImc([FromBody] PessoaData dados)
        {
            if (dados.Altura <= 0 || dados.Peso <= 0)
                return BadRequest("Peso e altura devem ser maiores que zero.");

            double imc = dados.Peso / (dados.Altura * dados.Altura);

            string descricao = "Obesidade Grau III";
            if (imc < 18.5) descricao = "Abaixo do peso";
            else if (imc < 25.0) descricao = "Peso normal";
            else if (imc < 30.0) descricao = "Sobrepeso";
            else if (imc < 35.0) descricao = "Obesidade Grau I";
            else if (imc < 40.0) descricao = "Obesidade Grau II";

            return Ok(new
            {
                dados.Nome,
                IMC = Math.Round(imc, 2),
                Descricao = descricao
            });
        }
    }
}