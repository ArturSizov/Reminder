namespace Reminder.DataAccessLayer.DAO
{
    /// <summary>
    /// User Data Access Object
    /// </summary>
    public class UserDAO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Note { get; set; }
        public string? Avatar { get; set; }
        public DateTime Birthday { get; set; }
    }
}
