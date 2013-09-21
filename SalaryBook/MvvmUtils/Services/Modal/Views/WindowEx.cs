using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Pellared.Utils.Mvvm.Services.Modal.Views
{
    public static class WindowEx
    {
        #region Closed attached DependencyProperty

        public static readonly DependencyProperty ClosedProperty =
                DependencyProperty.RegisterAttached(
                                                    "Closed",
                                                    typeof(bool),
                                                    typeof(WindowEx),
                                                    new PropertyMetadata(ClosedChanged));

        public static void SetClosed(Window target, bool value)
        {
            target.SetValue(ClosedProperty, value);
        }

        private static void ClosedChanged(
                DependencyObject d,
                DependencyPropertyChangedEventArgs e)
        {
            bool closed = (bool)e.NewValue;
            if (closed)
            {
                var window = d as Window;
                if (window != null)
                {
                    if (window.IsModal())
                    {
                        window.DialogResult = true;
                    }
                    else
                    {
                        window.Close();
                    }
                }
            }

        }

        public static bool IsModal(this Window window)
        {
            return (bool)typeof(Window).GetField("_showingAsDialog", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(window);
        }

        #endregion

        #region HideWindowButtons attached DependencyProperty

        private const int GwlStyle = -16;
        private const uint WsSysmenu = 0x80000;

        public static readonly DependencyProperty HideWindowButtonsProperty =
          DependencyProperty.RegisterAttached(
            "HideWindowButtons",
            typeof(bool),
            typeof(WindowEx),
            new PropertyMetadata(HideWindowButtonsChanged));

        public static void SetHideWindowButtons(Window element, bool value)
        {
            element.SetValue(HideWindowButtonsProperty, value);
        }

        private static void HideWindowButtonsChanged(
                DependencyObject d,
                DependencyPropertyChangedEventArgs e)
        {
            bool hideWindowButtons = (bool)e.NewValue;
            if (hideWindowButtons)
            {
                var window = d as Window;
                if (window != null)
                {
                    window.SourceInitialized += delegate
                    {
                        HideWindowButtons(window);
                    };
                }
            }
        }

        public static void HideWindowButtons(Window window)
        {
            var hwnd = new WindowInteropHelper(window).Handle;
            NativeMethods.SetWindowLong(hwnd, GwlStyle, NativeMethods.GetWindowLong(hwnd, GwlStyle) & (~WsSysmenu));
        }



        #endregion
    }

    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    }
}