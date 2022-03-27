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
                
                var panel = new Panel();
                panel.Margin = new Padding(0,0,0,0);
                panel.Show();
                panel.Size = new Size(320, 220);
                panel.Parent = flowLayoutPanel1;
                Console.WriteLine(video);
                var button = new Button();
                button.Text = "";
                button.BackgroundImageLayout = ImageLayout.Center;
                button.BackgroundImage = GetImageFromPicPath("https://img.youtube.com/vi/" + video.link + "/mqdefault.jpg");
                button.Margin = new Padding(0, 0, 0, 0);
                button.Parent = panel;
                button.Size = new Size(320, 180);
                button.Click += new EventHandler(this.GreetingBtn_Click);
                button.Name = video.link;
                button.Location = new Point(0, 40);
                var text = new Label();
                text.Margin = new Padding(0, 0, 0, 0);
                text.Size = new Size(220, 40);
                text.Parent = panel;
                text.Text = video.name;
                text.Font = new Font("Nirmala UI", 20);
                text.Font = new Font(text.Font, FontStyle.Bold);
               

            }
            var returnbutton = new Button();
            returnbutton.Parent = this;
            returnbutton.Text = "Logout";
            returnbutton.Location = new Point(0, 1000);
            
        }
        void GreetingBtn_Click(Object sender, EventArgs e)
        {
            foreach (Comments video in Global.Videos)
            {
                if (video.link == (sender as Button).Name)
                {
                    flowLayoutPanel1.Visible = false;
                    var webBrowser1 = new WebBrowser();
                    webBrowser1.Size = new Size(1920, 1020);
                    webBrowser1.Parent = this;
                    var embed = $"<html><head>" +
    "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
    "</head><body>" +
    "<iframe width=1920 height=1020 border=none src=\"{0}\"" +
    "frameborder=0 allow = \" encrypted-media\" frameborder=\"0\" allowfullscreen></iframe>" +
    "</body></html>";
                    var url = "https://www.youtube.com/embed/" + video.link + "?rel=0&autoplay=1&fs=1";
                    webBrowser1.DocumentText = string.Format(embed, url);
                    webBrowser1.Name = "WEB";
                    var optionspannel = new Panel();
                    optionspannel.Name = "Options";
                    optionspannel.Dock = DockStyle.Bottom;
                    optionspannel.Parent = this;
                    optionspannel.Size = new Size(1920, 60);
                    var likebutton = new Button();
                    Console.WriteLine(video.liked);
                    if (video.liked == "true")
                    {
                        likebutton.Image = GetImageFromPicPath("https://cdn.discordapp.com/attachments/758677986756395038/957718243630514266/liked.png");
                        likebutton.Name = "LIKED";
                    }
                    else
                    {
                        likebutton.Image = GetImageFromPicPath("https://cdn.discordapp.com/attachments/758677986756395038/957718034045345802/unknown.png");
                        likebutton.Name = "LIKE";
                    }
                    likebutton.Tag = video.name;
                    likebutton.Click += new EventHandler(this.LikeButton);
                    
                    likebutton.Size = new Size(60, 60);
                    likebutton.Parent = optionspannel;
                    var backbutton = new Button();
                    backbutton.Parent = optionspannel;
                    backbutton.Size = new Size(60, 60);
                    backbutton.Text = "Back";
                    backbutton.Font = new Font("Nirmala UI", 10);
                    backbutton.Font = new Font(backbutton.Font, FontStyle.Bold);
                    backbutton.Location = new Point(1860, 0);
                    backbutton.Click += new EventHandler(this.BackButton);
                }
            }
        }
        private void BackButton(Object sender, EventArgs e)
        {
            this.Controls.RemoveByKey("Options");
            foreach (var c in this.Controls.OfType<WebBrowser>().Where(x => x.Name == "WEB"))
            { 
                c.DocumentText = "";
            }
            this.Controls.RemoveByKey("WEB");
            flowLayoutPanel1.Visible = true;

        }
        public class WebResult
        {
            public string Response { get; set; }
            public bool WasSuccessful { get; set; }
            public HttpStatusCode? StatusCode { get; set; }
        }
        public WebResult GetUrlContents(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var responseStream = new StreamReader(response.GetResponseStream()))
                {
                    return new WebResult
                    {
                        WasSuccessful = true,
                        Response = responseStream.ReadToEnd(),
                        StatusCode = response.StatusCode
                    };
                }
            }
            catch (WebException webException)
            {
                return new WebResult
                {
                    Response = null,
                    WasSuccessful = false,
                    StatusCode = (webException.Response as HttpWebResponse)?.StatusCode
                };
            }
        }
        private void LikeButton(Object sender, EventArgs e)
        {
            Button x = sender as Button;

            if (x.Name == "LIKED")
            {

                if (GetUrlContents("https://backend-server.18jchadwick.repl.co/api/like/" + Global.Token + "/" + x.Tag).WasSuccessful != false)
                {
                    x.Image = GetImageFromPicPath("https://cdn.discordapp.com/attachments/758677986756395038/957718034045345802/unknown.png");
                    x.Name = "LIKE";
                    WebRequest request = WebRequest.Create("https://backend-server.18jchadwick.repl.co/api/like/" + Global.Token + "/" + x.Tag);
                    request.Proxy = null;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                }
            }

            else
            {
                if (GetUrlContents("https://backend-server.18jchadwick.repl.co/api/like/" + Global.Token + "/" + x.Tag).WasSuccessful != false)
                {
                    x.Image = GetImageFromPicPath("https://cdn.discordapp.com/attachments/758677986756395038/957718243630514266/liked.png");
                x.Name = "LIKED";
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

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
