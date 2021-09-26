using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Microservice.Consumer.Infra.Data.Context
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions op) : base(op)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
