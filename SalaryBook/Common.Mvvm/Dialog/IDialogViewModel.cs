namespace Pellared.Common.Mvvm.Dialog
{
    public interface IDialogViewModel
    {
        bool Closed { get; set; }
        string Title { get; }
    }
}