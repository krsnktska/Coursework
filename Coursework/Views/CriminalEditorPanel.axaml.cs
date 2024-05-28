using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Coursework.Models;

namespace Coursework.Views;

public partial class CriminalEditorPanel : UserControl
{
    public Criminal Criminal;
    public CriminalEditorPanel(Criminal criminal)
    {
        InitializeComponent();
        
        this.Criminal = criminal;

        if (criminal.ImagePath != "")
        {
            Image.Source =  new Bitmap(AssetLoader.Open(new Uri(criminal.ImagePath)));   
        }
        Name.Text = criminal.Name;
        Nick.Text = criminal.Nickname;
        Height.Value = (decimal)criminal.Height;
        Profession.Text = criminal.Profession;
        LastCrime.Text = criminal.LastCrime;
        SpecialChars.Text = criminal.SpecialChars;
        KnownLanguages.Text = criminal.KnownLanguages;
        Citizen.Text = criminal.Citizen;
        Birthday.SelectedDate = Criminal.Birthday;
        IsArchived.IsChecked = criminal.IsArchived;
    }
}