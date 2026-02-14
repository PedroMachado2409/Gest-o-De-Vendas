using GestaoPedidos.Domain.Abstractions;
using NexusGym.Exceptions.Clientes;

namespace GestaoPedidos.Application.UseCases.Clientes.Commands
{
    public class InativarClienteUseCase
      : IUseCase<int, bool>
    {
        private readonly IClienteRepository _repository;

        public InativarClienteUseCase(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Execute(int id)
        {
            var cliente = await _repository.ObterPorId(id)
                ?? throw new(ClientesExceptions.Cliente_NaoEncontrado);

            if (cliente.Ativo == false)
                throw new(ClientesExceptions.Cliente_JaInativo);

            cliente.Inativar();
            await _repository.Atualizar(cliente);
            return true;
        }
    }
}
