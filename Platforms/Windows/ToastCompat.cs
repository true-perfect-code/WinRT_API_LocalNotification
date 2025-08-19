//// File: Platforms/Windows/ToastCompat.cs
//// Purpose: Minimal Compat-Layer für Windows.UI.Notifications ohne Microsoft.Toolkit.Uwp.Notifications
////          – funktioniert für sowohl verpackte (MSIX) als auch unverpackte Desktop-Apps.
//// Notes (Warum wichtig): Für unverpackte Apps MUSS ein Startmenü‑Shortcut mit AUMID existieren,
////          sonst wirft CreateToastNotifier(..) 0x80070490 (Element not found). Dieses Helper legt
////          den Shortcut an und nutzt die richtige CreateToastNotifier‑Überladung.

//using System.Runtime.InteropServices;
//using System.Text;
//using Windows.UI.Notifications;

//namespace WinRT_API_LocalNotification.Platforms.Windows
//{
//    internal static class ToastCompat
//    {
//        public static ToastNotifier CreateToastNotifier()
//        {
//            // Da wir nur noch verpackte Apps (MSIX) unterstützen,
//            // können wir die Standard-Überladung der API nutzen.
//            // Die AUMID wird automatisch vom Betriebssystem vergeben.
//            return ToastNotificationManager.CreateToastNotifier();
//        }

//        //// TODO: Anpassen – eindeutige AUMID für die unverpackte Variante wählen
//        //private const string Aumid = "pFemme.Desktop";   // Schema: Company.Product

//        //// Name & Speicherort des Verknüpfungsfiles im Startmenü des aktuellen Users
//        //private const string ShortcutName = "pFemme.lnk";

//        //public static ToastNotifier CreateToastNotifier()
//        //{
//        //    if (IsPackaged())
//        //    {
//        //        // Packaged (MSIX): Identität vorhanden, Standard‑Überladung benutzen
//        //        return ToastNotificationManager.CreateToastNotifier();
//        //    }

//        //    // Unverpackt: registriere Startmenü‑Shortcut (AUMID), dann AUMID‑Überladung verwenden
//        //    EnsureStartMenuShortcutWithAumid();
//        //    return ToastNotificationManager.CreateToastNotifier(Aumid);
//        //}

//        ////private static bool IsPackaged()
//        ////{
//        ////    try
//        ////    {
//        ////        var _ = Windows.ApplicationModel.Package.Current; // wirft in unverpackten Apps
//        ////        return true;
//        ////    }
//        ////    catch
//        ////    {
//        ////        return false;
//        ////    }
//        ////}
//        //[DllImport("kernel32.dll", ExactSpelling = true)]
//        //private static extern int GetCurrentPackageFullName(ref int length, StringBuilder buffer);

//        //private static bool IsPackaged()
//        //{
//        //    int length = 0;
//        //    int result = GetCurrentPackageFullName(ref length, null);
//        //    return result != 15700; // APPMODEL_ERROR_NO_PACKAGE
//        //}

//        //private static void EnsureStartMenuShortcutWithAumid()
//        //{
//        //    string startMenu = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
//        //    string shortcutPath = Path.Combine(startMenu, "Programs", ShortcutName);

//        //    try
//        //    {
//        //        Directory.CreateDirectory(Path.GetDirectoryName(shortcutPath)!);

//        //        // (Re)create shortcut on each run – idempotent und robust bei Updates
//        //        CreateShellLinkWithAumid(shortcutPath, Aumid);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // Nur bewusst: Ohne gültigen Shortcut schlägt CreateToastNotifier(AUMID) fehl
//        //        throw new InvalidOperationException(
//        //            $"Startmenü‑Shortcut konnte nicht erstellt werden: {shortcutPath}", ex);
//        //    }
//        //}

//        //private static void CreateShellLinkWithAumid(string shortcutPath, string aumid)
//        //{
//        //    var exePath = GetExecutablePath();

//        //    var shellLink = (IShellLinkW)new CShellLink();
//        //    shellLink.SetPath(exePath);
//        //    shellLink.SetArguments(string.Empty);
//        //    shellLink.SetIconLocation(exePath, 0);

