using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDBProvider
{
    public class GenericRepo<T>  where T : BaseClass
    {
        public readonly ProductContext applicationContext;
        public DbSet<T> entity => applicationContext.Set<T>();

        public GenericRepo(ProductContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task Delete(int id)
        {


            var entity = this.applicationContext.Find<T>(id);
            if (entity == null)
            {
                throw new ArgumentNullException("entity id");
            }
            this.entity.Remove(entity);
            await this.applicationContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return this.entity.AsNoTracking().ToList<T>();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await this.applicationContext.FindAsync<T>(id);
        }

        public async Task<int?> Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            //configure await: this should only be used on tasks different from UI. On UI it should be set to TRUE to use the UI Thread. useful to avoud DEADLOCK

            await this.entity.AddAsync(entity).ConfigureAwait(false);
            await this.applicationContext.SaveChangesAsync().ConfigureAwait(false);

            return entity.Id;
        }

        public async Task Update(T entity)
        {

            this.applicationContext.Update<T>(entity);
            await this.applicationContext.SaveChangesAsync().ConfigureAwait(false);

        }

        public async Task UpdateBatch(List<T> records)
        {

            foreach (T record in records)
            {
                this.entity.Add(record);

            }
            await this.applicationContext.SaveChangesAsync().ConfigureAwait(false);

        }
    }
}
