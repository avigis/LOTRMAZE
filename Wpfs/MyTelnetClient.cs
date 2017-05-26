using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Wpfs
{
    public class MyTelnetClient : ITelnetClient
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        public bool IsConnected
        {
            get
            {
                if (client == null)
                    return false;

                if (this.client.Client.Available == 0)
                    return true;

                if (client.Client.Poll(1000, SelectMode.SelectRead))
                    return false;


                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            //this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        public void Connect(string ip, int port)
        {
            IPAddress ipAdress = System.Net.IPAddress.Parse(ip);  
            
            IPEndPoint ep = new IPEndPoint(ipAdress, port);
            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public string Read()
        {
            //while (IsConnected)
            //{
                StringBuilder data = new StringBuilder();
                do
                {
                    data.Append((char) reader.Read()/*reader.ReadLine()*/);
                } while (reader.Peek() > 0/*stream.DataAvailable*/);/////////////////////////////////////////////////////////////

                //Console.WriteLine(data);
                
            //
            return data.ToString();
        }

        public void Write(string command)
        {
            writer.WriteLine(command);
        }

        public void Close()
        {
            stream.Close();
            client.Close();
        }


    }
}
