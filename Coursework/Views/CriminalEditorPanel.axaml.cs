using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Coursework.Models;

namespace Coursework.Views;

public partial class CriminalEditorPanel : UserControl
{
    public Criminal Criminal;
    public CriminalsManager Parent;
    
    public CriminalEditorPanel(Criminal criminal, CriminalsManager parent)
    {
        InitializeComponent();
        
        this.Criminal = criminal;
        this.Parent = parent;

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

        EditLevel.Maximum = (decimal)App.Window.LoggedUser.UsersAccessRights;
        EditLevel.Minimum = 0;
        EditLevel.Value = (decimal)this.Criminal.EditRight;
        
        ReviewLevel.Maximum = (decimal)App.Window.LoggedUser.UsersAccessRights;
        ReviewLevel.Minimum = 0;
        ReviewLevel.Value = (decimal)this.Criminal.ReviewRight;
    }

    private void Apply_OnClick(object? sender, RoutedEventArgs e)
    {
        Criminal.Name = Name.Text ?? "";
        Criminal.Nickname = Nick.Text ?? "";
        Criminal.Height = Decimal.ToDouble(Height.Value ?? 160) ;
        Criminal.Profession = Profession.Text ?? "";
        Criminal.LastCrime = LastCrime.Text ?? "";
        Criminal.SpecialChars = SpecialChars.Text ?? "";
        Criminal.KnownLanguages = KnownLanguages.Text ?? "";
        Criminal.Citizen = Citizen.Text ?? "";
        Criminal.Birthday = (Birthday.SelectedDate ?? default).DateTime;
        Criminal.IsArchived = IsArchived.IsChecked ?? false;

        Criminal.EditRight = (AccessRight)(EditLevel.Value ?? (int)Criminal.EditRight);
        Criminal.ReviewRight = (AccessRight)(ReviewLevel.Value ?? (int)Criminal.ReviewRight);
        
        this.Parent.CriminalEditPlaceHolder.Children.Clear();
        this.Parent.CriminalEditPlaceHolder.Children.Add(new CriminaReviewPanel(Criminal, this.Parent));
        
        this.Parent.UpdateList("");
    }
}