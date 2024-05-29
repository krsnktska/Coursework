using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Coursework.Models;

namespace Coursework.Views;

public partial class CriminalsManager : UserControl
{
    public object? PrevView;

    public CriminalsManager(object prevView)
    {
        InitializeComponent();

        this.PrevView = prevView;

        UpdateList("");
    }

    public void UpdateList(string filter)
    {
        CriminalsListBox.ItemsSource =
            App.Window.ViewModel.Database.CriminalList.Where(
                criminal =>
                {
                    bool found = false;
                    foreach (var prop in typeof(Criminal).GetProperties())
                    {
                        found = ((string?)prop.GetValue(criminal))?.Contains(filter) ?? false;
                        if (found) break;
                    }

                    return (!criminal.IsArchived || (ShowArchived.IsChecked ?? false))
                           &&
                           criminal.ReviewRight <= App.Window.LoggedUser?.UsersAccessRights
                           &&
                           found;
                }

            ).ToList();
    }

    private void SearchButton_OnClick(object? sender, RoutedEventArgs e)
    {
        UpdateList(Search.Text ?? "");
    }

    private void ShowArchived_OnClick(object? sender, RoutedEventArgs e)
    {
        UpdateList(Search.Text ?? "");
    }

    private void Add_OnClick(object? sender, RoutedEventArgs e)
    {
        App.Window.ViewModel.Database.CriminalList.Add(new Criminal());
        UpdateList(Search.Text ?? "");
    }

    private void Delete_OnClick(object? sender, RoutedEventArgs e)
    {
        var selected = (Criminal?)CriminalsListBox.SelectedItem;
        if (selected == null) return;

        if (App.Window.LoggedUser.UsersAccessRights < selected.EditRight)
        {
            App.Window.Notify("Weak permission", $"{selected.EditRight} rights needed", "critical", 2500);
            return;
        }
        
        App.Window.ViewModel.Database.CriminalList.Remove(selected);
        UpdateList(Search.Text ?? "");
    }

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        App.Window.Save();
    }

    private void CriminalsListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selected = (Criminal?)CriminalsListBox.SelectedItem;
        if (selected == null) return;

        CriminalEditPlaceHolder.Children.Clear();
        CriminalEditPlaceHolder.Children.Add(new CriminaReviewPanel(selected, this));
    }
}