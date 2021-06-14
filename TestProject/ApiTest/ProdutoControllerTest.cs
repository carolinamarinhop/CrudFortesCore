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
    class ProdutoControllerTest : UnitTest
    {
        private ProdutoController produtoController;
        private IRepository<Produto> produtoRepository;
        private ProdutoDTO novoProduto;

        [SetUp]
        public void Config()
        {
            produtoController = container.GetInstance<ProdutoController>();
            produtoRepository = container.GetInstance<IRepository<Produto>>();

            novoProduto = new ProdutoDTO
            {
                Descricao = "Descricao teste",
                ValorAux = "8",
                Valor = 8
            };
        }

        [NonParallelizable]
        [Test]
        public async Task CreatePedidoTest()
        {
            await CreateProduto();

            var produtoCreate = produtoRepository.Query().FirstOrDefault();

            Assert.AreEqual(novoProduto.Descricao, produtoCreate.Descricao);
            Assert.AreEqual(novoProduto.Valor, produtoCreate.Valor);
            Assert.AreEqual(novoProduto.DataCadastro, produtoCreate.DataCadastro);
            Assert.IsNull(produtoCreate.DataAlteracao);
        }

        [NonParallelizable]
        [Test]
        public async Task ListProdutoTest()
        {
            await CreateProduto();

            novoProduto = new ProdutoDTO
            {
                Descricao = "Descricao Segundo",
                ValorAux = "9",
                Valor = 9
            };

            await CreateProduto();

            var produtos = await produtoController.Index();

            var produtosListModel = (List<ProdutoDTO>)((ViewResult)produtos).Model;

            var produtosCreate = produtoRepository.Query().ToList();

            Assert.AreEqual(produtosCreate.Count, produtosListModel.Count);
        }

        [NonParallelizable]
        [Test]
        public async Task DeleteProdutoTest()
        {
            await CreateProduto();

            var produtoCreate = produtoRepository.Query().FirstOrDefault();

            await produtoController.DeleteConfirmed(produtoCreate.IdProduto);

            var produtoDelete = produtoRepository.Query().FirstOrDefault();

            Assert.IsNull(produtoDelete);
        }

        [NonParallelizable]
        [Test]
        public async Task EditProdutoTest()
        {
            await CreateProduto();

            var produtoCreate = produtoRepository.Query().FirstOrDefault();

            var produtoEditDTO = new ProdutoDTO
            {
                Descricao = "DescricaoEdit",
                ValorAux = "7",
                Valor = 7,
                IdProduto = produtoCreate.IdProduto,
                DataCadastro = produtoCreate.DataCadastro
            };

            await produtoController.Edit(produtoEditDTO);

            var produtoEdit = produtoRepository.Query().Where(w => w.IdProduto == produtoCreate.IdProduto).FirstOrDefault();

            Assert.AreEqual(produtoEditDTO.Descricao, produtoEdit.Descricao);
            Assert.AreEqual(produtoEditDTO.Valor, produtoEdit.Valor);
            Assert.AreEqual(produtoEditDTO.DataCadastro, produtoEdit.DataCadastro);
            Assert.IsNotNull(produtoEdit.DataAlteracao);
        }

        private async Task CreateProduto() => await produtoController.Create(novoProduto);
    }
}
