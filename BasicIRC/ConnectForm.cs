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
    public partial class ConnectForm : Form
    {
        private IRC irc;

        public ConnectForm()
        {
            InitializeComponent();
            irc = new IRC();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {

        }
    }
}
