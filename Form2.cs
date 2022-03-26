using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Netflix
{
    public partial class Form2 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        /*protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            foreach (Control cnt in this.Controls)
                ResizeAll(cnt, base.Size);

            oldSize = base.Size;
        }
        private void ResizeAll(Control control, Size newSize)
        {
            int width = newSize.Width - oldSize.Width;
            control.Left += (control.Left * width) / oldSize.Width;
            control.Width += (control.Width * width) / oldSize.Width;

            int height = newSize.Height - oldSize.Height;
            control.Top += (control.Top * height) / oldSize.Height;
            control.Height += (control.Height * height) / oldSize.Height;
        }*/
        public Form2()
        {

            InitializeComponent();
            /*this.FormClosed +=
           new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.TopMost = true;

            this.FormBorderStyle = FormBorderStyle.None;

            this.WindowState = FormWindowState.Maximized;*/
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 c = new Form1();
            this.Hide();
            c.ShowDialog();
            this.Close();


        }
        private void button1_Click(object sender, EventArgs e)
        
        {
            Label.CheckForIllegalCrossThreadCalls = false;
            if (comboBox2.Text == "")
            {
                label3.Text = "You must choose a genre";
                Task t = Task.Run(() =>
                {

                    Task.Delay(2500).Wait();
                    label3.Text = "";

                });
                return;

            }
            WebRequest request = WebRequest.Create("https://backend-server.18jchadwick.repl.co/api/register/" + textBox1.Text + "/" + textBox2.Text+"/"+ comboBox2.Text + "/"+dateTimePicker1.Text + "/" + textBox3.Text);
            request.Proxy = null;
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Label.CheckForIllegalCrossThreadCalls = false;

            Form1 c = new Form1();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void showPasswordCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (showPasswordCheck.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Value = new DateTime(2000, 01, 01);
            comboBox2.SelectedIndex = comboBox2.FindStringExact("");
        }
    }
}
