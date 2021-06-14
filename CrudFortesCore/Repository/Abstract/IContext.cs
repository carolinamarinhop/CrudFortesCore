using CrudFortesCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace CrudFortesCore.Repository.Abstract
{
    public interface IContext
    {
        DatabaseFacade Database { get; }
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T targetValue) where T : class;
        int SaveChanges();

        DbSet<Fornecedor> Fornecedor { get; set; }
        DbSet<Pedido> Pedido { get; set; }
        DbSet<Produto> Produto { get; set; }
    
    }
}
