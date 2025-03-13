using GS.Core;
using GS.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GS.Data
{
    /// <summary>
    /// Represents base object context
    /// </summary>
    public partial class GSObjectContext : DbContext, IDbContext
    {
        #region Ctor

        public GSObjectContext(DbContextOptions<GSObjectContext> options) : base(options)
        {
        }

        #endregion
        #region Utilities

        /// <summary>
        /// Further configuration the model
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //dynamically load all entity and query type configurations
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                    && (type.BaseType.GetGenericTypeDefinition() == typeof(GSEntityTypeConfiguration<>)
                        || type.BaseType.GetGenericTypeDefinition() == typeof(GSQueryTypeConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(modelBuilder);
            }
            //thiet dat query cho toan bo BaseReportEntity
            foreach (Type type in
            Assembly.GetAssembly(typeof(BaseViewEntity)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(BaseViewEntity))))
            {
                modelBuilder.Query(type);
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Modify the input SQL query by adding passed parameters
        /// </summary>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>Modified raw SQL query</returns>
        protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            //add parameters to sql
            if (this.Database.IsSqlServer())
            {
                for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
                {
                    if (!(parameters[i] is DbParameter parameter))
                        continue;

                    sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";

                    //whether parameter is output
                    if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                        sql = $"{sql} output";
                }
            }
            else
            {
                for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
                {
                    if (!(parameters[i] is DbParameter parameter))
                        continue;

                    sql = $"{sql}{(i > 0 ? "," : "(")} :{parameter.ParameterName}";
                }
                sql = $"BEGIN {sql}); END;";
            }
            
            return sql;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        public virtual new DbSet<TEntity> SetClass<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        /// <summary>
        /// Generate a script to create all tables for the current model
        /// </summary>
        /// <returns>A SQL script</returns>
        public virtual string GenerateCreateScript()
        {
            return this.Database.GenerateCreateScript();
        }

        /// <summary>
        /// Creates a LINQ query for the query type based on a raw SQL query
        /// </summary>
        /// <typeparam name="TQuery">Query type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <returns>An IQueryable representing the raw SQL query</returns>
        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class
        {
            return this.Query<TQuery>().FromSql(sql);
        }
        public virtual IQueryable<TQuery> QueryFromSqlWithparameters<TQuery>(string sql, params object[] parameters) where TQuery : class
        {
            return this.Query<TQuery>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        }

        /// <summary>
        /// Creates a LINQ query for the entity based on a raw SQL query
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>An IQueryable representing the raw SQL query</returns>
        public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity
        {
            return this.Set<TEntity>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        }
        public virtual IQueryable<TEntity> EntityFromSqlNoParams<TEntity>(string sql) where TEntity : BaseEntity
        {
            return this.Set<TEntity>().FromSql(sql);    
        }
        public virtual IQueryable<TEntity> ViewEntityFromSqlNoParams<TEntity>(string sql) where TEntity : BaseViewEntity
        {
            return this.Query<TEntity>().FromSql(sql);    
        }
        public virtual IQueryable<TEntity> EntityFromSqlNoParams2<TEntity>(string sql) where TEntity : class
        {
            return this.SetClass<TEntity>().FromSql(sql);    
        }
        public virtual IQueryable<TEntity> EntityFromStore1<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity
        {
            // trong truong hop su dung oracle, luon su dung bien sys_refcursor de out thong tin
            if (this.Database.IsOracle() && parameters == null
                || parameters.Where(c => c is DbParameter parameter && (parameter.Direction == ParameterDirection.Output
                || parameter.Direction == ParameterDirection.InputOutput)).Count() == 0)
            {
                var outpara = new Oracle.ManagedDataAccess.Client.OracleParameter("TBL_OUT", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                parameters = new object[] { outpara };
            }
            return this.Set<TEntity>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        }
        public virtual IQueryable<TEntity> EntityViewFromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseViewEntity
        {
            // trong truong hop su dung oracle, luon su dung bien sys_refcursor de out thong tin
            if (this.Database.IsOracle() && parameters == null || parameters.Length == 0)
            {
                var outpara = new Oracle.ManagedDataAccess.Client.OracleParameter("TBL_OUT", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                parameters = new object[] { outpara };
            }
            return this.Query<TEntity>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        }
        public virtual IQueryable<TEntity> EntityViewFromSql2<TEntity>(string sql, params object[] parameters) where TEntity : BaseViewEntity
        {
            // trong truong hop su dung oracle, luon su dung bien sys_refcursor de out thong tin
            if (this.Database.IsOracle() && parameters == null || parameters.Length == 0)
            {
                var outpara = new Oracle.ManagedDataAccess.Client.OracleParameter("TBL_OUT", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                parameters = new object[] { outpara };
            }
            return this.Query<TEntity>().FromSql(sql, parameters);
        }
        public virtual IQueryable<TEntity> EntityFromStore<TEntity>(string sql, params object[] parameters) where TEntity : BaseViewEntity
        {
            // trong truong hop su dung oracle, luon su dung bien sys_refcursor de out thong tin
            if (this.Database.IsOracle() && parameters == null
                || parameters.Where(c => c is DbParameter parameter && (parameter.Direction == ParameterDirection.Output
                || parameter.Direction == ParameterDirection.InputOutput)).Count() == 0)
            {
                var outpara = new Oracle.ManagedDataAccess.Client.OracleParameter("TBL_OUT", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                parameters = new object[] { outpara };
            }

            return this.Query<TEntity>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        }
        /// <summary>
        /// Executes the given SQL against the database
        /// </summary>
        /// <param name="sql">The SQL to execute</param>
        /// <param name="doNotEnsureTransaction">true - the transaction creation is not ensured; false - the transaction creation is ensured.</param>
        /// <param name="timeout">The timeout to use for command. Note that the command timeout is distinct from the connection timeout, which is commonly set on the database connection string</param>
        /// <param name="parameters">Parameters to use with the SQL</param>
        /// <returns>The number of rows affected</returns>
        public virtual int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            //set specific command timeout
            var previousTimeout = this.Database.GetCommandTimeout();
            this.Database.SetCommandTimeout(timeout);
            //var con =this.Database.GetDbConnection();
            //OracleCommand oracleCommand = new OracleCommand("", con);
            var result = 0;
            if (!doNotEnsureTransaction)
            {
                //use with transaction
                using (var transaction = this.Database.BeginTransaction())
                {
                    result = this.Database.ExecuteSqlCommand(sql, parameters);
                    transaction.Commit();
                }
            }
            else
                result = this.Database.ExecuteSqlCommand(sql, parameters);

            //return previous timeout back
            this.Database.SetCommandTimeout(previousTimeout);

            return result;
        }
        public async Task DoWork(RawSqlString sql, params object[] parameters)
        {
            await this.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        /// <summary>
        /// Detach an entity from the context
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        public virtual void Detach<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityEntry = this.Entry(entity);
            if (entityEntry == null)
                return;

            //set the entity is not being tracked by the context
            entityEntry.State = EntityState.Detached;
        }
        public virtual string ExecuteScalarSql(String sql, params object[] parameters)
        {
            this.Database.OpenConnection();
            DbCommand command = new OracleCommand();
            command.CommandType = CommandType.StoredProcedure;
            //command.CommandType = CommandType.Text;
            command.CommandText = sql;
            command.Connection = this.Database.GetDbConnection();
            if (parameters != null && parameters.Count() > 0)
                command.Parameters.AddRange(parameters);
            var rs = command.ExecuteScalar();
            this.Database.CloseConnection();
            command.Dispose();
            //trả về kết quả
            if (rs == null)
                return "";
            else
                return rs.ToString();
        }

        #region anhnt
        public virtual List<T> getTable<T>(string Oracle) where T : new()
        {
            this.Database.OpenConnection();
            DbCommand command = new OracleCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Oracle;
            command.Connection = this.Database.GetDbConnection();

            this.Database.CloseConnection();
            command.Dispose();
            return new List<T>();

            //DataTable tb = new DataTable();
            //OracleDataAdapter adapter = new OracleDataAdapter(Oracle, con);
            //adapter.Fill(tb);
            //// giải phóng tài nguyên
            //con.Close();
            //adapter.Dispose();
            //return CreateListFromTable<T>(tb);
        }
        public static List<T> CreateListFromTable<T>(DataTable tbl) where T : new()
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each row
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(CreateItemFromRow<T>(r));
            }

            // return the list
            return lst;
        }

        // function that creates an object from the given data row
        public static T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }

        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }
        #endregion

        #region Hungnt
        public virtual int CountAmountFromSqlNoParams(string sql) 
        {
            return 1;
        }
        #endregion
        #endregion
    }
}