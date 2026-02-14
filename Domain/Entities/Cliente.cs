namespace GestaoPedidos.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadstro { get; set; } = DateTime.UtcNow;

        protected Cliente() { }
        public Cliente (string nome, string email, string cpf)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        public void Atualizar(string nome, string email, string cpf)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            
        }

        public void Inativar() => Ativo = false;
        public void Ativar() => Ativo = true;
    }
}
