using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Coursework.Models;

namespace Coursework.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public Database Database { get; set; } = new();
    
    public bool SaveDatabase(string path)
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
        
            string jsonString = JsonSerializer.Serialize(this.Database, options);
            File.WriteAllText(path, jsonString); 
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public bool LoadDatabase(string path)
    {
        this.Database = null;
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
            this.Database = JsonSerializer.Deserialize<Database>(jsonString, options);
        }
        catch (Exception e)
        {
            this.Database = new Database();
            return false;
        }
        
        if (this.Database == null)
        {
            this.Database = new Database();
            return false;
        }
        
        return true;
    }
}