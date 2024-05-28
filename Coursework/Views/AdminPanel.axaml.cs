using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Coursework.Views;

public partial class AdminPanel : UserControl
{
    public object? PrevView;
    
    public AdminPanel(object prevView)
    {
        InitializeComponent();
        this.PrevView = prevView;
    }

    private void ManageUsers_OnClick(object? sender, RoutedEventArgs e)
    {
        App.Window.ViewPlacer.Content = new UserManager(this);
    }

    private void ManageCriminals_OnClick(object? sender, RoutedEventArgs e)
    {
        App.Window.ViewPlacer.Content = new CriminalsManager(this);
    }
}