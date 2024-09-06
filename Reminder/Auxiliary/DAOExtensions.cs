using Reminder.DataAccessLayer.DAO;
using Reminder.Models;

namespace Reminder.Auxiliary
{
    /// <summary>
    /// Data Access Object extensions
    /// </summary>
    public static class DAOExtensions
    {
        /// <summary>
        /// Transforms the UserDAO into a model
        /// </summary>
        /// <param name="dao"></param>
        /// <returns></returns>
        public static User ToModel(this UserDAO? dao) => new()
        {
            Id = dao!.Id,
            Name = dao?.Name,
            Avatar = dao?.Avatar,
            Birthday = dao!.Birthday,
            LastName = dao?.LastName,
            MiddleName = dao?.MiddleName,
            Note = dao?.Note
        };

        /// <summary>
        /// Transforms the user into a DAO
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static UserDAO ToDAO(this User? model) => new()
        {
           Id = model!.Id,
           Name = model?.Name,
           Note = model?.Note,
           Avatar = model?.Avatar,
           Birthday = model!.Birthday,
           MiddleName= model?.MiddleName,
           LastName = model?.LastName
        };
    }
}
