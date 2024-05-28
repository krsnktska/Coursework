using System;
using System.Collections.Generic;
using System.Linq;
using Coursework.Models;

namespace Coursework.ViewModels;

public class UserManagerViewModel
{
    public static List<User> UsersList
    {
        get
        {
            return App.Window.ViewModel.Database.UsersList;
        }
    }
}