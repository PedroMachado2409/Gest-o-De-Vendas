namespace GestaoPedidos.Application.DTO
{
    public class ClienteResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public int Cpf { get; set; }
        public bool Ativo { get; set; } 
        public DateTime DataCadstro { get; set; } 
    }
}
