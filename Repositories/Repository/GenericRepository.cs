﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.DataAccess;
using Repositories.Repository.Interface;
using Repositories.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repositories.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BLW_FOR_KIDContext dbContext;
        protected readonly DbSet<T> dbSet;

        public GenericRepository(BLW_FOR_KIDContext dbContext)
        {
            if (this.dbContext == null)
            {
                this.dbContext = dbContext;
            }
            dbSet = this.dbContext.Set<T>();
        }

        public async Task<bool> Add(T item)
        {
            try
            {
                dbSet.Add(item);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(object id)
        {
            try
            {
                var item = dbSet.Find(id);
                if (item != null)
                {
                    dbSet.Remove(item);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> Get(object id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<T> Get(object id1, object id2)
        {
            try
            {
                return await dbSet.FindAsync(id1, id2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> GetAll(Func<T, bool> where, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = dbSet;
                foreach (var include in includes)
                {
                    query.Include(include);
                }
                var result = query.Where(where).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(object id, T item)
        {
            try
            {
                var check = dbSet.Find(id);
                if (check != null)
                {
                    dbContext.Entry(check).State = EntityState.Detached;
                    dbSet.Update(item);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SqlDataReader GetDataReader(string procName, SqlParameter[] parameters)
        {
            try
            {
                SqlConnection connection = new SqlConnection(GetConnectionString.ConnectionString());

                var command = new SqlCommand(procName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddRange(parameters);
                }
                connection.Open();
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
