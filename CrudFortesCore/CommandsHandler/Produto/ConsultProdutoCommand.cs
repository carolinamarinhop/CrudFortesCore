using CrudFortesCore.DTO;
using MediatR;

namespace CrudFortesCore.CommandsHandler.Produto
{
    public class ConsultProdutoCommand : IRequest<ProdutoDTO>
    {
        public ConsultProdutoCommand(int produtoId) => ProdutoId = produtoId;

        public int ProdutoId { get; }
    }
}
