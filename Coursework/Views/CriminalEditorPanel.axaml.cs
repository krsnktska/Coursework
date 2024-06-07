using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Coursework.Models;

namespace Coursework.Views;

public partial class CriminalEditorPanel : UserControl
{
    private Criminal Criminal;
    private CriminalsManager ParentView;
    
    public CriminalEditorPanel(Criminal criminal, CriminalsManager parentView)
    {
        InitializeComponent();
        
        this.Criminal = criminal;
        this.ParentView = parentView;

        char[] chars = { '/', '\\' };
        Image.Content = criminal.ImagePath == "" ? "No Image" : criminal.ImagePath.Split(chars)[^1];
        Image.Tag = criminal.ImagePath;
        Name.Text = criminal.Name;
        Nick.Text = criminal.Nickname;
        Height.Value = (decimal)criminal.Height;
        Profession.Text = criminal.Profession;
        LastCrime.Text = criminal.LastCrime;
        SpecialChars.Text = criminal.SpecialChars;
        KnownLanguages.Text = criminal.KnownLanguages;
        Citizen.Text = criminal.Citizenship;
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
        Criminal.ImagePath = (string?)Image.Tag ?? "";
        Criminal.Name = Name.Text ?? "";
        Criminal.Nickname = Nick.Text ?? "";
        Criminal.Height = Decimal.ToDouble(Height.Value ?? 160) ;
        Criminal.Profession = Profession.Text ?? "";
        Criminal.LastCrime = LastCrime.Text ?? "";
        Criminal.SpecialChars = SpecialChars.Text ?? "";
        Criminal.KnownLanguages = KnownLanguages.Text ?? "";
        Criminal.Citizenship = Citizen.Text ?? "";
        Criminal.Birthday = (Birthday.SelectedDate ?? default).DateTime;
        Criminal.IsArchived = IsArchived.IsChecked ?? false;

        Criminal.EditRight = (AccessRight)(EditLevel.Value ?? (int)Criminal.EditRight);
        Criminal.ReviewRight = (AccessRight)(ReviewLevel.Value ?? (int)Criminal.ReviewRight);
        
        this.ParentView.CriminalEditPlaceHolder.Children.Clear();
        this.ParentView.CriminalEditPlaceHolder.Children.Add(new CriminalReviewPanel(Criminal, this.ParentView));
        
        this.ParentView.UpdateList("");
    }

    private async void Image_OnClick(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Open File",
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Pick image", Extensions = new List<string> { "png", "jpg", "webm" } },
            },
            AllowMultiple = false,
        };
        
        var result = await dialog.ShowAsync(App.Window);

        if (result != null && result.Length > 0)
        {
            char[] chars = { '/', '\\' };
            Image.Content = result[0].Split(chars)[^1];
            Image.Tag = result[0];

        }
    }
}