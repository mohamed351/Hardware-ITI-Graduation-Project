using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class Repositry<T, ID> : IRepositry<T, ID> where T:class
    {

        public Repositry(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            Context.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Edit(T entity)
        {
            Context.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }
        public IEnumerable<T> GetByCondition(Func<T,bool> search)
        {
            return Context.Set<T>().Where(search).ToList();
        }

        public T Get(ID Key)
        {
            return Context.Set<T>().Find(Key);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
       

        public DataTableViewModel<T> GetDataTable(int start, int lenght, Func<T, bool> search, Func<T, ID> OrderBy)
        {
            ///query the skip number of Rows and Take number of rows 
            ///Like Take and Fetch 
            var query = Context.Set<T>().Where(search)
                   .OrderBy(OrderBy).Skip(start)
                   .Take(lenght);
            ///Get the Total Count 
            var count = Context.Set<T>().Where(search).Count();
            ///Return The ViewModel That DataTable will take it in Json 
            return new DataTableViewModel<T>()
            {
                data = query,
                recordsFiltered = count,
                recordsTotal = count
            };
        }

        public IQueryable<T> GetIQueryable()
        {
            return Context.Set<T>();
        }

        public int SaveAll()
        {
           return  Context.SaveChanges();
        }
    }
}
