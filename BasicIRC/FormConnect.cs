using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicIRC
{
    public partial class FormConnect : Form
    {
        private IRC irc;

        public FormConnect()
        {
            InitializeComponent();
            irc = new IRC();
            irc.Connected += Connected;
            irc.Error += Error;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Enabled = false;

            if(!irc.Start(ServerBox.Text, NickBox.Text))
            {
                new FormErrorConnect().Show();
                Enabled = true;
            }
        }

        private void Error(object o, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                new FormError().Show();
                Enabled = true;
            });
        }

        private void Connected(object o, EventArgs e)
        {
            irc.Connected -= Connected;
            irc.Error -= Error;

            Invoke((MethodInvoker)delegate
            {
                Hide();
                var form = new FormClient(irc);
                form.Closed += (a, b) => Close();
                form.Show();
            });
        }


    }
}
