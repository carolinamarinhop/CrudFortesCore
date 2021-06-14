using CrudFortesCore.DTO;
using MediatR;
using System.Collections.Generic;

namespace CrudFortesCore.CommandsHandler.Pedido
{
    public class ListPedidoCommand : IRequest<List<PedidoDTO>>
    {
        public ListPedidoCommand() { }
    }
}