//        //    // Setze AUMID auf dem Shortcut (warum: Windows ordnet Toasts der App über AUMID zu)
//        //    var propStore = (IPropertyStore)shellLink;

//        //    var pvId = PROPVARIANT.FromString(aumid);
//        //    try
//        //    {
//        //        //propStore.SetValue(ref PKEY.AppUserModel_ID, ref pvId);
//        //        var key = PKEY.AppUserModel_ID;
//        //        propStore.SetValue(ref key, ref pvId);
//        //        propStore.Commit();
//        //    }
//        //    finally
//        //    {
//        //        pvId.Clear();
//        //    }

//        //    var persist = (IPersistFile)shellLink;
//        //    persist.Save(shortcutPath, true);
//        //}

//        //private static string GetExecutablePath()
//        //{
//        //    // Für MAUI/WinUI3 ist dies der Prozess‑EXE‑Pfad
//        //    return Environment.ProcessPath
//        //           ?? System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName
//        //           ?? throw new InvalidOperationException("EXE‑Pfad konnte nicht ermittelt werden.");
//        //}

//        //#region COM Interop – Shell Link & Property Store

//        //[ComImport]
//        //[Guid("00021401-0000-0000-C000-000000000046")]
//        //private class CShellLink { }

//        //[ComImport]
//        //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//        //[Guid("000214F9-0000-0000-C000-000000000046")]
//        //private interface IShellLinkW
//        //{
//        //    // Nur benötigte Methoden deklarieren
//        //    int SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
//        //    int SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
//        //    int SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
//        //    // Platzhalter für VTable – nicht verwendet
//        //    // (Die restlichen Methoden müssen nicht deklariert werden, wenn sie nicht aufgerufen werden)
//        //}

//        //[ComImport]
//        //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//        //[Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
//        //private interface IPropertyStore
//        //{
//        //    int GetCount(out uint cProps);
//        //    int GetAt(uint iProp, out PROPERTYKEY pkey);
//        //    int GetValue(ref PROPERTYKEY key, out PROPVARIANT pv);
//        //    int SetValue(ref PROPERTYKEY key, ref PROPVARIANT pv);
//        //    int Commit();
//        //}

//        //[ComImport]
//        //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//        //[Guid("0000010b-0000-0000-C000-000000000046")]
//        //private interface IPersistFile
//        //{
//        //    int GetClassID(out Guid pClassID);
//        //    int IsDirty();
//        //    int Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);
//        //    int Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, bool fRemember);
//        //    int SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
//        //    int GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
//        //}

//        //[StructLayout(LayoutKind.Sequential, Pack = 4)]
//        //private struct PROPERTYKEY
//        //{
//        //    public Guid fmtid;
//        //    public uint pid;
//        //}

//        //private static class PKEY
//        //{
//        //    // System.AppUserModel.ID – {9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}, PID 5
//        //    public static readonly PROPERTYKEY AppUserModel_ID = new PROPERTYKEY
//        //    {
//        //        fmtid = new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"),
//        //        pid = 5
//        //    };

//        //    // Optional: System.AppUserModel.ToastActivatorCLSID (für Klick‑Aktivierung)
//        //    public static readonly PROPERTYKEY AppUserModel_ToastActivatorCLSID = new PROPERTYKEY
//        //    {
//        //        fmtid = new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"),
//        //        pid = 26
//        //    };
//        //}

//        //[StructLayout(LayoutKind.Explicit)]
//        //private struct PROPVARIANT
//        //{
//        //    [FieldOffset(0)] public ushort vt;
//        //    [FieldOffset(8)] public IntPtr pointerValue; // LPWSTR

//        //    public static PROPVARIANT FromString(string value)
//        //    {
//        //        var pv = new PROPVARIANT
//        //        {
//        //            vt = 31, // VT_LPWSTR
//        //            pointerValue = Marshal.StringToCoTaskMemUni(value)
//        //        };
//        //        return pv;
//        //    }

//        //    public void Clear()
//        //    {
//        //        PropVariantClear(ref this);
//        //    }
//        //}

//        //[DllImport("Ole32.dll")]
//        //private static extern int PropVariantClear(ref PROPVARIANT pvar);

//        //#endregion
//    }
//}
