﻿using System;
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
        private Parser parser;

        public FormClient(Parser parser)
        {
            InitializeComponent();
            this.parser = parser;
            parser.JoinedChannel += (o, e) => NewChannelTab(e.message);
        }

        private void FormClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            parser.CloseConnection();           
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            parser.SendData(TextBoxMessage.Text, tabControl.TabCount == 0 ? null : tabControl.SelectedTab.Name);
            TextBoxMessage.Clear();
        }

        private void NewChannelTab(string channel)
        {
            Invoke((MethodInvoker)delegate
            {
                TabPage tabPage = new TabPage();
                tabPage.BackColor = System.Drawing.SystemColors.Control;
                tabPage.Location = new System.Drawing.Point(4, 22);
                tabPage.Name = channel;
                tabPage.Padding = new System.Windows.Forms.Padding(3);
                tabPage.Size = new System.Drawing.Size(802, 421);
                tabPage.TabIndex = 0;
                tabPage.Text = channel;

                TextBox chatTextBox = new TextBox();
                chatTextBox.BackColor = System.Drawing.SystemColors.Window;
                chatTextBox.Location = new System.Drawing.Point(0, 0);
                chatTextBox.Multiline = true;
                chatTextBox.Name = channel + "TextBox";
                chatTextBox.ReadOnly = true;
                chatTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
                chatTextBox.Size = new System.Drawing.Size(651, 425);
                chatTextBox.TabIndex = 0;

                TextBox userTextBox = new TextBox();
                userTextBox.BackColor = System.Drawing.SystemColors.Window;
                userTextBox.Location = new System.Drawing.Point(648, 0);
                userTextBox.Multiline = true;
                userTextBox.Name = channel + "UserTextBox";
                userTextBox.ReadOnly = true;
                userTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
                userTextBox.Size = new System.Drawing.Size(154, 425);
                userTextBox.TabIndex = 2;

                tabPage.Controls.Add(chatTextBox);
                tabPage.Controls.Add(userTextBox);
                tabControl.Controls.Add(tabPage);
                tabControl.SelectedTab = tabPage;
            });
        }
    }
}
