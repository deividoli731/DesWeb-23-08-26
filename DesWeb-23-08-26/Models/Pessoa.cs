namespace DesWeb_23_08_26.Models
{
    public class Pessoa
    {
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public double Peso { get; set; }
        public double Altura { get; set; }

        public double Imc => Altura > 0 ? Peso / (Altura * Altura) : 0;
    }
}