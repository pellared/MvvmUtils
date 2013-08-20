using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace Pellared.SalaryBook.WinForms
{
    public partial class WpfUserControl : UserControl
    {
        public WpfUserControl()
        {
            InitializeComponent();
        }

        public UIElement Element
        {
            get { return elementHost.Child; }
            set { elementHost.Child = value; }
        }
    }
}