using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;

namespace Coursework.Views;

public partial class MainWindow : Window
{

    public int RunningNotifyID = 0;
    
    public MainWindow()
    {
        InitializeComponent();
        
        ViewPlacer.Content = new LoginScreen();
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
}