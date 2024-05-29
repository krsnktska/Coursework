using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Coursework.Models;

namespace Coursework.Views;

public partial class CriminaReviewPanel : UserControl
{
    
    public Criminal Criminal;
    public CriminalsManager Parent;
    
    public CriminaReviewPanel(Criminal criminal, CriminalsManager parent)
    {
        InitializeComponent();
        
        this.Criminal = criminal;
        this.Parent = parent;

        if (criminal.ImagePath != "")
        {
            Image.Source =  new Bitmap(AssetLoader.Open(new Uri(criminal.ImagePath)));   
        }
        Name.Text = "Name: " + criminal.Name;
        Nick.Text = "Nickname: " + criminal.Nickname;
        Height.Text = "Height: " + criminal.Height.ToString();
        Profession.Text = "Profession: " + criminal.Profession;
        LastCrime.Text = "Last crime: " + criminal.LastCrime;
        SpecialChars.Text = "Specials: " + criminal.SpecialChars;
        KnownLanguages.Text = "Known languages: " + criminal.KnownLanguages;
        Citizen.Text = criminal.Citizen;
        Birthday.Text = "Birthday: " + Criminal.Birthday.ToString("MMMM d, yyyy");
        IsArchived.Text = criminal.IsArchived ? "Archived" : "Not Archived";
    }

    private void Edit_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.Window.LoggedUser.UsersAccessRights < this.Criminal.EditRight)
        {
            App.Window.Notify("Weak permission", $"{this.Criminal.EditRight} rights needed", "critical", 2500);
            return;
        }
        
        Parent.CriminalEditPlaceHolder.Children.Clear();
        Parent.CriminalEditPlaceHolder.Children.Add(new CriminalEditorPanel(this.Criminal, this.Parent));
    }
}