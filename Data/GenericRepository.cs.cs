using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data.Database;
using Core.Object;

namespace Data
{
    public partial class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly VideoEntities db = new VideoEntities();

        #region ADD
        public ObjectResult<T> Add(T entity)
        {
            ObjectResult<T> result = new ObjectResult<T>();
            try
            {
                using (VideoEntities context = new VideoEntities())
                {
                    context.Entry(entity).State = EntityState.Added;
                    context.SaveChanges();
                    result.status.OK();
                    result.obj = entity;
                }
            }
            catch (Exception ex)
            {
                result.status.Error("Database Error", ex.Message);
            }
            return result;
        }
        public List<ObjectResult<T>> Adds(List<T> entity)
        {
            List<ObjectResult<T>> result = new List<ObjectResult<T>>();
            foreach (var item in entity)
            {
                ObjectResult<T> added = new ObjectResult<T>();
                try
                {
                    using (VideoEntities context = new VideoEntities())
                    {
                        context.Entry(item).State = EntityState.Added;
                        context.SaveChanges();
                        added.obj = item;
                        added.status.OK();
                    }
                }
                catch (Exception ex)
                {
                    added.obj = item;
                    added.status.Error("Database Error", ex.Message);
                }
                result.Add(added);
            }
            return result;
        }

        public async Task<ObjectResult<T>> AddAsync(T entity)
        {
            ObjectResult<T> result = new ObjectResult<T>();

            try
            {
                using (VideoEntities context = new VideoEntities())
                {
                    context.Entry(entity).State = EntityState.Added;
                    await context.SaveChangesAsync();
                    result.status.OK();
                    result.obj = entity;
                }
            }
            catch (Exception ex)
            {
                result.status.Error("Database Error", ex.Message);
            }
            return result;
        }
        public async Task<List<ObjectResult<T>>> AddsAsync(List<T> entity)
        {
            List<ObjectResult<T>> result = new List<ObjectResult<T>>();
            foreach (var item in entity)
            {
                ObjectResult<T> added = new ObjectResult<T>();
                try
                {
                    using (VideoEntities context = new VideoEntities())
                    {
                        context.Entry(item).State = EntityState.Added;
                        await context.SaveChangesAsync();
                        added.obj = item;
                        added.status.OK();
                    }
                }
                catch (Exception ex)
                {
                    added.obj = item;
                    added.status.Error("Database Error", ex.Message);
                }
                result.Add(added);
            }
            return result;
        }
        #endregion

        #region Update
        public ServiceResult Update(T entity)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (VideoEntities context = new VideoEntities())
                {
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                    result.OK();
                }
            }
            catch (Exception ex)
            {
                result.Error("Database Error", ex.Message);
            }
            return result;
        }
        public List<ObjectResult<T>> Updates(List<T> entity)
        {
            List<ObjectResult<T>> result = new List<ObjectResult<T>>();
            foreach (var item in entity)
            {
                ObjectResult<T> added = new ObjectResult<T>();
                try
                {
                    using (VideoEntities context = new VideoEntities())
                    {
                        context.Entry(item).State = EntityState.Modified;
                        context.SaveChanges();
                        added.obj = item;
                        added.status.OK();
                    }
                }
                catch (Exception ex)
                {
                    added.obj = item;
                    added.status.Error("Database Error", ex.Message);
                }
                result.Add(added);
            }
            return result;
        }

        public async Task<ServiceResult> UpdateAsync(T entity)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                using (VideoEntities context = new VideoEntities())
                {
                    context.Entry(entity).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    result.OK();
                }
            }
            catch (Exception ex)
            {
                result.Error("Database Error", ex.Message);
            }
            return result;
        }
        public async Task<List<ObjectResult<T>>> UpdatesAsync(List<T> entity)
        {
            List<ObjectResult<T>> result = new List<ObjectResult<T>>();
            foreach (var item in entity)
            {
                ObjectResult<T> added = new ObjectResult<T>();
                try
                {
                    using (VideoEntities context = new VideoEntities())
                    {
                        context.Entry(item).State = EntityState.Modified;
                        await context.SaveChangesAsync();
                        added.obj = item;
                        added.status.OK();
                    }
                }
                catch (Exception ex)
                {
                    added.obj = item;
                    added.status.Error("Database Error", ex.Message);
                }
                result.Add(added);
            }
            return result;
        }
        #endregion

        #region Remove
        public ServiceResult Remove(T entity)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (VideoEntities context = new VideoEntities())
                {
                    context.Entry(entity).State = EntityState.Deleted;
                    context.SaveChanges();
                    result.OK();
                }
            }
            catch (Exception ex)
            {
                result.Error("Database Error", ex.Message);
            }
            return result;
        }
        public List<ObjectResult<T>> Removes(List<T> entity)
        {
            List<ObjectResult<T>> result = new List<ObjectResult<T>>();
            foreach (var item in entity)
            {
                ObjectResult<T> added = new ObjectResult<T>();
                try
                {
                    using (VideoEntities context = new VideoEntities())
                    {
                        context.Entry(item).State = EntityState.Deleted;
                        context.SaveChanges();
                        added.obj = item;
                        added.status.OK();
                    }
                }
                catch (Exception ex)
                {
                    added.obj = item;
                    added.status.Error("Database Error", ex.Message);
                }
                result.Add(added);
            }
            return result;
        }

        public async Task<ServiceResult> RemoveAsync(T entity)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                using (VideoEntities context = new VideoEntities())
                {
                    context.Entry(entity).State = EntityState.Deleted;
                    await context.SaveChangesAsync();
                    result.OK();
                }
            }
            catch (Exception ex)
            {
                result.Error("Database Error", ex.Message);
            }
            return result;
        }
        public async Task<List<ObjectResult<T>>> RemovesAsync(List<T> entity)
        {
            List<ObjectResult<T>> result = new List<ObjectResult<T>>();
            foreach (var item in entity)
            {
                ObjectResult<T> added = new ObjectResult<T>();
                try
                {
                    using (VideoEntities context = new VideoEntities())
                    {
                        context.Entry(item).State = EntityState.Deleted;
                        await context.SaveChangesAsync();
                        added.obj = item;
                        added.status.OK();
                    }
                }
                catch (Exception ex)
                {
                    added.obj = item;
                    added.status.Error("Database Error", ex.Message);
                }
                result.Add(added);
            }
            return result;
        }
        #endregion

        public async Task<List<T>> GetListAsync(IQueryable<T> query)
        {
            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<T> GetDataAsync(IQueryable<T> query)
        {
            return await query.AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(IQueryable<T> query)
        {
            return await query.AsNoTracking().CountAsync();
        }

        public IQueryable<T> GetTable
        {
            get { return db.Set<T>().AsNoTracking(); }
        }
    }
}
