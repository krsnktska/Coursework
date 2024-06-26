using System;

namespace Coursework.Models;

public class Criminal
{
    public string Name { get; set; } = "Name Surname";
    public string Nickname { get; set; } = "Nickname";
    public string ImagePath { get; set; } = "";
    public double Height { get; set; } = 160;
    public string Profession { get; set; } = "Thief";
    public string LastCrime { get; set; } = "None";
    public string SpecialChars { get; set; } = "None";
    public string KnownLanguages { get; set; } = "Russian";
    public string Citizenship { get; set; } = "Russia";
    public DateTime Birthday { get; set; } = DateTime.UnixEpoch;
    public AccessRight ReviewRight { get; set; } = AccessRight.Assistant;
    public AccessRight EditRight { get; set; } = AccessRight.Assistant;
    public bool IsArchived { get; set; } = false;

    public override string ToString()
    {
        return this.Name;
    }
}
