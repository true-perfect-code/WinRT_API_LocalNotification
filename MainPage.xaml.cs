namespace WinRT_API_LocalNotification
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        //private void OnCounterClicked(object? sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
        private void OnNotificationClicked(object? sender, EventArgs e)
        {
#if WINDOWS
            WinRT_API_LocalNotification.Platforms.Windows.LocalNotification.Schedule(
                "Reminder",
                NotesEditor.Text,
                DateTime.Now.AddMinutes(1));
#endif
            NotesEditor.IsVisible = false;
            LabelEditor.FontSize = 20;
            LabelEditor.TextColor = Colors.Green;
            LabelEditor.Text = "Notification scheduled for 1 minute from now. Please close now the app.";
            NotificationBtn.IsVisible = false;
        }

        //        protected override void OnAppearing()
        //        {
        //            base.OnAppearing();
        //#if WINDOWS
        //            WinRT_API_LocalNotification.Platforms.Windows.LocalNotification.Schedule(
        //                "Reminder",
        //                "Time for a coffee break!",
        //                DateTime.Now.AddMinutes(1));
        //#endif
        //        }
    }
}
