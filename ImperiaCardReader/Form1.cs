using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ImperiaCardReader
{


    public partial class ImperiaCardReader : Form
    {
        public ImperiaCardReader()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
            //HKEY_CLASSES_ROOT\MIME\Database\Content Type\application/json
            string keyName = @"HKEY_CLASSES_ROOT\MIME\Database\Content Type\application/json";

            var appName = Process.GetCurrentProcess().ProcessName + ".exe";

            try
            {
                if (Registry.GetValue(keyName, "CLSID", "{25336920-03F9-11cf-8FD0-00AA00686F13}") == null)
                    Registry.SetValue(keyName, "CLSID", "{25336920-03F9-11cf-8FD0-00AA00686F13}", RegistryValueKind.String);
                if (Registry.GetValue(keyName, "Encoding", new byte[] { 08, 00, 00, 00 }) == null)
                    Registry.SetValue(keyName, "Encoding", new byte[] { 08, 00, 00, 00 }, RegistryValueKind.Binary);


                SetIE11KeyforWebBrowserControl(appName);

            }
            catch (Exception e)
            {

            }
        }

        private void SetIE11KeyforWebBrowserControl(string appName)
        {
            RegistryKey Regkey = null;
            try
            {

                //For 64 bit Machine 
                if (Environment.Is64BitOperatingSystem)
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\MAIN\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                else  //For 32 bit Machine 
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);

                //If the path is not correct or 
                //If user't have priviledges to access registry 
                if (Regkey == null)
                {
                    MessageBox.Show("Application Settings Failed - Address Not found");
                    return;
                }

                string FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                //Check if key is already present 
                if (FindAppkey == "11000")
                {
                    Regkey.Close();
                    return;
                }

                //If key is not present add the key , Kev value 8000-Decimal 
                if (string.IsNullOrEmpty(FindAppkey))
                    Regkey.SetValue(appName, unchecked((int)0x2AF8), RegistryValueKind.DWord);

                //check for the key after adding 
                FindAppkey = Convert.ToString(Regkey.GetValue(appName));



            }
            catch (Exception ex)
            {
                MessageBox.Show("Application Settings Failed");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close the Registry 
                if (Regkey != null)
                    Regkey.Close();
            }


        }

        private void UserURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navigate(UserURL.Text);
            }
        }

        // Navigates to the URL in the address box when 
        // the Go button is clicked.
        private void ButtonGo_Click(object sender, EventArgs e)
        {
            Navigate(UserURL.Text);
        }

        // Navigates to the given URL if it is valid.
        private void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://") &&
                !address.StartsWith("https://"))
            {
                address = "http://" + address;
            }
            try
            {
                webBrowser1.Navigate(new Uri(address));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }

        // Updates the URL in TextBoxAddress upon navigation.
        private void webBrowser1_Navigated(object sender,
            WebBrowserNavigatedEventArgs e)
        {
            UserURL.Text = webBrowser1.Url.ToString();

        }

        bool IsReady;

        private void ButtonGetMap_Click(object sender, EventArgs e)
        {
            if (WorldNum.Value != 0)
            {
                WorldNum.Enabled = false;
                bool[,] check = new bool[10000, 10000];
                string path = Path.GetTempPath() + @"globalMapJson.csv";
                string user = "sapira1_import";
                string pw = "5aUYyJ";

                if (File.Exists(path))
                    File.Delete(path);


                string[] json = new string[10];
                json = generateJSONfile();

                for (int i = 0; i < 10; i++)
                {
                    JsonTextReader reader = new JsonTextReader(new StringReader(json[i]));


                    JsonSerializer serializer = new JsonSerializer();
                    RootObject r = serializer.Deserialize<RootObject>(reader);
                    List<Block> b = new List<Block>(r.blocks);
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                    {

                        foreach (Block block in b)
                        {
                            List<Koordinate> k = new List<Koordinate>(block.data);
                            foreach (Koordinate kord in k)
                            {
                                string spieler = "";
                                string allianz = "";
                                string type = "";

                                List<D> toolinfo = new List<D>(kord.tooltip.d);
                                foreach (D d in toolinfo)
                                {
                                    if (type == "0" || type == "")
                                    {
                                        switch (kord.tooltip.t)
                                        {
                                            case "Military post":
                                            case "Fort":
                                            case "Askeri karakol":
                                                type = "1";
                                                break;
                                            case "Trading post":
                                            case "Handelszentrum":
                                            case "Ticaret karakolu":
                                                type = "2";
                                                break;
                                            case "Colony":
                                            case "Kolonie":
                                            case "Koloni":
                                                type = "3";
                                                break;
                                            case "Vassal":
                                            case "Vasall":
                                            case "Köleleştirilmiş eyalet":
                                                type = "4";
                                                break;
                                            case "Rally point":
                                            case "Versammlungsplatz":
                                            case "Toplanma noktası":
                                                type = "5";
                                                break;
                                            default:
                                                type = "0";
                                                break;
                                        }
                                    }
                                    if (d.id == 11 && ((d.value.ToString() == "jolly camp") ||
                                        (d.value.ToString() == "Barbar Ordugahı") ||
                                        (d.value.ToString() == "Lustiges Lager")))
                                    {
                                        spieler = "jolly camp";
                                        type = "11";
                                    }
                                    if (d.id == 1)
                                    {
                                        spieler = d.value.ToString();
                                    }
                                    if (d.id == 4)
                                        if (spieler == "")
                                            spieler = d.value.ToString();
                                    if (d.id == 5)
                                        allianz = d.value.ToString();
                                }
                                if (type != "0")
                                {
                                    int x = Convert.ToInt32(kord.x) + 2000;
                                    int y = Convert.ToInt32(kord.y) + 2000;
                                    if (check[x, y] == false)
                                    {
                                        file.WriteLine("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";", WorldNum.Value, kord.x, kord.y, type, spieler, allianz);
                                        check[x, y] = true;
                                    }
                                }
                            }

                        }

                    }
                }

                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri("ftp://sapira1.bplaced.net/" + path));
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(user, pw);

                // Copy the contents of the file to the request stream.
                StreamReader sourceStream = new StreamReader(path);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                response.Close();
                File.Delete(path);

                Navigate("http://imperia.sapira1.bplaced.com/import/import.php");
                WorldNum.Enabled = true;
            }
            MessageBox.Show("Finish!");

            Navigate("http://www.imperiaonline.org/");
        }


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            IsReady = true;
        }

        private void webBrowser1_FileDownload(object sender, EventArgs e)
        {

        }

        private string[] generateJSONfile()
        {
            string[] ret = new string[10];
            string url = "";

            for (int z = 0; z < 10; z++)
            {
                ret[z] = "";
            }
            for (int a = 0; a < 10; a++)
            {
                for (int b = 0; b < 10; b++)
                {
                    for (int c = 0; c < 10; c++)
                    {
                        url = "http://" + webBrowser1.Url.Host + "/imperia/game_v6/game/json/globalMapJson.php?blocks=1";
                        for (int i = 0; i < 10; i++)
                        {
                            url += "&b[]=";
                            if (a != 0)
                                url += a.ToString();
                            if ((b != 0) || (a != 0))
                                url += b.ToString();
                            if ((c != 0) || (a != 0) || (b != 0))
                                url += c.ToString();

                            url += i.ToString();
                        }
                        url += "&decrypt=1";

                        IsReady = false;
                        Navigate(url);

                        do
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        } while (!IsReady);

                        if (webBrowser1.DocumentText.Length > 100)
                        {
                            if (ret[a] == "" || ret[a].StartsWith("<"))
                            {
                                ret[a] = webBrowser1.DocumentText.Substring(0, webBrowser1.DocumentText.Length - 3) + ",";
                            }
                            else
                            {
                                ret[a] += webBrowser1.DocumentText.Substring(0, webBrowser1.DocumentText.Length - 3).Remove(0, 11) + ",";
                            }
                        }
                        else //keine weiteren inhalte mehr
                        {
                            a = 10;
                            b = 10;
                            c = 10;
                        }

                    }

                }
                if (ret[a].Length > 0)
                    ret[a].Substring(0, ret.Length - 2);
                ret[a] += "]}";
            }
            return ret;
        }



        public class D2
        {
            public string key { get; set; }
            public object value { get; set; }
            public int id { get; set; }
            public int? holdingType { get; set; }
        }

        public class D
        {
            public string key { get; set; }
            public object value { get; set; }
            public int id { get; set; }
            public string t { get; set; }
            public List<D2> d { get; set; }
            public int? holdingType { get; set; }
        }

        public class Tooltip
        {
            public string t { get; set; }
            public string ic { get; set; }
            public List<D> d { get; set; }
        }

        public class Koordinate
        {
            public string x { get; set; }
            public string y { get; set; }
            public string w { get; set; }
            public string h { get; set; }
            public string id { get; set; }
            public object type { get; set; }
            public Tooltip tooltip { get; set; }
            public List<object> card { get; set; }
            public int? has_exclusive { get; set; }
            public string bColor { get; set; }
            public int? box { get; set; }
            public List<object> icons { get; set; }
            public string sub_id { get; set; }

            public string terrain_type { get; set; }
            public string num { get; set; }
            public int? border { get; set; }
        }

        public class Block
        {
            public string id { get; set; }
            public List<Koordinate> data { get; set; }
        }

        public class RootObject
        {
            public List<Block> blocks { get; set; }
        }

    }
}
