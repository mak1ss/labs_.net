using ADO_Dapper_ServiceManagment.DAL.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Dapper_ServiceManagment.DAL.interfaces.sql.services
{
    public interface ITagService
    {

        long CreateTag(Tag entity);

        Tag GetTagById(int id);

        void UpdateTag(Tag entity);

        void DeleteTag(Tag entity);

        IEnumerable<Tag> GetAllTags();
    }
}
