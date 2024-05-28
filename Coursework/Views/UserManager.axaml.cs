using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Coursework.Models;

namespace Coursework.Views;

public partial class UserManager : UserControl
{
    public object? PrevView;
    
    public UserManager(object prevView)
    {
        InitializeComponent();
        this.PrevView = prevView;
        
        UpdateUsersGrid();
    }

    private void Add_OnClick(object? sender, RoutedEventArgs e)
    {
        App.Window.ViewModel.Database.UsersList.Add(new User());
        UpdateUsersGrid();
    }

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        App.Window.Save();
    }

    private void Delete_OnClick(object? sender, RoutedEventArgs e)
    {
        var seleted = (User?)UsersGrid.SelectedItem;

        if (seleted == null) return;

        App.Window.ViewModel.Database.UsersList.Remove(seleted);
        UpdateUsersGrid();
    }

    private void UpdateUsersGrid()
    {
        UsersGrid.ItemsSource = App.Window.ViewModel.Database.UsersList.ToList();
        UsersGrid.SelectedItem = null;
    }
}