using MediatR;

namespace CrudFortesCore.CommandsHandler.Fornecedor
{
    public class DeleteFornecedorCommand : IRequest
    {
        public DeleteFornecedorCommand(int fornecedorId) => FornecedorId = fornecedorId;

        public int FornecedorId { get; }
    }
}
