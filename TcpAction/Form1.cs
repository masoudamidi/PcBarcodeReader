using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace PcBarcodeReaderSenderSimulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
           // string HostName = "192.168.0.218";// txtHost.Text;
            string HostName = txtHost.Text;
            int prt = 2112;// Int32.Parse(txtPort.Text);

            TcpClient tc = new TcpClient(HostName, prt);

            NetworkStream ns = tc.GetStream();

            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(textBox1.Text);
            ns.Write(myWriteBuffer, 0, myWriteBuffer.Length);


            //FileStream fs = File.Open(txtFile.Text, FileMode.Open);

            //int data = fs.ReadByte();

            //while (data != -1)
            //{
            //    ns.WriteByte((byte)data);
            //    data = fs.ReadByte();
            //}
            //fs.Close();
            ns.Close();
            tc.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string str = ofd.FileName;
            //txtFile.Text = str.ToString();
        }
    }
}
