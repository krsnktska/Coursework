using System.Globalization;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Coursework.Models;

namespace Coursework.Views;

public partial class CriminalReviewPanel : UserControl
{
    
    public Criminal Criminal;
    public CriminalsManager Parent;
    
    public CriminalReviewPanel(Criminal criminal, CriminalsManager parent)
    {
        InitializeComponent();
        
        this.Criminal = criminal;
        this.Parent = parent;

        if (criminal.ImagePath != "")
        {
            FileStream? fileStream = null;
            try
            { 
                fileStream = File.OpenRead(criminal.ImagePath);
            }
            catch
            {
                App.Window.Notify("Error", $"Can't load image file", "critical", 2500);
            }

            if (fileStream != null)
            {
                Image.Source = Bitmap.DecodeToWidth(fileStream, 150);
            }
        }
        else
        {
            // Image.Source =  new Bitmap(AssetLoader.Open(new Uri("/Assets/")));   
        }
        Name.Text = "Name: " + criminal.Name;
        Nick.Text = "Nickname: " + criminal.Nickname;
        Height.Text = "Height: " + criminal.Height.ToString();
        Profession.Text = "Profession: " + criminal.Profession;
        LastCrime.Text = "Last crime: " + criminal.LastCrime;
        SpecialChars.Text = "Specials: " + criminal.SpecialChars;
        KnownLanguages.Text = "Known languages: " + criminal.KnownLanguages;
        Citizenship.Text = criminal.Citizenship;
        Birthday.Text = "Birthday: " + Criminal.Birthday.ToString("d MMMM, yyyy");
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