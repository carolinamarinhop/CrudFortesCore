using CrudFortesCore.DTO;
using MediatR;

namespace CrudFortesCore.CommandsHandler.Pedido
{
    public class ConsultPedidoCommand : IRequest<PedidoDTO>
    {
        public ConsultPedidoCommand(int pedidoId) => PedidoId = pedidoId;

        public int PedidoId { get; }
    }
}
