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
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Web.Script.Serialization;
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
        public class Comments
        {
            public string name { get; set; }
            public string link { get; set; }
            public string genre { get; set; }
            public string liked { get; set; }
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
            

            this.TopMost = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

        }
        public static Image GetImageFromPicPath(string strUrl)
        {
            Console.WriteLine(strUrl.ToString());
            WebResponse wrFileResponse;
            wrFileResponse = WebRequest.Create(strUrl).GetResponse();
            using (Stream objWebStream = wrFileResponse.GetResponseStream())
            {
                return Image.FromStream(objWebStream);
            }
        }
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }
        public class Global
        {
            public static String Token { get; set; }
            public static List<Comments> Videos { get; set; }

        }
        protected override void OnLoad(EventArgs e)
        {
            Console.WriteLine(Global.Token);
            WebRequest request = WebRequest.Create("https://backend-server.18jchadwick.repl.co/api/films/token/" + Global.Token);
            request.Proxy = null;
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Comments> videos = (List<Comments>)serializer.Deserialize(content, typeof(List<Comments>));
            Global.Videos = videos;
            Console.WriteLine(videos);
            foreach (Comments video in videos)
            {
                Console.WriteLine(video);
                var button = new Button();
                button.Text = "";
                button.BackgroundImageLayout = ImageLayout.Center;
                button.BackgroundImage = GetImageFromPicPath("https://img.youtube.com/vi/"+ video.link + "/mqdefault.jpg");
                
                button.Parent = flowLayoutPanel1;
                button.Size = new Size(320, 180);
                button.Click += new EventHandler(this.GreetingBtn_Click);
                button.Text = video.name;
                button.Font = new Font("Nirmala UI", 22);
                button.Font = new Font(button.Font, FontStyle.Bold);
                button.TextAlign = ContentAlignment.TopLeft;


            }
                
            
        }
        void GreetingBtn_Click(Object sender,EventArgs e)
        {
            foreach (Comments video in Global.Videos)
            {
                if(video.name == (sender as Button).Text)
                {
                    flowLayoutPanel1.Visible = false;
                    webBrowser1.Visible = true;
                    var embed = $"<html><head>" +
    "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
    "</head><body>" +
    "<iframe width=1890\"\" height=1050 src=\"{0}\"" +
    "frameborder = \"0\" allow = \" encrypted-media\"></iframe>" +
    "</body></html>";
                    var url = "https://www.youtube.com/embed/"+ video.link;
                    webBrowser1.DocumentText = string.Format(embed, url);
                }
            }
        }

            private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted_2(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
