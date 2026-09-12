using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DesWeb_23_08_26.Models
{
    public class Produto
    {
        [Required(ErrorMessage = "O código do produto é obrigatório.")]
        [ValidarCodigoProduto] // Atributo customizado criado abaixo
        public string CodigoProduto { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 150 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser um número negativo.")]
        public int Estoque { get; set; }
    }

    public class ValidarCodigoProdutoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("O código do produto é obrigatório.");
            }

            string codigo = value.ToString()!.Trim();

            if (codigo.Length != 8)
            {
                return new ValidationResult("O código do produto deve ter exatamente 8 caracteres (Exemplo: ABC-1234).");
            }

            if (!Regex.IsMatch(codigo, @"^[A-Z]{3}-\d{4}$"))
            {
                return new ValidationResult("O código do produto deve conter 3 letras MAIÚSCULAS, um hífen e 4 números (Exemplo: ABC-1234).");
            }

            return ValidationResult.Success;
        }
    }
}