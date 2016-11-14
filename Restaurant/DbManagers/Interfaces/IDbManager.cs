using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DbManagers.Interfaces
{
    public interface IDbManager<T> where T : class
    {
        void Create(T item);
        void Edit(T item);
        void Delete(T item);
        List<T> FindAll();
        List<T> FildAll(Func<T, bool> predicate);
        //T Get(string id);
        //T Get(Func<T, bool> predicate);

        #region Find
        T Find(string id);
        T Find(int id);
        #endregion

        void Save();
    }
}
