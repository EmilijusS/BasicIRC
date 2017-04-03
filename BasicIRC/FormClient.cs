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
    public partial class FormClient : Form
    {
        private IRC irc;

        public FormClient(IRC irc)
        {
            InitializeComponent();
            this.irc = irc;
            TestTab('1');
            TestTab('2');
            TestTab('3');
        }

        private void FormClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            irc.CloseConnection();           
        }

        private void TestTab(char num)
        {
            TabPage testTabPage = new TabPage();
            testTabPage.BackColor = System.Drawing.SystemColors.Control;
            testTabPage.Location = new System.Drawing.Point(4, 22);
            testTabPage.Name = "testTabPage" + num;
            testTabPage.Padding = new System.Windows.Forms.Padding(3);
            testTabPage.Size = new System.Drawing.Size(802, 421);
            testTabPage.TabIndex = 0;
            testTabPage.Text = "test" + num;

            TextBox chatTextBox = new TextBox();
            chatTextBox.BackColor = System.Drawing.SystemColors.Window;
            chatTextBox.Location = new System.Drawing.Point(0, 0);
            chatTextBox.Multiline = true;
            chatTextBox.Name = "chatTextBox" + num;
            chatTextBox.ReadOnly = true;
            chatTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            chatTextBox.Size = new System.Drawing.Size(651, 425);
            chatTextBox.TabIndex = 0;

            TextBox userTextBox = new TextBox();
            userTextBox.BackColor = System.Drawing.SystemColors.Window;
            userTextBox.Location = new System.Drawing.Point(648, 0);
            userTextBox.Multiline = true;
            userTextBox.Name = "userTextBox" + num;
            userTextBox.ReadOnly = true;
            userTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            userTextBox.Size = new System.Drawing.Size(154, 425);
            userTextBox.TabIndex = 2;

            testTabPage.Controls.Add(chatTextBox);
            testTabPage.Controls.Add(userTextBox);
            tabControl.Controls.Add(testTabPage);
        }
    }
}
