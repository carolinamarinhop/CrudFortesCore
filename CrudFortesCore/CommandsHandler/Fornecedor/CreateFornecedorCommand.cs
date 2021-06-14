using CrudFortesCore.DTO;
using MediatR;

namespace CrudFortesCore.CommandsHandler.Fornecedor
{
    public class CreateFornecedorCommand : IRequest
    {
        public CreateFornecedorCommand(FornecedorDTO fornecedorDTO) => FornecedorDTO = fornecedorDTO;

        public FornecedorDTO FornecedorDTO { get; }
    }
}
