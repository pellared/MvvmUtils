namespace Pellared.Utils.Mvvm.Services.Dialog
{
    public interface IDialogViewModel
    {
        bool Closed { get; set; }
        string Title { get; }
    }
}