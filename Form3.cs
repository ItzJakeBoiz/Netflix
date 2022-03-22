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
    public partial class Form3 : Form
    {
        private Size oldSize;
        private void Form1_Load(object sender, EventArgs e) => oldSize = base.Size;
        protected override void OnResize(System.EventArgs e)
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
        }
        public Form3(string token)
        {
            Global.Token = token;
            InitializeComponent();


            //this.TopMost = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

        }
        public class Global
        {
            public static string token;
            public static String Token { get; set; }
        }
        protected override void OnLoad(EventArgs e)
        {
            Console.WriteLine(Global.Token);
            WebRequest request = WebRequest.Create("https://backend-server.18jchadwick.repl.co/api/films/token/" + Global.Token);
            request.Proxy = null;
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string[] videos = content.Split(',');
            foreach (var video in videos)
            {
                var web = new WebBrowser();
                web.Parent = flowLayoutPanel1;
                web.Size = new Size(300, 200);
                base.OnLoad(e);
                var embed = "<html><head>" +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
                "</head><body>" +
                "<iframe width=\"300\" src=\"{0}\"" +
                "frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
                "</body></html>";
                var url = video;
                web.DocumentText = string.Format(embed, url);
            }
                
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
