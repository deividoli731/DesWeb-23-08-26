using Microsoft.AspNetCore.Mvc;

namespace MeuProjeto.Controllers
{
    public class ProdutoData
    {
        public string NomeProduto { get; set; } = string.Empty;
        public float Peso { get; set; } 
        public float Altura { get; set; } 
        public float Largura { get; set; } 
        public float Comprimento { get; set; } 
        public string UF { get; set; } = string.Empty; 
    }

    [ApiController]
    [Route("api/[controller]")]
    public class FreteController : ControllerBase
    {
        [HttpPost("calcular-frete")]
        public IActionResult CalcularFrete([FromBody] ProdutoData produto)
        {
            if (produto.Altura <= 0 || produto.Largura <= 0 || produto.Comprimento <= 0)
            {
                return BadRequest("Altura, largura e comprimento devem ser maiores que zero.");
            }

            float volume = produto.Altura * produto.Largura * produto.Comprimento;

            double taxaEstado = produto.UF.ToUpper() switch
            {
                "SP" => 50.0,
                "RJ" => 60.0,
                "MG" => 55.0,
                _ => 70.0 
            };

            double taxaPorCm3 = 0.01;

            double valorFrete = (volume * taxaPorCm3) + taxaEstado;

            return Ok(new
            {
                Produto = produto.NomeProduto,
                PesoKg = produto.Peso,
                VolumeCm3 = volume,
                UF = produto.UF.ToUpper(),
                TaxaEstado = taxaEstado,
                ValorFrete = Math.Round(valorFrete, 2)
            });
        }
    }
}