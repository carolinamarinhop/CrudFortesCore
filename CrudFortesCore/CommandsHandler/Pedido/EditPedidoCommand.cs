using CrudFortesCore.DTO;
using MediatR;

namespace CrudFortesCore.CommandsHandler.Pedido
{
    public class EditPedidoCommand : IRequest
    {
        public EditPedidoCommand(PedidoDTO pedidoDTO) => PedidoDTO = pedidoDTO;

        public PedidoDTO PedidoDTO { get; }
    }
}
