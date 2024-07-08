using LiteDB;
using Microsoft.Extensions.Logging;
using Reminder.Auxiliary;
using Reminder.DataAccessLayer.DAO;
using SDK.ViewModels.Abstractions;

namespace Reminder.DataAccessLayer
{
    /// <summary>
    /// Working with the database Users
    /// </summary>
    public class UserLiteDbProvider : IDataProvider<UserDAO>
    {
        #region Private property
        private const string usersDataCollectionName = "reminder";

        /// <summary>
        /// Logger Company LiteDatabase provider
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Connection string 
        /// </summary>
        private readonly string? _connectionString;

        /// <summary>
        /// Lite collection
        /// </summary>
        private ILiteCollection<UserDAO?>? _collection;

        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public UserLiteDbProvider(ILogger<UserLiteDbProvider> logger, DbConnectionOptions options)
        {
            _logger = logger;
            _connectionString = options.ConnectionString;

            Task.Run(InitAsync);

        }

        /// <inheritdoc/>
        public Task<int> CreateAsync(UserDAO item)
        {
            if (_collection is null)
                return Task.FromResult(0);

            try
            {
                _collection.Insert(item);

                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in CreateAsync()");
                return Task.FromResult(0);
            }
        }

        /// <inheritdoc/>
        public Task<int> DeleteAllAsync()
        {
            if (_collection is null)
                return Task.FromResult(0);

            try
            {
                _collection.DeleteAll();

                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAllAsync()");
                return Task.FromResult(0);
            }
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(UserDAO item)
        {
            if (_collection is null)
                return Task.FromResult(0);

            try
            {
                _collection.Delete(item.Id);

                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAsync()");
                return Task.FromResult(0);
            }
        }

        /// <inheritdoc/>
        public Task<List<UserDAO?>> ReadAllAsync()
        {
            if (_collection is null)
                return Task.FromResult(new List<UserDAO?>());

            try
            {
                return Task.FromResult(_collection.FindAll().ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ReadAllAsync()");
                return Task.FromResult(new List<UserDAO?>());
            }

        }

        /// <inheritdoc/>
        public Task<UserDAO?> ReadAsync(int id)
        {
            if (_collection is null)
                return Task.FromResult<UserDAO?>(null);

            try
            {
                return Task.FromResult(_collection.FindById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ReadAsync()");
                return Task.FromResult<UserDAO?>(null);
            }
        }

        /// <inheritdoc/>
        public Task<int> UpdateAsync(UserDAO item)
        {
            if (_collection is null)
                return Task.FromResult(0);

            try
            {
                _collection.Update(item);

                return Task.FromResult(1);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateAsync()");
                return Task.FromResult(0);
            }

        }

        /// <summary>
        /// Initializes the database
        /// </summary>
        /// <returns></returns>
        private void InitAsync()
        {
            try
            {
                var database = new LiteDatabase(_connectionString);
                _collection = database.GetCollection<UserDAO?>(usersDataCollectionName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in InitAsync()");
            }
        }

    }
}
