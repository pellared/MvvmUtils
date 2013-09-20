namespace Pellared.Utils.Mvvm.Services.Modal
{
    public interface IDialogViewModel
    {
        bool Closed { get; set; }
        string Title { get; }
    }
}