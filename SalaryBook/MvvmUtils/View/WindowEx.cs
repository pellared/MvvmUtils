using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Pellared.Utils.Mvvm.View
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
            Contract.Requires<ArgumentNullException>(target != null, "target");

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

        #endregion

        public static bool IsModal(this Window window)
        {
            Contract.Requires<ArgumentNullException>(window != null, "window");

            return (bool)typeof(Window).GetField("_showingAsDialog", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(window);
        }
    }

    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    }
}