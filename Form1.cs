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

/*things that have been deleted and idk if u need them or not
=> oldSize = base.Size;
*/

namespace Netflix
{
    public partial class Form1 : Form
    {
        
        private void Form1_Load(object sender, EventArgs e)
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
        public Form1()
        {

            InitializeComponent();


            this.TopMost = true;

            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;

            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor = Color.Transparent;

        }




private void textBox_Leave(object sender, EventArgs e)
{

}
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("https://backend-server.18jchadwick.repl.co/api/token/" + textBox1.Text + "/" + textBox2.Text);
            request.Proxy = null;
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Label.CheckForIllegalCrossThreadCalls = false;
            if (content == "null") {
                label3.Text = "Incorrect username or password";
                Task t = Task.Run(() =>
                {
                    Task.Delay(2500).Wait();
                    label3.Text = "";

                });
                
            }
            else
            {
                Form3 c = new Form3(content);
                this.Hide();
                c.ShowDialog();
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }


        private void Form_load(object sender, EventArgs e)
        {
            textBox1.Text = "Place Holder text...";
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Place Holder text...")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                textBox1.Text = "Place Holder text...";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            Form2 c = new Form2();
            this.Hide();
            c.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
