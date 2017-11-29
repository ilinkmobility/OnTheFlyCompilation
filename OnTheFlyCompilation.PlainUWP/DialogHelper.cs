using System;
using Windows.UI.Popups;

namespace OnTheFlyCompilation.PlainUWP
{
    public class DialogHelper
    {
        public static async void ShowDialog(string msg)
        {
            var messageDialog = new MessageDialog(msg);
            await messageDialog.ShowAsync();
        }
    }
}
