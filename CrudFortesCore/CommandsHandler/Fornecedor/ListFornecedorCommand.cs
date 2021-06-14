using CrudFortesCore.DTO;
using MediatR;
using System.Collections.Generic;

namespace CrudFortesCore.CommandsHandler.Fornecedor
{
    public class ListFornecedorCommand : IRequest<List<FornecedorDTO>>
    {
        public ListFornecedorCommand() { }
    }
}
