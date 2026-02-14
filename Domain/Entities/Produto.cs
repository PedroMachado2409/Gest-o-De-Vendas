namespace GestaoPedidos.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public double Preco { get; set; }
        public int Estoque { get; set; } = 0;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;
        protected Produto() { }

        public Produto (string nome, string marca, int estoque, double preco)
        {
            Nome = nome;
            Marca = marca;
            Estoque = estoque;
            Preco = preco;
        }
        public void Atualizar(string nome, string marca, int estoque, double preco)
        {
            Nome = nome;
            Marca = marca;
            Estoque = estoque;
            Preco = preco;
        }

        public void Ativar () => Ativo = true;
        public void Inativar () => Ativo = false;



    }
}
