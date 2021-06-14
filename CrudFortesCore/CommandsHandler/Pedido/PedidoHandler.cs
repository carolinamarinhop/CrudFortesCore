using CrudFortesCore.DTO;
using CrudFortesCore.Repository.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrudFortesCore.CommandsHandler.Pedido
{
    public class PedidoHandler : IRequestHandler<ListPedidoCommand, List<PedidoDTO>>,
                                 IRequestHandler<CreatePedidoCommand>,
                                 IRequestHandler<DeletePedidoCommand>,
                                 IRequestHandler<EditPedidoCommand>,
                                 IRequestHandler<ConsultPedidoCommand, PedidoDTO>

    {
        private readonly IRepository<Models.Pedido> _pedidoRepository;
        private readonly IRepository<Models.Produto> _produtoRepository;

        public PedidoHandler(IRepository<Models.Pedido> pedidoRepository, IRepository<Models.Produto> produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public Task<List<PedidoDTO>> Handle(ListPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedidos = _pedidoRepository.Query().Select(pedido => new PedidoDTO
            {
                IdFornecedor = pedido.IdFornecedor,
                Fornecedor = pedido.Fornecedor,
                Produto = pedido.Produto,
                IdProduto = pedido.IdProduto,
                IdPedido = pedido.IdPedido,
                QtdPedido = pedido.QtdPedido,
                DataPedido = pedido.DataPedido,
                ValorTotal = pedido.ValorTotal
            }).ToList();

            return Task.FromResult(pedidos);
        }

        public Task<Unit> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
        {
            var produto = _produtoRepository.Find(request.PedidoDTO.IdProduto);

            _pedidoRepository.Create(new Models.Pedido(request.PedidoDTO.IdFornecedor,
                request.PedidoDTO.IdProduto,
                request.PedidoDTO.QtdPedido,
                request.PedidoDTO.ValorTotal = request.PedidoDTO.QtdPedido * produto.Valor));

            return Task.FromResult(Unit.Value);
        }

        public Task<Unit> Handle(DeletePedidoCommand request, CancellationToken cancellationToken)
        {
            _pedidoRepository.Delete(_pedidoRepository.Find(request.PedidoId));
            return Task.FromResult(Unit.Value);
        }

        public Task<Unit> Handle(EditPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = _pedidoRepository.Find(request.PedidoDTO.IdPedido);
            var produto = _produtoRepository.Find(request.PedidoDTO.IdProduto);

            pedido.IdFornecedor = request.PedidoDTO.IdFornecedor;
            pedido.IdProduto = request.PedidoDTO.IdProduto;
            pedido.QtdPedido = request.PedidoDTO.QtdPedido;
            pedido.ValorTotal = request.PedidoDTO.ValorTotal = request.PedidoDTO.QtdPedido * produto.Valor;
            pedido.DataAlteracao = DateTime.Now;

            _pedidoRepository.Edit(pedido);

            return Task.FromResult(Unit.Value);
        }

        public Task<PedidoDTO> Handle(ConsultPedidoCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_pedidoRepository.Query().Where(p => p.IdPedido == request.PedidoId).Select(pedido => new PedidoDTO
            {
                IdPedido = pedido.IdPedido,
                IdFornecedor = pedido.IdFornecedor,
                IdProduto = pedido.IdProduto,
                QtdPedido = pedido.QtdPedido,
                ValorTotal = pedido.ValorTotal
            }).FirstOrDefault());
        }
    }
}

