using MediatR;

namespace CrudFortesCore.CommandsHandler.Produto
{
    public class DeleteProdutoCommand : IRequest
    {
        public DeleteProdutoCommand(int produtoId) => ProdutoId = produtoId;

        public int ProdutoId { get; }
    }
}
