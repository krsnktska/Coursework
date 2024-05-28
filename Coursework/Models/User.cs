namespace Coursework.Models;

public class User
{
    public string Username { get; set; } = "User";
    public string Password { get; set; } = "password123";
    public AccessRight UsersAccessRights { get; set; } = AccessRight.Assistant;
}