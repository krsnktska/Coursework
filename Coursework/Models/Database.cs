using System.Collections.Generic;

namespace Coursework.Models;

public class Database
{
    public List<Criminal> CriminalList { get; set; } = [];
    public List<User> UsersList { get; set; } = [];

    public User? SearchForUser(string username)
    {
        foreach (var user in UsersList)
        {
            if (user.Username == username) return user;
        }

        return null;
    }
}

