using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Coursework.Models;

public class Database
{
    public List<Criminal> CriminalList { get; set; } = [];
    public List<CriminalGroup> CriminaGrouplList { get; set; } = [];
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