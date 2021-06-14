using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrudFortesCore.DTO;
using MediatR;
using CrudFortesCore.CommandsHandler.Fornecedor;

namespace CrudFortesCore.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IMediator _mediator;

        public FornecedorController(IMediator mediator) => _mediator = mediator;

        // GET: Fornecedor
        public async Task<IActionResult> Index() => View(await _mediator.Send(new ListFornecedorCommand()));

        // GET: Fornecedor/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var fornecedor = await _mediator.Send(new ConsultFornecedorCommand(id));

            return View(fornecedor);
        }

        // GET: Fornecedor/Create
        public IActionResult Create() => View();

        // POST: Fornecedor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("IdFornecedor,RazaoSocial,Cnpj,Uf,EmailContato,NomeContato,DataAlteracao")] FornecedorDTO fornecedorDTO)
        {
            if (!ModelState.IsValid)
                return View(fornecedorDTO);

            await _mediator.Send(new CreateFornecedorCommand(fornecedorDTO));
            return RedirectToAction(nameof(Index));
        }

        // GET: Fornecedor/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var fornecedor = await _mediator.Send(new ConsultFornecedorCommand(id));

            return View(fornecedor);
        }

        // POST: Fornecedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdFornecedor,RazaoSocial,Cnpj,Uf,EmailContato,NomeContato,DataAlteracao")] FornecedorDTO fornecedorDTO)
        {
            if (!ModelState.IsValid)
                return View(fornecedorDTO);

            await _mediator.Send(new EditFornecedorCommand(fornecedorDTO));
            return RedirectToAction(nameof(Index));
        }

        // GET: Fornecedor/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var fornecedor = await _mediator.Send(new ConsultFornecedorCommand(id));

            return View(fornecedor);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _mediator.Send(new DeleteFornecedorCommand(id));

            return RedirectToAction(nameof(Index));
        }
    }
}
