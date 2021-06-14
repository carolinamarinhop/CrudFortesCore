using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudFortesCore.Data;
using CrudFortesCore.Models;
using MediatR;
using CrudFortesCore.CommandsHandler.Pedido;
using CrudFortesCore.DTO;
using CrudFortesCore.CommandsHandler.Fornecedor;
using CrudFortesCore.CommandsHandler.Produto;

namespace CrudFortesCore.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IMediator _mediator;
        public PedidoController(IMediator mediator) => _mediator = mediator;

        // GET: Pedido
        public async Task<IActionResult> Index() => View(await _mediator.Send(new ListPedidoCommand()));

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pedido = await _mediator.Send(new ConsultPedidoCommand(id));

            return View(pedido);
        }

        // GET: Pedido/Create
        public async Task<IActionResult> CreateAsync()
        {
            var fornecedores = await _mediator.Send(new ListFornecedorCommand());
            var produtos = await _mediator.Send(new ListProdutoCommand());
            ViewData["IdFornecedor"] = new SelectList(fornecedores, "IdFornecedor", "RazaoSocial");
            ViewData["IdProduto"] = new SelectList(produtos, "IdProduto", "Descricao");
            return View();
        }

        // POST: Pedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedido,DataPedido,IdProduto,QtdPedido,IdFornecedor,ValorTotal,DataAlteracao")] PedidoDTO pedidoDTO)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreatePedidoCommand(pedidoDTO));
                return RedirectToAction(nameof(Index));
            }

            var fornecedores = await _mediator.Send(new ListFornecedorCommand());
            var produtos = await _mediator.Send(new ListProdutoCommand());

            ViewData["IdFornecedor"] = new SelectList(fornecedores, "IdFornecedor", "RazaoSocial", pedidoDTO.IdFornecedor);
            ViewData["IdProduto"] = new SelectList(produtos, "IdProduto", "Descricao", pedidoDTO.IdProduto);
            return View(pedidoDTO);
        }

        // GET: Pedido/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var pedido = await _mediator.Send(new ConsultPedidoCommand(id));

            var fornecedores = await _mediator.Send(new ListFornecedorCommand());
            var produtos = await _mediator.Send(new ListProdutoCommand());

            ViewData["IdFornecedor"] = new SelectList(fornecedores, "IdFornecedor", "RazaoSocial", pedido.IdFornecedor);
            ViewData["IdProduto"] = new SelectList(produtos, "IdProduto", "Descricao", pedido.IdProduto);

            return View(pedido);
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdPedido,DataPedido,IdProduto,QtdPedido,IdFornecedor,ValorTotal,DataAlteracao")] PedidoDTO pedidoDTO)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new EditPedidoCommand(pedidoDTO));
                return RedirectToAction(nameof(Index));
            }
            var fornecedores = await _mediator.Send(new ListFornecedorCommand());
            var produtos = await _mediator.Send(new ListProdutoCommand());

            ViewData["IdFornecedor"] = new SelectList(fornecedores, "IdFornecedor", "RazaoSocial", pedidoDTO.IdFornecedor);
            ViewData["IdProduto"] = new SelectList(produtos, "IdProduto", "Descricao", pedidoDTO.IdProduto);
            return View(pedidoDTO);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _mediator.Send(new ConsultPedidoCommand(id));

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _mediator.Send(new DeletePedidoCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
