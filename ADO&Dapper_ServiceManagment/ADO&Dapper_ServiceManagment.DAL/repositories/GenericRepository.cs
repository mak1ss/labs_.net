using ADO_Dapper_ServiceManagment.DAL.interfaces;
using ADO_Dapper_ServiceManagment.DAL.interfaces.entities;
using Dapper;
using System.Data;
using System.Reflection;

namespace ADO_Dapper_ServiceManagment.DAL.repositories
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        protected IConnectionFactory _connectionFactory;
        private readonly string _tableName;

        public GenericRepository(IConnectionFactory connectionFactory, string tableName)
        {
            _connectionFactory = connectionFactory;
            _tableName = tableName;
        }

        public long Add(TEntity entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns);
            var stringOfProperties = string.Join(", ", columns.Select(e => "@" + e));
            var query = "SP_InsertRecord";

            using (var db = _connectionFactory.GetSqlConnection)
            {
                var InsertionStatement = db.Query<string>(
                    sql: query,
                    param: new { P_tableName = _tableName, P_columnsString = stringOfColumns, P_propertiesString = stringOfProperties },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                var InsertedEntityId = db.Execute(
                    sql: InsertionStatement,
                    param: entity,
                    commandType: CommandType.Text);

                return InsertedEntityId;
            }
        }

        public void Update(TEntity entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));

            using (var db = _connectionFactory.GetSqlConnection)
            {
                var query = "SP_UpdateRecord";

                var UpdateStatement = db.Query<string>(
                    sql: query,
                    param: new { P_tableName = _tableName, P_columnsString = stringOfColumns, P_Id = entity.Id },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                db.Execute(
                    sql: UpdateStatement,
                    param: entity,
                    commandType: CommandType.Text);
            }
        }

        public void Delete(TEntity entity)
        {
            using (var db = _connectionFactory.GetSqlConnection)
            {
                var query = "SP_DeleteRecord";
                db.Execute(
                    sql: query,
                    param: new { P_tableName = _tableName, P_Id = entity.Id },
                    commandType: CommandType.StoredProcedure);
            }  
        }

        public TEntity Get(TId id)
        {
            var query = "SP_GetRecordById";

            using (var db = _connectionFactory.GetSqlConnection)
            {
                return db.Query<TEntity>(query,
                    new { P_tableName = _tableName, P_Id = id.ToString() },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            var query = "SP_GetAllRecords";

            using (var db = _connectionFactory.GetSqlConnection)
            {
                return db.Query<TEntity>(query,
                    new { P_tableName = _tableName },
                    commandType: CommandType.StoredProcedure);
            }
        }

        private IEnumerable<string> GetColumns()
        {
            return typeof(TEntity)
                    .GetProperties()
                    .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
    }
}
