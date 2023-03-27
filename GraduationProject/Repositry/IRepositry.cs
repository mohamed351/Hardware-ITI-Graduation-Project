using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
   public interface IRepositry<T,ID> where T:class
    {
        public IEnumerable<T> GetAll();

        public IQueryable<T> GetIQueryable();

        public IEnumerable<T> GetByCondition(Func<T, bool> search);

        public T Get(ID Key);

        public void Add(T entity);

        public void Edit(T entity);

        public void Delete(T entity);

        public DataTableViewModel<T> GetDataTable(int start, int lenght, Func<T, bool> search, Func<T, ID> OrderBy);

        public int SaveAll();
        
    }

}
