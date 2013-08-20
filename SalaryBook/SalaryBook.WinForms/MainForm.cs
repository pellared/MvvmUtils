using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace Pellared.SalaryBook.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public UIElement Element
        {
            set { wpfUserControl.Element = value; }
        }
    }
}