namespace Pellared.Utils.Mvvm.Dialog
{
    public interface IDialogViewModel
    {
        bool Closed { get; set; }
        string Title { get; }
    }
}