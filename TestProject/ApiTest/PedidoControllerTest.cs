using CrudFortesCore.Controllers;
using CrudFortesCore.DTO;
using CrudFortesCore.Models;
using CrudFortesCore.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.ApiTeste
{
    class PedidoControllerTest : UnitTest
    {
        private FornecedorController fornecedorController;
        private IRepository<Fornecedor> fornecedorRepository;
        private FornecedorDTO novoFornecedor;

        private ProdutoController produtoController;
        private IRepository<Produto> produtoRepository;
        private ProdutoDTO novoProduto;

        private PedidoController pedidoController;
        private IRepository<Pedido> pedidoRepository;
        private PedidoDTO novoPedido;

        [SetUp]
        public void Config()
        {
            fornecedorController = container.GetInstance<FornecedorController>();
            fornecedorRepository = container.GetInstance<IRepository<Fornecedor>>();

            produtoController = container.GetInstance<ProdutoController>();
            produtoRepository = container.GetInstance<IRepository<Produto>>();

            pedidoController = container.GetInstance<PedidoController>();
            pedidoRepository = container.GetInstance<IRepository<Pedido>>();

            novoProduto = new ProdutoDTO
            {
                Descricao = "Descricao teste",
                ValorAux = "8"
            };

            novoFornecedor = new FornecedorDTO
            {
                RazaoSocial = "RazaoTeste",
                Cnpj = "03119537000163",
                Uf = "CE",
                EmailContato = "test@teste.com",
                NomeContato = "Teste"
            };

            novoPedido = new PedidoDTO
            {
                IdFornecedor = novoFornecedor.IdFornecedor,
                IdProduto = novoProduto.IdProduto,
                QtdPedido = 4
            };
        }
        [NonParallelizable]
        [Test]
        public async Task CreatePedidoTest()
        {

            await CreateFornecedor();
            await CreateProduto();
            await CreatePedido();

            var pedidoCreate = pedidoRepository.Query().FirstOrDefault();

            Assert.AreEqual(novoPedido.IdFornecedor, pedidoCreate.IdFornecedor);
            Assert.AreEqual(novoPedido.IdProduto, pedidoCreate.IdProduto);
            Assert.AreEqual(novoPedido.QtdPedido, pedidoCreate.QtdPedido);
            Assert.AreEqual(novoPedido.ValorTotal, pedidoCreate.ValorTotal);
            Assert.IsNull(pedidoCreate.DataAlteracao);
        }

        [NonParallelizable]
        [Test]
        public async Task ListPedidoTest()
        {
            await CreateFornecedor();
            await CreateProduto();
            await CreatePedido();

            novoProduto = new ProdutoDTO
            {
                Descricao = "Descricao Segundo",
                Valor = 9
            };

            novoFornecedor = new FornecedorDTO
            {
                RazaoSocial = "RazaoTeste Segundo",
                Cnpj = "97848265000186",
                Uf = "PA",
                EmailContato = "outro@teste.com",
                NomeContato = "Outro Test"
            };

            novoPedido = new PedidoDTO
            {
                IdFornecedor = 1,
                IdProduto = 1,
                QtdPedido = 4,
                ValorTotal = 4 * novoProduto.Valor
            };

            await CreateFornecedor();
            await CreateProduto();
            await CreatePedido();

            var pedidos = await pedidoController.Index();

            var pedidosListModel = (List<PedidoDTO>)((ViewResult)pedidos).Model;

            var pedidosCreate = pedidoRepository.Query().ToList();

            Assert.AreEqual(pedidosCreate.Count, pedidosListModel.Count);
        }

        [NonParallelizable]
        [Test]
        public async Task DeletePedidoTest()
        {
            await CreateFornecedor();
            await CreateProduto();
            await CreatePedido();

            var pedidoCreate = pedidoRepository.Query().FirstOrDefault();

            await pedidoController.DeleteConfirmed(pedidoCreate.IdPedido);

            var pedidoDelete = pedidoRepository.Query().FirstOrDefault();

            Assert.IsNull(pedidoDelete);
        }
        [NonParallelizable]
        [Test]
        public async Task EditPedidoTest()
        {
            await CreateFornecedor();
            await CreateProduto();
            await CreatePedido();

            var pedidoCreateID = pedidoRepository.Query().Select(s => s.IdPedido).FirstOrDefault();

            var produtoId = produtoRepository.Query().Select(s => s.IdProduto).FirstOrDefault();
            var fornecedorId = fornecedorRepository.Query().Select(s => s.IdFornecedor).FirstOrDefault();


            var pedidoEditDTO = new PedidoDTO
            {
                IdFornecedor = fornecedorId,
                IdProduto = produtoId,
                QtdPedido = 7,
                IdPedido = pedidoCreateID

            };

            await pedidoController.Edit(pedidoEditDTO);

            var pedidoEdit = pedidoRepository.Query().Where(w => w.IdPedido == pedidoCreateID).FirstOrDefault();

            Assert.AreEqual(pedidoEditDTO.IdFornecedor, pedidoEdit.IdFornecedor);
            Assert.AreEqual(pedidoEditDTO.IdProduto, pedidoEdit.IdProduto);
            Assert.AreEqual(pedidoEditDTO.QtdPedido, pedidoEdit.QtdPedido);
            Assert.AreEqual(pedidoEditDTO.ValorTotal, pedidoEdit.ValorTotal);
            Assert.IsNotNull(pedidoEdit.DataAlteracao);
        }

        private async Task CreateFornecedor() => await fornecedorController.CreateAsync(novoFornecedor);
        private async Task CreateProduto() => await produtoController.Create(novoProduto);
        private async Task CreatePedido()
        {
            var produtoId = produtoRepository.Query().Select(s => s.IdProduto).FirstOrDefault();
            var fornecedorId = fornecedorRepository.Query().Select(s => s.IdFornecedor).FirstOrDefault();

            novoPedido.IdFornecedor = fornecedorId;
            novoPedido.IdProduto = produtoId;

            await pedidoController.Create(novoPedido);

        }
    }
}
