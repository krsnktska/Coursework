using Avalonia.Controls;
using Avalonia.Interactivity;
using Coursework.Models;

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
        LogIn();
    }

    public void LogIn()
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
        
        bool success = false;
        string message, messageType;
        
        var user = App.Window.ViewModel.Database.SearchForUser(username);
        if (user != null)
        {
            success = user.Password == password;
            if (success)
            {
                if (user.UsersAccessRights == AccessRight.Administrator)
                {
                    App.Window.ViewPlacer.Content = new AdminPanel(this);
                    
                }
                else
                {
                    App.Window.ViewPlacer.Content = new UserPanel(this);
                }

                App.Window.LoggedUser = user;
                message = $"Hello, {username}!";
                messageType = "ok";
            }
            else
            {
                message = "Wrong password";
                messageType = "critical";
            }
        }
        else
        {
            message = $"Wrong username";
            messageType = "warn";
        }
        
        App.Window.Notify(message, success ? "Successfully logged in" : "Try again", messageType, 2500);
    }
}