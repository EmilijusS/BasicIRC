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
        private Parser parser;

        public FormConnect()
        {
            InitializeComponent();
            parser = new Parser();
            parser.Connected += Connected;
            parser.Error += Error;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Enabled = false;

            if(!parser.Start(ServerBox.Text, NickBox.Text))
            {
                new FormErrorConnect().Show();
                Enabled = true;
            }
        }

        private void Error(object o, MessageEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                new FormError(e.message).Show();
                Enabled = true;
            });
        }

        private void Connected(object o, EventArgs e)
        {
            parser.Connected -= Connected;

            Invoke((MethodInvoker)delegate
            {
                Hide();
                var form = new FormClient(parser);
                form.Closed += (a, b) => Close();
                form.Show();
            });
        }


    }
}
