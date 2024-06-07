using System.Collections.Generic;
using Coursework.Models;

namespace Coursework.ViewModels;

public class UserManagerViewModel
{
    public List<User> UsersList => App.Window.ViewModel.Database.UsersList;
}