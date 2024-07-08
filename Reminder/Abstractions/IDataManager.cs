using SDK.ViewModels.Abstractions;
using System.Collections.ObjectModel;

namespace Reminder.Abstractions
{
    public interface IDataManager<T> : IBasicCRUD<T>
    {
        /// <summary>
		/// Observable items
		/// </summary>
		ObservableCollection<T> Items { get; set; }

        /// <summary>
        /// Read database
        /// </summary>
        /// <returns></returns>
        Task ReadAllUsersAsync();
    }

}
