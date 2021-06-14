using CrudFortesCore.DTO;
using MediatR;

namespace CrudFortesCore.CommandsHandler.Produto
{
    public class EditProdutoCommand : IRequest
    {
        public EditProdutoCommand(ProdutoDTO produtoDTO) => ProdutoDTO = produtoDTO;

        public ProdutoDTO ProdutoDTO { get; }
    }
}
