namespace CrossApplication.Core.Contracts.Navigation
{
    public interface IViewManager
    {
        ViewItem LoginViewItem { get; set; }

        void AddViewItem(ViewItem viewItem);
        ViewItem GetViewItem(string viewKey);
        void RegisterRichShell(string name, ViewItem viewItem);
        ViewItem GetRichShell(string name);
    }
}