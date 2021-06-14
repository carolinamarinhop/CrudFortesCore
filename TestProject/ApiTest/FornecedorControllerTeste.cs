using CrudFortesCore.Controllers;
using CrudFortesCore.DTO;
using CrudFortesCore.Models;
using CrudFortesCore.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace TestProject.ApiTeste
{
    class FornecedorControllerTeste : UnitTest
    {
        private FornecedorController fornecedorController;
        private IRepository<Fornecedor> fornecedorRepository;
        private FornecedorDTO novoFornecedor;
        
        [SetUp]
        public void Config()
        {
            fornecedorController = container.GetInstance<FornecedorController>();
            fornecedorRepository = container.GetInstance<IRepository<Fornecedor>>();

            novoFornecedor = new FornecedorDTO
            {
                RazaoSocial = "RazaoTeste",
                Cnpj = "03119537000163",
                Uf = "CE",
                EmailContato = "test@teste.com",
                NomeContato = "Teste"
            };
        }

        [NonParallelizable]
        [Test]
        public void CreateFornecedorTest()
        {
             CreateFornecedor();

            var fornecedorCreate = fornecedorRepository.Query().FirstOrDefault();

            Assert.AreEqual(novoFornecedor.RazaoSocial, fornecedorCreate.RazaoSocial);
            Assert.AreEqual(novoFornecedor.Cnpj, fornecedorCreate.Cnpj);
            Assert.AreEqual(novoFornecedor.Uf, fornecedorCreate.Uf);
            Assert.AreEqual(novoFornecedor.EmailContato, fornecedorCreate.EmailContato);
            Assert.AreEqual(novoFornecedor.NomeContato, fornecedorCreate.NomeContato);
            Assert.IsNull(fornecedorCreate.DataAlteracao);
        }

        [NonParallelizable]
        [Test]
        public void ListFornecedorTest()
        {
             CreateFornecedor();

            novoFornecedor = new FornecedorDTO
            {
                RazaoSocial = "RazaoTeste Segundo",
                Cnpj = "97848265000186",
                Uf = "PA",
                EmailContato = "outro@teste.com",
                NomeContato = "Outro Test"
            };

             CreateFornecedor();

            var fornecedores =  fornecedorController.Index().Result;

            var fornecedoresListModel = (List<FornecedorDTO>)((ViewResult)fornecedores).Model;

            var fornecedoresCreate = fornecedorRepository.Query().ToList();

            Assert.AreEqual(fornecedoresCreate.Count, fornecedoresListModel.Count);
        }

        [NonParallelizable]
        [Test]
        public void DeleteFornecedorTest()
        {
             CreateFornecedor();

            var fornecedorCreate = fornecedorRepository.Query().FirstOrDefault();

            _ = fornecedorController.DeleteConfirmed(fornecedorCreate.IdFornecedor);

            var fornecedorDelete = fornecedorRepository.Query().FirstOrDefault();

            Assert.IsNull(fornecedorDelete);
        }

        [NonParallelizable]
        [Test]
        public void EditFornecedorTest()
        {
             CreateFornecedor();

            var fornecedorCreateId = fornecedorRepository.Query().FirstOrDefault().IdFornecedor;

            var fornecedorEditDTO = new FornecedorDTO
            {
                RazaoSocial = "RazaoEdit",
                Cnpj = "55682472000163",
                IdFornecedor = fornecedorCreateId,
                EmailContato = "edit@teste.com,",
                Uf = "PA",
                NomeContato = "Edit"
            };

            _ = fornecedorController.Edit(fornecedorEditDTO);

            var fornecedorEdit = fornecedorRepository.Query().Where(w => w.IdFornecedor == fornecedorCreateId).FirstOrDefault();

            Assert.AreEqual(fornecedorEditDTO.RazaoSocial, fornecedorEdit.RazaoSocial);
            Assert.AreEqual(fornecedorEditDTO.Cnpj, fornecedorEdit.Cnpj);
            Assert.AreEqual(fornecedorEditDTO.Uf, fornecedorEdit.Uf);
            Assert.AreEqual(fornecedorEditDTO.EmailContato, fornecedorEdit.EmailContato);
            Assert.AreEqual(fornecedorEditDTO.NomeContato, fornecedorEdit.NomeContato);
            Assert.IsNotNull(fornecedorEdit.DataAlteracao);
        }

        private void CreateFornecedor() => fornecedorController.CreateAsync(novoFornecedor).Wait();
    }
}
