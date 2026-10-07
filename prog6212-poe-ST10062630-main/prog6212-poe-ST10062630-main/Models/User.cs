namespace ST10062630_PROG6212_POE.Models
{
    public class User
    {
        public int UserId { get; set; } // Primary Key
        public string UserName { get; set; }

        // Corrected property name to match the database column
        public int UserRoleId { get; set; }

        // Navigation property for the related Role
        public UserRole Role { get; set; }
    }


    public class UserRole
    {
        public int UserRoleId { get; set; }  // Corrected property name
        public string RoleName { get; set; }
    }


}
