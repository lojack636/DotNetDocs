using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data
{
    public class EF_RepositoryDbContext : DbContext
    {
        public EF_RepositoryDbContext() : base("name=EF_Repository_DbContext")
        {

        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
        public DbSet<UserEntity> Users { set; get; }
    }

}
