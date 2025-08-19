# WinRT\_API\_LocalNotification (MAUI Windows)

**Scheduled local Windows toast notifications** for .NET MAUI apps using the **pure WinRT API** (`Windows.UI.Notifications`) — **no external NuGet packages** required.

This sample shows how to build and schedule toast notifications (via `ScheduledToastNotification`) from a MAUI Windows app. It uses simple toast XML, works with MSIX packaging, and keeps the code minimal and easy to follow.

---

## ✨ Features

* 🔔 Local toast notifications via **WinRT**
* ⏰ **Scheduled notifications** (e.g., "in 1 minute")
* 🧩 No dependency on *Microsoft.WindowsAppSDK* or *CommunityToolkit*
* 📝 Simple UI to type a message and schedule the toast
* 🧪 Debug-friendly when **MSIX packaging** is enabled

---

## 🛠 Requirements

* Windows 10 or 11
* Visual Studio 2022 with **.NET MAUI** workload installed
* **MSIX packaging enabled** (required for toasts to appear in Debug)

> **Why MSIX?** Windows associates toast permissions with an App Identity. MSIX provides that identity automatically.

---

## ⚡ Quick Start

1. **Clone** the repository and open the solution in Visual Studio 2022.
2. Target **Windows**.
3. **Enable MSIX packaging** for the Windows head project:

   * *Project* → **Properties** → **Windows** → check ✅ **Create App Packages (MSIX)**
     *(On some VS versions this is labeled "Package using MSIX".)*
4. **Build & Run** the app.
5. Type a message, click **Send**, then close the app. The toast will appear at the scheduled time.

---

## 📂 Project Structure

```
Platforms/Windows/LocalNotification.cs   # Core logic for scheduled toasts via WinRT
MainPage.xaml                            # UI with input field & button
MainPage.xaml.cs                         # Calls LocalNotification.Schedule(...)
```

---

## 🧩 Usage Example

```csharp
// Schedule a notification for 1 minute from now
WinRT_API_LocalNotification.Platforms.Windows.LocalNotification.Schedule(
    "Reminder",
    "Time for a coffee",
    DateTime.Now.AddMinutes(1));
```

---

## 🧰 Troubleshooting

* **No toast shows in Debug**
  Ensure **MSIX packaging is enabled** (see *Quick Start*, step 3). Without package identity the notifier may fail silently.

* **Nothing appears at the scheduled time**
  Verify the scheduled time is **in the future** (system clock, time zones). The code checks this and throws if it isn't.

* **Focus Assist / Do Not Disturb**
  If Windows Focus Assist (Do Not Disturb) is on, toasts may be suppressed.

* **Second run crash (previous experiments)**
  Only create notifiers and schedule toasts **after** the app is launched (e.g., in `OnLaunched` or `MainPage.OnAppearing`). Avoid doing this in the app constructor.

---

## 🚫 Limitations (by design)

* This sample focuses on **scheduled** local toasts with minimal plumbing.
* **No click/activation handling** out of the box. Add a COM activator + manifest if you need to process user actions from the toast.
* For **unpackaged** desktop scenarios you would need additional AppUserModelID shortcut registration; this sample favors the simpler MSIX path.

---

## 📜 License

MIT — see `LICENSE` file if provided.

---

## 🙌 Why this project exists

`Microsoft.Windows.AppNotifications` (Windows App SDK) does not support **scheduled** toasts. This repo demonstrates a practical, lightweight approach using **WinRT** directly from a MAUI Windows app.
