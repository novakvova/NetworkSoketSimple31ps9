using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSocketApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse("10.7.180.167");//IPAddress.Parse("127.0.0.1"); //Dns.GetHostAddresses("google.com.ua")[0];
            IPEndPoint ep = new IPEndPoint(ip, 1098);
            Socket s = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.IP);
            try
            {
                s.Connect(ep);
                if (s.Connected)
                {
                    string strSend = "Привіт Я Славік\r\n\r\n";
                    s.Send(Encoding.UTF8.GetBytes(strSend));
                    byte[] buffer = new byte[1024];
                    int l;
                    do
                    {
                        l = s.Receive(buffer);
                        txtMesssage.Text += Encoding.UTF8.GetString(buffer, 0, l);
                    } while (l > 0);
                    //txtMesssage.Text = "Connected good";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                s.Shutdown(SocketShutdown.Both);
                s.Close();
            }
        }
    }
}
