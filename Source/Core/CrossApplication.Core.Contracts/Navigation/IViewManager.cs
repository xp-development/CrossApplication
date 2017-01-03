namespace CrossApplication.Core.Contracts.Navigation
{
    public interface IViewManager
    {
        ViewItem RichViewItem { get; set; }
        ViewItem LoginViewItem { get; set; }

        void AddViewItem(ViewItem viewItem);
        ViewItem GetViewItem(string viewKey);
    }
}