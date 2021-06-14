using CrudFortesCore.DTO;
using MediatR;

namespace CrudFortesCore.CommandsHandler.Fornecedor
{
    public class EditFornecedorCommand : IRequest
    {
        public EditFornecedorCommand(FornecedorDTO fornecedorDTO) => FornecedorDTO = fornecedorDTO;

        public FornecedorDTO FornecedorDTO { get; }
    }
}
