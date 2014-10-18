using Pellared.Common;
using Pellared.MvvmUtils.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Pellared.MvvmUtils
{
    public enum ResizeMode
    {
        NoResize,
        CanMinimize,
        CanResize
    }

    public interface IWindowService
    {
        void Show(IWindowViewModel viewModel, ResizeMode resizeMode);

        void ShowDialog(IWindowViewModel viewModel, ResizeMode resizeMode);
    }

    public class WindowService : IWindowService
    {
        private readonly Window ownerWindow;
        private readonly Form ownerForm;

        public WindowService()
        {
        }

        public WindowService(Window ownerWindow)
        {
            Ensure.NotNull(ownerWindow, "ownerWindow");
            this.ownerWindow = ownerWindow;
        }

        public WindowService(Form ownerForm)
        {
            Ensure.NotNull(ownerForm, "ownerForm");
            this.ownerForm = ownerForm;
        }

        public void Show(IWindowViewModel viewModel, ResizeMode resizeMode)
        {
            viewModel.Closed = false;
            ClosableWindow window = CreateWindow(viewModel, resizeMode);
            window.Open();
        }

        public void ShowDialog(IWindowViewModel viewModel, ResizeMode resizeMode)
        {
            viewModel.Closed = false;
            ClosableWindow window = CreateWindow(viewModel, resizeMode);
            window.OpenDialog();
        }

        private ClosableWindow CreateWindow(IWindowViewModel viewModel, ResizeMode resizeMode)
        {
            System.Windows.ResizeMode mode = GetMode(resizeMode);
            var window = new ClosableWindow(viewModel, mode);
            if (ownerWindow != null)
            {
                window.Owner = ownerWindow;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else if (ownerForm != null)
            {
                var helper = new WindowInteropHelper(window);
                helper.Owner = ownerForm.Handle;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            return window;
        }

        private System.Windows.ResizeMode GetMode(ResizeMode resizeMode)
        {
            switch (resizeMode)
            {
                case ResizeMode.NoResize:
                    return System.Windows.ResizeMode.NoResize;

                case ResizeMode.CanMinimize:
                    return System.Windows.ResizeMode.CanMinimize;

                case ResizeMode.CanResize:
                    return System.Windows.ResizeMode.CanResize;

                default:
                    return System.Windows.ResizeMode.NoResize;
            }
        }
    }
}
