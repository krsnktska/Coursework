using Coursework.Models;

namespace Coursework.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public Database Database { get; set; } = new();
}