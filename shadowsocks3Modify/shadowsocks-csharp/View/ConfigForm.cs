using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using Shadowsocks.Controller;
using Shadowsocks.Model;
using Shadowsocks.Properties;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Shadowsocks.View
{
    public partial class ConfigForm : Form
    {
        private ShadowsocksController controller;

        // this is a copy of configuration that we are working on
        private Configuration _modifiedConfiguration;
        private int _lastSelectedIndex = -1;
        private static string str= @"<div\s+class\='testvpnitem'>(?:(?!<b>测试节点：)(?:.|\n))*<b>测试节点：((?:(?!</b><br\s+/>)(?:.|\n))*)</b><br\s+/>(?:(?!服务器IP：<span>)(?:.|\n))*服务器IP：<span>((?:(?!</span><br\s+/>)(?:.|\n))*)</span><br\s+/>(?:(?!端口：)(?:.|\n))*端口：((?:(?!<br\s+/>)(?:.|\n))*)<br\s+/>(?:(?!密码：)(?:.|\n))*密码：((?:(?!<br\s+/>)(?:.|\n))*)<br\s+/>(?:(?!加密方式：<span>)(?:.|\n))*加密方式：<span>((?:(?!</span>)(?:.|\n))*)</span>(?:(?!</div>)(?:.|\n))*</div>";
        private static bool flag=false;
        public ConfigForm(ShadowsocksController controller)
        {
            this.Font = System.Drawing.SystemFonts.MessageBoxFont;
            InitializeComponent();

            // a dirty hack
            this.ServersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PerformLayout();

            UpdateTexts();
            this.Icon = Icon.FromHandle(Resources.ssw128.GetHicon());

            this.controller = controller;
            controller.ConfigChanged += controller_ConfigChanged;

            LoadCurrentConfiguration();
        }

        private void UpdateTexts()
        {
            AddButton.Text = I18N.GetString("&Add");
            DeleteButton.Text = I18N.GetString("&Delete");
            IPLabel.Text = I18N.GetString("Server IP");
            ServerPortLabel.Text = I18N.GetString("Server Port");
            PasswordLabel.Text = I18N.GetString("Password");
            EncryptionLabel.Text = I18N.GetString("Encryption");
            ProxyPortLabel.Text = I18N.GetString("Proxy Port");
            RemarksLabel.Text = I18N.GetString("Remarks");
            OneTimeAuth.Text = I18N.GetString("Onetime Authentication (Experimental)");
            ServerGroupBox.Text = I18N.GetString("Server");
            OKButton.Text = I18N.GetString("OK");
            MyCancelButton.Text = I18N.GetString("Cancel");
            MoveUpButton.Text = I18N.GetString("Move &Up");
            MoveDownButton.Text = I18N.GetString("Move D&own");
            BtnUpdateServer.Text = "每次启动前请点击我获取最新的代理服务器!\n温馨提示：随机获取到的代理服务器有效时间不定！";
            this.Text = "佛跳墙";
        }

        private void controller_ConfigChanged(object sender, EventArgs e)
        {
            LoadCurrentConfiguration();
        }

        private void ShowWindow()
        {
            this.Opacity = 1;
            this.Show();
            IPTextBox.Focus();
        }

        private bool SaveOldSelectedServer()
        {
            try
            {
                if (_lastSelectedIndex == -1 || _lastSelectedIndex >= _modifiedConfiguration.configs.Count)
                {
                    return true;
                }
                Server server = new Server
                {
                    server = IPTextBox.Text.Trim(),
                    server_port = int.Parse(ServerPortTextBox.Text),
                    password = PasswordTextBox.Text,
                    method = EncryptionSelect.Text,
                    remarks = RemarksTextBox.Text,
                    auth = OneTimeAuth.Checked
                };
                int localPort = int.Parse(ProxyPortTextBox.Text);
                Configuration.CheckServer(server);
                Configuration.CheckLocalPort(localPort);
                _modifiedConfiguration.configs[_lastSelectedIndex] = server;
                _modifiedConfiguration.localPort = localPort;

                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(I18N.GetString("Illegal port number format"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void LoadSelectedServer()
        {
            if (ServersListBox.SelectedIndex >= 0 && ServersListBox.SelectedIndex < _modifiedConfiguration.configs.Count)
            {
                Server server = _modifiedConfiguration.configs[ServersListBox.SelectedIndex];

                IPTextBox.Text = server.server;
                ServerPortTextBox.Text = server.server_port.ToString();
                PasswordTextBox.Text = server.password;
                ProxyPortTextBox.Text = _modifiedConfiguration.localPort.ToString();
                EncryptionSelect.Text = server.method ?? "aes-256-cfb";
                RemarksTextBox.Text = server.remarks;
                OneTimeAuth.Checked = server.auth;
            }
        }

        private void LoadConfiguration(Configuration configuration)
        {
            ServersListBox.Items.Clear();
            foreach (Server server in _modifiedConfiguration.configs)
            {
                ServersListBox.Items.Add(server.FriendlyName());
            }
        }

        private void LoadCurrentConfiguration()
        {
            _modifiedConfiguration = controller.GetConfigurationCopy();
            LoadConfiguration(_modifiedConfiguration);
            _lastSelectedIndex = _modifiedConfiguration.index;
            if (_lastSelectedIndex < 0)
            {
                _lastSelectedIndex = 0;
            }
            ServersListBox.SelectedIndex = _lastSelectedIndex;
            UpdateMoveUpAndDownButton();
            LoadSelectedServer();
        }
        private void ConfigForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Sometimes the users may hit enter key by mistake, and the form will close without saving entries.

            if (e.KeyCode == Keys.Enter)
            {
                Server server = controller.GetCurrentServer();
                if (!SaveOldSelectedServer())
                {
                    return;
                }
                if (_modifiedConfiguration.configs.Count == 0)
                {
                    MessageBox.Show(I18N.GetString("Please add at least one server"));
                    return;
                }
                controller.SaveServers(_modifiedConfiguration.configs, _modifiedConfiguration.localPort);
                controller.SelectServerIndex(_modifiedConfiguration.configs.IndexOf(server));
            }

        }

        private void ServersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ServersListBox.CanSelect)
            {
                return;
            }
            if (_lastSelectedIndex == ServersListBox.SelectedIndex)
            {
                // we are moving back to oldSelectedIndex or doing a force move
                return;
            }
            if (!SaveOldSelectedServer())
            {
                // why this won't cause stack overflow?
                ServersListBox.SelectedIndex = _lastSelectedIndex;
                return;
            }
            if (_lastSelectedIndex >= 0)
            {
                ServersListBox.Items[_lastSelectedIndex] = _modifiedConfiguration.configs[_lastSelectedIndex].FriendlyName();
            }
            UpdateMoveUpAndDownButton();
            LoadSelectedServer();
            _lastSelectedIndex = ServersListBox.SelectedIndex;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            Server server = Configuration.GetDefaultServer();
            _modifiedConfiguration.configs.Add(server);
            LoadConfiguration(_modifiedConfiguration);
            ServersListBox.SelectedIndex = _modifiedConfiguration.configs.Count - 1;
            _lastSelectedIndex = ServersListBox.SelectedIndex;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            _lastSelectedIndex = ServersListBox.SelectedIndex;
            if (_lastSelectedIndex >= 0 && _lastSelectedIndex < _modifiedConfiguration.configs.Count)
            {
                _modifiedConfiguration.configs.RemoveAt(_lastSelectedIndex);
            }
            if (_lastSelectedIndex >= _modifiedConfiguration.configs.Count)
            {
                // can be -1
                _lastSelectedIndex = _modifiedConfiguration.configs.Count - 1;
            }
            ServersListBox.SelectedIndex = _lastSelectedIndex;
            LoadConfiguration(_modifiedConfiguration);
            ServersListBox.SelectedIndex = _lastSelectedIndex;
            LoadSelectedServer();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Server server = controller.GetCurrentServer();
            if (!SaveOldSelectedServer())
            {
                return;
            }
            if (_modifiedConfiguration.configs.Count == 0)
            {
                MessageBox.Show(I18N.GetString("Please add at least one server"));
                return;
            }
            controller.SaveServers(_modifiedConfiguration.configs, _modifiedConfiguration.localPort);
            controller.SelectServerIndex(_modifiedConfiguration.configs.IndexOf(server));
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfigForm_Shown(object sender, EventArgs e)
        {
            IPTextBox.Focus();
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            controller.ConfigChanged -= controller_ConfigChanged;
        }

        private void MoveConfigItem(int step)
        {
            int index = ServersListBox.SelectedIndex;
            Server server = _modifiedConfiguration.configs[index];
            object item = ServersListBox.SelectedItem;

            _modifiedConfiguration.configs.Remove(server);
            _modifiedConfiguration.configs.Insert(index + step, server);
            _modifiedConfiguration.index += step;

            ServersListBox.BeginUpdate();
            ServersListBox.Enabled = false;
            _lastSelectedIndex = index + step;
            ServersListBox.Items.Remove(item);
            ServersListBox.Items.Insert(index + step, item);
            ServersListBox.Enabled = true;
            ServersListBox.SelectedIndex = index + step;
            ServersListBox.EndUpdate();

            UpdateMoveUpAndDownButton();
        }

        private void UpdateMoveUpAndDownButton()
        {
            if (ServersListBox.SelectedIndex == 0)
            {
                MoveUpButton.Enabled = false;
            }
            else
            {
                MoveUpButton.Enabled = true;
            }
            if (ServersListBox.SelectedIndex == ServersListBox.Items.Count - 1)
            {
                MoveDownButton.Enabled = false;
            }
            else
            {
                MoveDownButton.Enabled = true;
            }
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            if (ServersListBox.SelectedIndex > 0)
            {
                MoveConfigItem(-1);  // -1 means move backward
            }
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            if (ServersListBox.SelectedIndex < ServersListBox.Items.Count - 1)
            {
                MoveConfigItem(+1);  // +1 means move forward
            }
        }

        private void EncryptionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EncryptionSelect.Text == "rc4" || EncryptionSelect.Text == "table")
            {
                OneTimeAuth.Enabled = false;
                OneTimeAuth.Checked = false;
            }
            else
            {
                OneTimeAuth.Enabled = true;
            }
        }

        private void BtnUpdateServer_Click(object sender, EventArgs e)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://www.yingsuowang.com/page/testss.html");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                Regex reg = new Regex(str, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                string html = reader.ReadToEnd();
                Match match = reg.Match(html);
                if (match.Success)
                {
                    if (flag)
                    {
                        flag = false;
                        match = reg.Match(html, match.Index + match.Length);
                    }
                    else
                    {
                        flag = true;
                    }
                    RemarksLabel.Text = I18N.GetString("Remarks");
                    IPLabel.Text = I18N.GetString("Server IP");
                    ServerPortLabel.Text = I18N.GetString("Server Port");
                    PasswordLabel.Text = I18N.GetString("Password");              
                    RemarksTextBox.Text= match.Groups[1].Value.ToString();
                    IPTextBox.Text= match.Groups[2].Value.ToString();
                    ServerPortTextBox.Text= match.Groups[3].Value.ToString();
                    PasswordTextBox.Text= match.Groups[4].Value.ToString();
                }
                else
                {
                    RemarksTextBox.Text = "";
                    IPTextBox.Text = "";
                    ServerPortTextBox.Text = "8888";
                    PasswordTextBox.Text = "";
                }
            }
            catch (System.Net.WebException exception)
            {
                MessageBox.Show("网络故障，无法与代理服务器连接！");
                Console.WriteLine(exception.ToString());
            }
            StringBuilder strText = new StringBuilder();
            strText.Append("获取最新的代理服务器!\n一旦无法跳墙，也请点击我！\n");
            strText.Append("当前网络节点:"+RemarksTextBox.Text);
            strText.Append("\n当前服务器IP:"+IPTextBox.Text);
            strText.Append("\n当前端口:"+ServerPortTextBox.Text);
            (sender as Button).Text = strText.ToString();
        }
    }
}
