using CrudFortesCore.DTO;
using MediatR;

namespace CrudFortesCore.CommandsHandler.Fornecedor
{
    public class ConsultFornecedorCommand : IRequest<FornecedorDTO>
    {
        public ConsultFornecedorCommand(int fornecedorId) => FornecedorId = fornecedorId;

        public int FornecedorId { get; }
    }
}
