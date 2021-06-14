using CrudFortesCore.DTO;
using MediatR;
using System.Collections.Generic;

namespace CrudFortesCore.CommandsHandler.Produto
{
    public class ListProdutoCommand : IRequest<List<ProdutoDTO>>
    {
        public ListProdutoCommand() { }
    }
}
