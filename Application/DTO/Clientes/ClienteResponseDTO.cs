namespace GestaoPedidos.Application.DTO.Clientes
{
    public class ClienteResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadstro { get; set; }
    }
}
