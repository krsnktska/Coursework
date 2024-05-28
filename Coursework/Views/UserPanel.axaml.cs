using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coursework.Models;

namespace Coursework.Views;

public partial class UserPanel : UserControl
{
    public User LoggedUser { get; set; }
    public object PrevView;
    
    public UserPanel(object prevView, User user)
    {
        InitializeComponent();
        this.LoggedUser = user;
        this.PrevView = prevView;
    }
}