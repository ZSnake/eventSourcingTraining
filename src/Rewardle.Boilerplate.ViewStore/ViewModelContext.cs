using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Rewardle.Boilerplate.Domain;
using Rewardle.Boilerplate.Domain.Services;
using Rewardle.Boilerplate.Domain.Services.Interfaces;

namespace Rewardle.Boilerplate.ViewStore
{
    public class ViewModelContext : DbContext, IViewStore, IViewModelWriter
    {
        readonly ILogger _logger;

        public ViewModelContext(string connectionString, ILogger logger) : base(connectionString)
        {
            _logger = logger;
        }

        public async Task UpdateModel()
        {
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Query<T>(Expression<Func<T, bool>> expression) where T : class, IViewModel
        {
            try
            {
                DbSet<T> dbSet = Set<T>();
                IQueryable<T> queryable = dbSet.Where(expression);
                var list = await queryable.ToListAsync();
                return list;
            }
            catch (InvalidOperationException ex)
            {
                _logger.Error("Error when trying to query the view store.", ex, this);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("Error when trying to query the view store.", ex, this);
                throw;
            }
            
        }

        public async Task Add<T>(T model) where T : class, IViewModel
        {
            Set<T>().Add(model);
            await SaveChangesAsync();
        }

        public async Task<T> Get<T>(int id) where T : class, IViewModel
        {
            return await Set<T>().FindAsync(id);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MethodInfo entityMethod = typeof (DbModelBuilder).GetMethod("Entity");

            IEnumerable<Type> entityTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof (IViewModel).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface);

            foreach (Type type in entityTypes)
            {
                try
                {
                    entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
                }
                catch (Exception ex)
                {
                    
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}