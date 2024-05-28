using System;
using System.Collections.Generic;
using System.IO;

namespace Coursework.Models;

public class Criminal
{
    public string Name { get; set; } = "Name Surname";
    public string Nickname { get; set; } = "Nickname";
    public string ImagePath { get; set; } = "";
    public double Height { get; set; } = 160;
    public string Profession { get; set; } = "Thief";
    public string LastCrime { get; set; } = "None";
    public List<string> SpecialChars { get; set; } = [];
    public List<string> KnownLanguages { get; set; } = [];
    public string Citizen { get; set; } = "Russia";
    public double[] Location { get; set; } = [6105178.323194, 8688138.383006];
    public DateTime Birthday { get; set; } = DateTime.UnixEpoch;
    public AccessRight ReviewRight = AccessRight.Assistant;
    public AccessRight EditRight = AccessRight.Assistant;
    public bool IsArchived { get; set; } = false;
}