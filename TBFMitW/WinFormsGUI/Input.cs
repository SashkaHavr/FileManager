using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsGUI
{
    partial class Input : Form
    {
        DialogWindows Return { get; }
        public Input(DialogWindows d)
        {
            InitializeComponent();
            Return = d;
            ResText.Focus();
            OKBtn.Click += Ok_Clkick;
            ResText.Focus();
            KeyDown += Enter_Click;
            KeyPreview = true;
        }
        public void SetText(string msg) => MsgText.Text = msg;
        void Ok_Clkick(object o, EventArgs e)
        {
            Return.ResString = ResText.Text;
            ResText.Text = String.Empty;
            Close();
        }
        void Enter_Click(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Ok_Clkick(o, e);
        }
    }
}
