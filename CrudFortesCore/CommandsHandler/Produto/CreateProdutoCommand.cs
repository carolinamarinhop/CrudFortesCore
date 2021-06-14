using CrudFortesCore.DTO;
using MediatR;

namespace CrudFortesCore.CommandsHandler.Produto
{
    public class CreateProdutoCommand : IRequest
    {
        public CreateProdutoCommand(ProdutoDTO produtoDTO) => ProdutoDTO = produtoDTO;

        public ProdutoDTO ProdutoDTO { get; }
    }
}
