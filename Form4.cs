using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Netflix
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            this.TopMost = true;
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Netflix c = new Netflix();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("https://backend-server.18jchadwick.repl.co/resetpassword/" + textBox1.Text);
            request.Proxy = null;
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            label3.Text = content;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
