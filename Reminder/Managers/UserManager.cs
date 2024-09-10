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

        /// <inheritdoc/>
        public async Task<bool> CreateAsync(User item)
        {
            int id;

            if (Items.Count <= 0)
                id = 1;
            else
                id = Items.Max(x => x.Id) + 1;

            item.Id = id;

            var result = await _dataProvider.CreateAsync(item.ToDAO());

            if (!result)
                return false;

            Items.Add(item);

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAllAsync()
        {
            var result = await _dataProvider.DeleteAllAsync();

            if (result)
            {
                Items.Clear();
                return true;
            }

            return false;  
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(User item)
        {
            var result = await _dataProvider.DeleteAsync(item.ToDAO());

            if(result)
            {
                Items.Remove(item);
                return true;
            }

            return false;
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
        public async Task<bool> UpdateAsync(User item)
        {
            var user = Items.FirstOrDefault(x => x.Id == item.Id);

            if (user == null)
                return false;

            var result = await _dataProvider.UpdateAsync(item.ToDAO());

            if(result)
            {
                user.Id = item.Id;
                user.Name = item.Name;
                user.LastName = item.LastName;
                user.MiddleName = item.MiddleName;
                user.Note = item.Note;
                user.Birthday = item.Birthday;
                user.Avatar = item.Avatar;

                return true;
            }

            return false;
        }
    }
}
