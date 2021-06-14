using MediatR;

namespace CrudFortesCore.CommandsHandler.Pedido
{
    public class DeletePedidoCommand : IRequest
    {
        public DeletePedidoCommand(int pedidoId) => PedidoId = pedidoId;

        public int PedidoId { get; }
    }
}
