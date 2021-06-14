using CrudFortesCore.DTO;
using MediatR;

namespace CrudFortesCore.CommandsHandler.Pedido
{
    public class CreatePedidoCommand : IRequest
    {
        public CreatePedidoCommand(PedidoDTO pedidoDTO) => PedidoDTO = pedidoDTO;

        public PedidoDTO PedidoDTO { get; }
    }
}
