using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace PcBarcodeReaderHOST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tStart();
        }

        public void tStart()
        {
            Thread t = new Thread(new ThreadStart(ListenData));
            t.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string oo = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    oo = ip.ToString();
                }
            }

            label1.Text = oo;
            label4.Text = Environment.MachineName.ToString();
            oo += ":2112";
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pictureBox1.Image = qrcode.Draw(oo, 70);
        }

        public delegate void testDelegate(string str);

        public void test(string str)
        {
            txtData.Text = str;
            Thread.Sleep(5000);
            Clipboard.SetText(str);
            SendKeys.Send("^{v}");
            SendKeys.Send("{ENTER}");
        }
        public void ListenData()
        {
            string oo = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    oo = ip.ToString();
                }
            }

            IPAddress ipad = IPAddress.Parse(oo);
            Int32 prt = 2112;
            TcpListener tl = new TcpListener(ipad, prt);
            tl.Start();

            TcpClient tc = tl.AcceptTcpClient();

            NetworkStream ns = tc.GetStream();
            StreamReader sr = new StreamReader(ns);

            string result = sr.ReadToEnd();

            

            Invoke(new testDelegate(test), new object[] { result });

            tc.Close();
            tl.Stop();

            tStart();

        }
    }
}
