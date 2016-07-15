using BaseWebProject.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseWebProject.Dapper;
using BaseWebProject.Identity.Dapper;

namespace BaseWebProject.Dal
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext db = ApplicationDbContext.Create();
        private readonly string _tableName;

        public SqlRepository(string tableName)
        {
            _tableName = tableName;
        }
        
        internal virtual dynamic Mapping(T item)
        {
            return item;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return db.Connection.Query<T>("SELECT * FROM " + _tableName);
        }

        public virtual IEnumerable<T> GetAll(int agencyId)
        {            
            return db.Connection.Query<T>("SELECT * FROM " + _tableName + " WHERE Agency_Id = @AgencyId", new { AgencyId = agencyId });           
        }

        public virtual T Get(int id, int agencyId)
        {
            return db.Connection.Query<T>("SELECT * FROM " + _tableName + " WHERE Id = @Id AND Agency_Id = @AgencyId", new {Id = id, AgencyId = agencyId }).SingleOrDefault(); 
        }

        public virtual int Create(T item, int userid)
        {
            if (typeof(IAudit).IsAssignableFrom(typeof(T)))
            {
                IAudit obj = item as IAudit;
                obj.CreatedBy_Member_Id = obj.ModifiedBy_Member_Id = userid;
                obj.CreatedOn = obj.LastModifiedOn = DateTime.UtcNow;
                var parameters = (object)Mapping(item);
                return db.Connection.Insert<int>(_tableName, parameters);
            }
            else
            {
                var parameters = (object)Mapping(item);
                return db.Connection.Insert<int>(_tableName, parameters);
            }
        }

        public virtual void Update(T item, int userid)
        {
            if (typeof(IAudit).IsAssignableFrom(typeof(T)))
            {
                IAudit obj = item as IAudit;
                obj.ModifiedBy_Member_Id = userid;
                obj.LastModifiedOn = DateTime.UtcNow;
                var parameters = (object)Mapping(item);
                db.Connection.Update(_tableName, parameters);
            }
            else
            {
                var parameters = (object)Mapping(item);
                db.Connection.Update(_tableName, parameters);
            }
        }

        public virtual void Delete(int id, int agencyId)
        {
            db.Connection.Execute("DELETE FROM " + _tableName + " WHERE Id=@Id AND Agency_Id = @AgencyId", new { Id = id, AgencyId = agencyId });
        }
    }
}
