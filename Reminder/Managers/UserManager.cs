using Reminder.Abstractions;
using Reminder.Auxiliary;
using Reminder.DataAccessLayer.DAO;
using Reminder.Models;
using SDK.Base.Abstractions;
using System.Collections.ObjectModel;

namespace Reminder.Managers
{
    public class UserManager : IDataManager<User>
    {
        #region Private property

        /// <summary>
        /// Company data provider
        /// </summary>
        private readonly IDataProvider<UserDAO> _dataProvider;

        #endregion
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dataProvider"></param>
        public UserManager(IDataProvider<UserDAO> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        /// <inheritdoc/>
        public ObservableCollection<User> Items { get; set; } = new();

        public Task<int> CreateAsync(User item)
        {
            int? id;

            if (Items.Count <= 0)
                id = 1;
            else
                id = Items.Max(x => x.Id) + 1;

            item.Id = id;

            Items.Add(item);
            return _dataProvider.CreateAsync(item.ToDAO());
        }

        /// <inheritdoc/>
        public Task<int> DeleteAllAsync()
        {
            Items.Clear();
            return _dataProvider.DeleteAllAsync();
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(User item)
        {
            Items.Remove(item);
            return _dataProvider.DeleteAsync(item.ToDAO());
        }

        /// <inheritdoc/>
        public Task<List<User?>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task ReadAllUsersAsync()
        {
            var items = await _dataProvider.ReadAllAsync();

            Items = new ObservableCollection<User>(items.Select(x => x.ToModel()));
        }

        /// <inheritdoc/>
        public Task<User?> ReadAsync(int id)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);
        }

        /// <inheritdoc/>
        public Task<int> UpdateAsync(User item)
        {
            var user = Items.FirstOrDefault(x => x.Id == item.Id);

            if (user == null)
                return Task.FromResult(0);

            user.Id = item.Id;
            user.Name = item.Name;
            user.LastName = item.LastName;
            user.MiddleName = item.MiddleName;
            user.Position = item.Position;
            user.Birthday = item.Birthday;
            user.Avatar = item.Avatar;

            return _dataProvider.UpdateAsync(item.ToDAO());
        }
    }
}
