using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_ServcieManagement.DAL.Interfaces
{
    public interface ISeeder<TEntity> where TEntity : class, IEntity<int>
    {

        void Seed(EntityTypeBuilder<TEntity> builder);
    }
}
