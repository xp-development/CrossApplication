namespace CrossApplication.Wpf.Contracts.Navigation
{
    public interface IViewManager
    {
        ViewItem LoginViewItem { get; set; }

        void AddViewItem(ViewItem viewItem);
        ViewItem GetViewItem(string viewKey);
    }
}