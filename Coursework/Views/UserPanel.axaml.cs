using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Coursework.Models;

namespace Coursework.Views;

public partial class UserPanel : UserControl
{
    public object? PrevView;
    
    public UserPanel(object prevView)
    {
        InitializeComponent();
        this.PrevView = prevView;
    }

    private void ManageCriminals_OnClick(object? sender, RoutedEventArgs e)
    {
        App.Window.ViewPlacer.Content = new CriminalsManager(this);
    }
}