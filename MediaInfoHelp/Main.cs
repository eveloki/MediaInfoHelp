using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaInfo.DotNetWrapper.Enumerations;
using MediaInfoHelp.M;

namespace MediaInfoHelp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private delegate void delInfo1(string text);
        private void Main_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;

            StartInfo();

        }
        string basepath= AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
        bool m_ready = true;
        public void StartInfo()
        {
            try
            {
                using (var mediaInfo = new MediaInfo.DotNetWrapper.MediaInfo())
                {
                    var text = "\r\n已检测到安装的MediaInfo \r\n";
                    text += mediaInfo.Option("Info_Version");

                    //Information about MediaInfo
                    //text += "\r\n\r\nInfo_Parameters\r\n";
                    //text += mediaInfo.Option("Info_Parameters");

                    //text += "\r\n\r\nInfo_Capacities\r\n";
                    //text += mediaInfo.Option("Info_Capacities");

                    //text += "\r\n\r\nInfo_Codecs\r\n";
                    //text += mediaInfo.Option("Info_Codecs");

                    SetrichTextBox1(text);

                }
            }
            catch (Exception ex)
            {
                m_ready = false;
                string msg = "请安装MediaInfo 64位";
                SetrichTextBox1(msg);
                MessageBox.Show(msg);
            }

        }
        public void GetInfo(string file)
        {
            string mainbasefile = basepath + "MB\\1.txt";
            string mbstring = System.IO.File.ReadAllText(mainbasefile, Encoding.UTF8);
            if (!m_ready)
            {
                string msg = "程序没有正确初始化 err 001";
                SetrichTextBox1(msg);
                MessageBox.Show(msg);
                return;
            }
            string name = System.IO.Path.GetFileNameWithoutExtension(file);
            string type = System.IO.Path.GetExtension(file);
            type = type.ToLower();
            switch (type)
            {
                case ".mkv":
                case ".mp4":
                case ".flv":
                    try
                    {
                        using (var mediaInfo = new MediaInfo.DotNetWrapper.MediaInfo())
                        {
                            string text = "";
                            text += "\r\n\r\nOpen\r\n";
                            mediaInfo.Open(file);

                            //text += "\r\n\r\nInform with Complete=false\r\n";
                            //mediaInfo.Option("Complete");
                            //text += mediaInfo.Inform();

                            string  sss= mediaInfo.Inform();
                            
                            var fistinfp= GetKeyValues(sss);
                            //编码时间
                            string reeeeddd = fistinfp.FirstOrDefault(T => T.key == "Encoded date").value;
                            var RELEASE_DATE = reeeeddd.Split(' ')[1];
                            mbstring=mbstring.Replace("{RELEASE DATE}", RELEASE_DATE);

                            //文件大小
                            var RELEASE_SiZE = fistinfp.FirstOrDefault(T => T.key == "File size").value;
                            mbstring = mbstring = mbstring.Replace("{RELEASE SiZE}", RELEASE_SiZE);

                            //播放时间
                            //var RUNTiME = fistinfp.FirstOrDefault(T => T.key == "Duration").value;
                            //mbstring = mbstring.Replace("{RUNTiME}", RUNTiME);

                            //色深
                            var Bit_depth= fistinfp.FirstOrDefault(T => T.key == "Bit depth").value;
                            mbstring = mbstring.Replace("{BIT DEPTH}", Bit_depth);

                            //编码
                            var ViDEO_CODEC = fistinfp.FirstOrDefault(T => T.key == "Format profile").value;

                            var Bit_rate= fistinfp.FirstOrDefault(T => T.key == "Bit rate").value;

                            Bit_rate = Bit_rate.Replace("kb/s", "Kbps");
                            Bit_rate = Bit_rate.Replace("mb/s", "Mbps");
                            mbstring = mbstring.Replace("{Bit_rate}", Bit_rate);


                            //高宽
                            var Width = fistinfp.FirstOrDefault(T => T.key == "Width").value.Replace(" pixels","");
                            Width = Width.Replace(" ", "");
                            var Height = fistinfp.FirstOrDefault(T => T.key == "Height").value.Replace(" pixels", "");
                            Height = Height.Replace(" ", "");
                            //像素比
                            var RESOLUTiON = Width+" x "+ Height;
                            mbstring = mbstring.Replace("{RESOLUTiON}", RESOLUTiON);
                            
                            //高宽比
                            var DiSPLAY_ASPECT_RATiO= fistinfp.FirstOrDefault(T => T.key == "Display aspect ratio").value;
                            mbstring = mbstring.Replace("{DiSPLAY ASPECT RATiO}", DiSPLAY_ASPECT_RATiO);

                            //fps
                            var FRAME_RATE = fistinfp.FirstOrDefault(T => T.key == "Frame rate").value;
                            mbstring = mbstring.Replace("{FRAME RATE}", FRAME_RATE);



                            mediaInfo.Option("Complete", "1");
                            var s2= mediaInfo.Inform();
                            var fistinfp2 = GetKeyValues(s2);


                            var Encoded_Library_Name = fistinfp2.FirstOrDefault(T => T.key == "Encoded_Library_Name").value;
                            var Format_profile = fistinfp2.FirstOrDefault(T => T.key == "Format profile").value;
                            Format_profile += "";


                            //string Encoded_Library_Name = mediaInfo.Get(StreamKind.Video,0, "Encoded_Library_Name");

                            //string Format_profile = mediaInfo.Get(StreamKind.Video, 0, "Format profile",InfoKind.Name);
                            //Format_profile += " Tier";

                            //string Frame_rate = mediaInfo.Get(StreamKind.Video, 0, "Frame rate", InfoKind.Name);
                            //text += "\r\n\r\nInform with Complete=true\r\n";
                            //mediaInfo.Option("Complete", "1");
                            //text += mediaInfo.Inform();

                            //text += "\r\n\r\nCustom Inform\r\n";
                            //mediaInfo.Option("Inform", "General;File size is %FileSize% bytes");
                            //text += mediaInfo.Inform();

                            //foreach (string param in new[] { "BitRate", "BitRate/String", "BitRate_Mode" })
                            //{
                            //    text += "\r\n\r\nGet with Stream=Audio and Parameter='" + param + "'\r\n";
                            //    text += mediaInfo.Get(StreamKind.Audio, 0, param);
                            //}

                            //text += "\r\n\r\nGet with Stream=General and Parameter=46\r\n";
                            //text += mediaInfo.Get(StreamKind.General, 0, 46);

                            //text += "\r\n\r\nCount_Get with StreamKind=Stream_Audio\r\n";
                            //text += mediaInfo.CountGet(StreamKind.Audio);

                            //text += "\r\n\r\nGet with Stream=General and Parameter='AudioCount'\r\n";
                            //text += mediaInfo.Get(StreamKind.General, 0, "AudioCount");

                            //text += "\r\n\r\nGet with Stream=Audio and Parameter='StreamCount'\r\n";
                            //text += mediaInfo.Get(StreamKind.Audio, 0, "StreamCount");
                            SetrichTextBox1(text);
                        }
                       


                        //ffmpeg部分
                        var ffProbe = new NReco.VideoInfo.FFProbe();
                        var videoInfo = ffProbe.GetMediaInfo(file);



                        //播放时间
                        string sj = ToReadableString(videoInfo.Duration);
                        mbstring = mbstring.Replace("{RUNTiME}", sj);




                        richTextBox2.Text = "";
                        richTextBox2.Text = mbstring;


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误 请将输出提供给开发人员");
                        SetrichTextBox1(ex.ToString());
                    }
                    break;
                default:
                    string msg = "不支持文件格式";
                    SetrichTextBox1(msg);
                    MessageBox.Show(msg);
                    break;
            }

        }


        public List<KeyValue> GetKeyValues(string s)
        {
            List<KeyValue> keyValues = new List<KeyValue>();
            var sss = s.Split('\n');

            foreach (var item in sss)
            {
                var thisitem = item.Replace("\t", "");
                thisitem = thisitem.Replace("\r", "");
                thisitem = thisitem.Replace("\n", "");
                thisitem = thisitem.Trim();
                var intemss = thisitem.Split(new[] { " : " }, StringSplitOptions.None);
                if (intemss.Length > 1)
                {
                    keyValues.Add(new KeyValue() { key = intemss[0].Trim(), value = intemss[1].Trim() });
                }
                else
                {
                    keyValues.Add(new KeyValue() { key = thisitem, value = "Main_Top" });
                }
            }
            return keyValues;
        }

        private void SetrichTextBox1(string value)
        {

            if (richTextBox1.InvokeRequired)
            {
                delInfo1 d = SetrichTextBox1;
                richTextBox1.Invoke(d, value);
                return;
            }
            value = DateTime.Now.ToString("HH:mm:ss") + " " + value;
            richTextBox1.AppendText(value + "\n");
        }

        public static string ToReadableString(TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
         span.Duration().Days > 0 ? string.Format("{0:0}d{1}: ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
         span.Duration().Hours > 0 ? string.Format("{0:0}h{1}: ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
         span.Duration().Minutes > 0 ? string.Format("{0:0}m{1}: ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
         span.Duration().Seconds > 0 ? string.Format("{0:0}s{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }








        private void Main_Resize(object sender, EventArgs e)
        {
            //pictureBox1.Width = this.Width;

        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
           string file = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }

        private void label1_DragOver(object sender, DragEventArgs e)
        {
        }

       

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string file= ((string[])e.Data.GetData(DataFormats.FileDrop.ToString()))[0];
                label2.Text = "已选择文件" + file;
                GetInfo(file);
            }
        }

        private void label1_DragDrop(object sender, DragEventArgs e)
        {
            string file = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }

        private void Main_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            int maxlend = 800;
            if (this.richTextBox1.Lines.Length > maxlend)
            {
                int moreLines = this.richTextBox1.Lines.Length - maxlend;
                string[] lines = this.richTextBox1.Lines;
                Array.Copy(lines, moreLines, lines, 0, maxlend);
                Array.Resize(ref lines, maxlend);
                this.richTextBox1.Lines = lines;
            }

            richTextBox1.SelectionStart = richTextBox1.Text.Length; //Set the current caret position at the end
            richTextBox1.ScrollToCaret(); //Now scroll it automatically
        }
    }
}
