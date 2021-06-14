using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudFortesCore.Data;
using CrudFortesCore.Models;
using MediatR;
using CrudFortesCore.CommandsHandler.Produto;
using CrudFortesCore.DTO;

namespace CrudFortesCore.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IMediator _mediator;

        public ProdutoController(IMediator mediator) => _mediator = mediator;

        // GET: Produto  
        public async Task<IActionResult> Index() => View(await _mediator.Send(new ListProdutoCommand()));

        // GET: Produto/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var produto = await _mediator.Send(new ConsultProdutoCommand(id));
            return View(produto);
        }

        // GET: Produto/Create
        public IActionResult Create() => View();

        // POST: Produto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduto,Descricao,DataCadastro,Valor,DataAlteracao")] ProdutoDTO produtoDTO)
        {
            if (!ModelState.IsValid)
                return View(produtoDTO);

            await _mediator.Send(new CreateProdutoCommand(produtoDTO));
            return RedirectToAction(nameof(Index));
        }

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _mediator.Send(new ConsultProdutoCommand(id));

            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdProduto,Descricao,DataCadastro,Valor,DataAlteracao")] ProdutoDTO produtoDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(produtoDTO);
            }

            await _mediator.Send(new EditProdutoCommand(produtoDTO));
            return RedirectToAction(nameof(Index));
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _mediator.Send(new ConsultProdutoCommand(id));

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _mediator.Send(new DeleteProdutoCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
