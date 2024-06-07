using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Coursework.Models;
using Coursework.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace Coursework.Views;

public partial class MainWindow : Window
{
    public int RunningNotifyID = 0;
    public MainWindowViewModel ViewModel = new();
    public Dictionary<Key, bool> PressedKeys = new();
    public User? LoggedUser { get; set; } = null;
    
    public MainWindow()
    {
        InitializeComponent();

        this.LoadDatabase();
        
        ViewPlacer.Content = new LoginScreen();
            
        this.KeyDown += OnKeyDown;
        this.KeyUp += OnKeyUp;
        this.AutoSave();
        this.Closing += Window_OnClosing;
    }

    private async void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        e.Cancel = true;
        
        var box = MessageBoxManager
            .GetMessageBoxStandard("Save?", "Save before exit?",
                ButtonEnum.YesNoCancel);

        switch (await box.ShowAsPopupAsync(this))
        {
            case ButtonResult.Yes:
                this.SaveDatabase();
                this.Closing -= Window_OnClosing;
                this.Close();
                break;
            case ButtonResult.No:
                this.Closing -= Window_OnClosing;
                this.Close();
                break;
            case ButtonResult.Cancel:
                break;
        }
    }

    public async void Notify(string header, string description, string state, int duration)
    {
        NotifyPlaceholder.Children.Clear();

        var currId = Random.Shared.Next();
        this.RunningNotifyID = currId;
        
        var notify = new Notification();
        notify.NotificationHeader.Text = header;
        notify.NotificationDescription.Text = description;

        string color;
        switch (state)
        {
            case "critical":
                color = "#7A4248";
                break;
            case "warn":
                color = "#5A5633";
                break;
            case "ok":
                color = "#567845";
                break;
            default:
                color = "#734478";
                break;
        }

        var brush = new SolidColorBrush(Avalonia.Media.Color.Parse(color));
        notify.NotificationBorder.BorderBrush = brush;
        notify.NotificationTimeout.Foreground = brush;

        NotifyPlaceholder.Children.Add(notify);

        for (int i = 0; i < 101; i++)
        {
            await Task.Delay(duration / 100);
            notify.NotificationTimeout.Value = i;
        }

        if (RunningNotifyID == currId)
        {
            NotifyPlaceholder.Children.Clear();
        }
    }
    
    private void OnKeyUp(object? sender, KeyEventArgs e)
    {
        this.PressedKeys[e.Key] = false;
    }
    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        this.PressedKeys[e.Key] = true;

        this.PressedKeys.TryGetValue(Key.S, out bool s);
        this.PressedKeys.TryGetValue(Key.LeftCtrl, out bool leftCtrl);
        this.PressedKeys.TryGetValue(Key.Enter, out bool enter);
        this.PressedKeys.TryGetValue(Key.Escape, out bool escape);

        if (enter)
        {
            switch (this.ViewPlacer.Content.GetType().Name)
            {
                case "LoginScreen": 
                    ((LoginScreen)this.ViewPlacer.Content).LogIn();
                    break;
            }
        } 
        
        if (escape)
        {
            switch (this.ViewPlacer.Content.GetType().Name)
            {
                case "AdminPanel":
                    var adminPanel = (AdminPanel)this.ViewPlacer.Content;
                    if (adminPanel.PrevView == null) return;
            
                    this.ViewPlacer.Content = adminPanel.PrevView;
                    break;
                case "UserManager":
                    var userManager = (UserManager)this.ViewPlacer.Content;
                    if (userManager.PrevView == null) return;
            
                    this.ViewPlacer.Content = userManager.PrevView;
                    break;
                case "UserPanel":
                    var userPanel = (UserPanel)this.ViewPlacer.Content;
                    if (userPanel.PrevView == null) return;
            
                    this.ViewPlacer.Content = userPanel.PrevView;
                    break;
                case "CriminalsManager":
                    var criminalsManager = (CriminalsManager)this.ViewPlacer.Content;
                    if (criminalsManager.PrevView == null) return;
            
                    this.ViewPlacer.Content = criminalsManager.PrevView;
                    break;
            }
        }
        
        if (leftCtrl && s)
        {
            SaveDatabase();
        }
    }

    public bool SaveDatabase()
    {
        if (!this.ValidateDatabase()) return false;
        
        if (this.ViewModel.SaveDatabase("database.database"))
        {
            this.Notify("Saved", "Database saved", "ok", 2000);
            return true;
        }
        
        this.Notify("Can't save", "Some error occured", "critical", 2000);
        return false;
    }
    public void LoadDatabase()
    {
        if (this.ViewModel.LoadDatabase("database.database"))
        {
            this.Notify("Database loaded", "Loaded successfully", "ok", 2500);
        }
        else
        {
            this.Notify("Can't load", "Some error occured, creating new database", "critical", 3000);
        }
    }
    public bool ValidateDatabase()
    {
        HashSet<string> names = [];
        
        if (ViewModel.Database.UsersList.Any(user => user.Username == ""))
        {
            Notify("Can't save database", "Username can't be empty", "critical", 2500);
            return false;
        }
        
        if (ViewModel.Database.UsersList.Any(user => user.Username.Trim() != user.Username))
        {
            Notify("Can't save database", "Username can't start or end with \" \"", "critical", 2500);
            return false;
        }
        
        if (ViewModel.Database.UsersList.Any(user => !names.Add(user.Username)))
        {
            Notify("Can't save database", "Users can't have same username", "critical", 2500);
            return false;
        }
        
        if (ViewModel.Database.UsersList.Any(user => user.Password == ""))
        {
            Notify("Can't save database", "Password can't be empty", "critical", 2500);
            return false;
        }
        
        if (ViewModel.Database.UsersList.Any(user => user.Password.Trim() != user.Password))
        {
            Notify("Can't save database", "Password can't start or end with \" \"", "critical", 2500);
            return false;
        }
        
        return true;
    }
    public async void AutoSave()
    {
        await Task.Delay(60 * 1000);
        this.SaveDatabase();
        this.AutoSave();
    }
}