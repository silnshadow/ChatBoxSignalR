namespace ChatBoxClient.Services
{
    public interface IDialogService
    {
        string OpenFile(string caption, string filter = "All files (*.*)|*.*");
        bool ShowConfirmationRequest(string message, string caption = "");
        void ShowNotification(string message, string caption = "");
    }
}