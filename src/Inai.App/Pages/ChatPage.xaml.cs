using Inai.App.ViewModels;
using System.Collections;

namespace Inai.App.Pages;

public partial class ChatPage : ContentPage
{
    public ChatPage()
    {
        InitializeComponent();
        BindingContext = new ChatViewModel();

        // Subscribe to collection changes for auto-scroll
        if (BindingContext is ChatViewModel vm)
        {
            vm.Messages.CollectionChanged += async (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    await ScrollToLastMessageAsync();
                }
            };
        }
    }
    private async Task ScrollToLastMessageAsync()
    {
        // Assuming your CollectionView x:Name="MessagesView"
        if (MessagesView.ItemsSource is not null && MessagesView.ItemsSource.Cast<object>().Any())
        {
            await Task.Delay(50); // allow UI to render first
            var lastIndex = ((ICollection)MessagesView.ItemsSource).Count - 1;
            if (lastIndex >= 0)
                MessagesView.ScrollTo(lastIndex, position: ScrollToPosition.End, animate: true);
        }
    }

}
