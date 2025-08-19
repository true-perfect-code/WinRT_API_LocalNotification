WinRT_API_LocalNotification (MAUI Windows)

📢 Scheduled local Windows toast notifications for .NET MAUI Apps – without external NuGet packages such as Microsoft.Toolkit.Uwp.Notifications.

This sample demonstrates how to use the WinRT API (Windows.UI.Notifications) in a .NET MAUI Windows project to schedule local toast notifications (using ScheduledToastNotification).

✨ Features

🔔 Local toast notifications via pure WinRT API

⏰ Scheduled notifications (e.g., “in 1 minute”)

✅ No dependency on Microsoft.WindowsAppSDK or CommunityToolkit

📝 Simple input form inside the app → message is scheduled as toast

🚀 Works in Debug & Release mode (requires MSIX packaging)

📷 Screenshots

(Add screenshots here if available, e.g., app UI and resulting toast notification)

🛠️ Requirements

Windows 10 or Windows 11

Visual Studio 2022 with .NET MAUI workload installed

Enable MSIX Packaging:

Right-click project → Properties → Windows →

Check ✅ “Create App Packages (MSIX)”

Toasts will not show in Debug mode without MSIX packaging

📂 Project Structure
Platforms/Windows/LocalNotification.cs   // Core logic for scheduled toasts
MainPage.xaml                            // UI with input field & button
MainPage.xaml.cs                         // Calls LocalNotification.Schedule()

⚡ Usage Example
// Schedule a notification for 1 minute from now
WinRT_API_LocalNotification.Platforms.Windows.LocalNotification.Schedule(
    "Reminder",
    "Time to drink some water 💧",
    DateTime.Now.AddMinutes(1));

🚀 How to Run

Clone this repository

Open the solution in Visual Studio 2022

Set target to Windows

Make sure MSIX Packaging is enabled

Run the app → enter a message → click Send → close the app →
A toast will appear at the scheduled time 🎉

📌 Notes

ScheduledToastNotification requires the app to be packaged as MSIX.

Notifications use the AppUserModelID automatically assigned by the MSIX package.

No activator is required since this sample only schedules simple local toasts.
