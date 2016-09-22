namespace CrossApplication.Wpf.Contracts.Navigation
{
    public interface IViewManager
    {
        void AddViewItem(ViewItem viewItem);
        ViewItem GetViewItem(string viewKey);
    }
}