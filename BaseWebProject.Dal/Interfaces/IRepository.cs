using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseWebProject.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(int agencyId);
        T Get(int id, int agencyId);
        int Create(T item, int userid);
        void Update(T item, int userid);
        void Delete(int id, int agencyId);      
    }
}
