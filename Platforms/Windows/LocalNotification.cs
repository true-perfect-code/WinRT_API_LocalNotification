// File: Platforms/Windows/LocalNotification.cs
// Purpose: Planbare lokale Toast‑Benachrichtigung ohne Toolkit


using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;


namespace WinRT_API_LocalNotification.Platforms.Windows
{
    public static class LocalNotification
    {
        public static void Schedule(string title, string message, DateTime scheduledTime)
        {
            try
            {
                if (scheduledTime.ToUniversalTime() <= DateTime.UtcNow)
                    throw new ArgumentException("scheduledTime muss in der Zukunft liegen.", nameof(scheduledTime));


                string toastXmlString =
$@"<toast>
<visual>
<binding template='ToastGeneric'>
<text>{EscapeXml(title)}</text>
<text>{EscapeXml(message)}</text>
</binding>
</visual>
</toast>";


                var toastXml = new XmlDocument();
                toastXml.LoadXml(toastXmlString);


                var scheduled = new ScheduledToastNotification(toastXml, new DateTimeOffset(scheduledTime))
                {
                    // Optional: Group/Tag setzen, um später gezielt zu löschen
                    Tag = Guid.NewGuid().ToString("N"),
                    Group = "pFemme"
                };


                //var notifier = ToastCompat.CreateToastNotifier();
                var notifier = ToastNotificationManager.CreateToastNotifier();
                notifier.AddToSchedule(scheduled);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private static string EscapeXml(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return System.Security.SecurityElement.Escape(input) ?? string.Empty;
        }
    }
}
