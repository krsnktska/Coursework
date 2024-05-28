using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Coursework.Models;
using Coursework.ViewModels;
using DynamicData;

namespace Coursework.Views;

public partial class MainWindow : Window
{
    public int RunningNotifyID = 0;
    public MainWindowViewModel ViewModel = new();
    public Dictionary<Key, bool> PressedKeys = new();
    
    public MainWindow()
    {
        InitializeComponent();

        this.ViewModel.Database = this.LoadDatabase("database.database");
        
        ViewPlacer.Content = new LoginScreen();
        // ViewPlacer.Content = new AdminPanel();
            
        this.KeyDown += OnKeyDown;
        this.KeyUp += OnKeyUp;
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
    
    public bool SaveDatabase(Database database, string path)
    {
        try
        {
            var options =  new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, 
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower) },
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
        
            string jsonString = JsonSerializer.Serialize(database, options);
            File.WriteAllText(path, jsonString); 
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public Database LoadDatabase(string path)
    {
        Database? database = null;
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower) },
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            
            string jsonString = File.ReadAllText(path);
            database = JsonSerializer.Deserialize<Database>(jsonString, options);
        }
        catch (Exception e)
        {
        }

        if (database == null)
        {
            this.Notify("Can't load database", "Creating new one", "critical", 2500);
            return new Database();
        }
        
        this.Notify("Database loaded", "Loaded successfully", "ok", 2500);
        return database;
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
            }
            
        }
        
        if (leftCtrl && s)
        {
            Save();
        }
    }

    public void Save()
    {
        if (this.SaveDatabase(this.ViewModel.Database, "database.database"))
        {
            this.Notify("Saved", "Database saved", "ok", 2000);
        }
        else
        {
            this.Notify("Can't save", "Some error occured", "critical", 2000);
        }
    }
}