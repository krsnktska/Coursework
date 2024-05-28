using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Coursework.Views;

public partial class LoginScreen : UserControl
{
    public LoginScreen()
    {
        InitializeComponent();
    }

    private void ForgotPasswordButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.Window.Notify("Forgot Password", "Ask the administrator for help", "warn", 5000);
    }

    private void LogInButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var username = UsernamePrompt.Text;
        var password = PasswordPrompt.Text;

        if (string.IsNullOrEmpty(username))
        {
           App.Window.Notify("Error", "Please enter username", "critical", 2500);
           return;
        }

        if (string.IsNullOrEmpty(password))
        {
            App.Window.Notify("Error", "Please enter password", "critical", 2500);
            return;
        }
        
        App.Window.Notify($"Hello, {username}!", $"Successfully logged in", "ok", 2500);

    }
}