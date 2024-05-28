using System.Collections.Generic;

namespace Coursework.Models;

public class CriminalGroup
{
    public string Name { get; set; } = "Name";
    public string ImagePath { get; set; } = "";
    public string DislocationCountry { get; set; } = "Russia";
    public List<Criminal> Members { get; set; } = [];
    public AccessRight ReviewRight = AccessRight.Assistant;
    public AccessRight EditRight = AccessRight.Assistant;
    public bool IsArchived { get; set; } = false;
}