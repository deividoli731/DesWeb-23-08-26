using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DesWeb_23_08_26.Models
{
    public class Aluno
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O RA é obrigatório.")]
        [ValidarRA]
        public string Ra { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail em formato válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos numéricos.")]
        public string Cpf { get; set; } = string.Empty;

        public bool Ativo { get; set; }
    }

    public class ValidarRAAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("O RA é obrigatório.");
            }

            string ra = value.ToString()!;

            if (!Regex.IsMatch(ra, @"^(RA|ra)\d{6}$"))
            {
                return new ValidationResult("O RA deve começar com 'RA' seguido de exatamente 6 dígitos numéricos (Exemplo: RA123456).");
            }

            return ValidationResult.Success;
        }
    }
}