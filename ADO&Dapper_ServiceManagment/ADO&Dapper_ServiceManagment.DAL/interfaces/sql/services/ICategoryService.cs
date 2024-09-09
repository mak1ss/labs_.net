using ADO_Dapper_ServiceManagment.DAL.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services
{
    public interface ICategoryService
    {

        long CreateCategory(Category entity);

        Category GetCategoryById(int id);

        void UpdateCategory(Category entity);

        void DeleteCategory(Category entity);

        IEnumerable<Category> GetAllCategories();
    }
}
