namespace GestaoPedidos.Application.DTO.Produtos
{
    public class ProdutoCreateDTO
    {
        public string Nome { get; set; }

        public double Preco {  get; set; }
        public string Marca { get; set; }
        public int Estoque { get; set; }
    }
}
