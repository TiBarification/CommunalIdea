using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace projectBOSE.ClientAuthentication
{
    class ClientAuthenticator : ISender
    {
        private int portNumber;
        private IPAddress ipAddress;
        private TcpClient tcpClient;
        private NetworkStream netStream;
        /// <summary>
        /// 
        /// </summary>
        public ClientAuthenticator()
        {
            using (StreamReader sr = new StreamReader("port_ip.txt"))
            {
                try
                {
                    this.portNumber = int.Parse(sr.ReadLine()); // Первая строка - порт
                    this.ipAddress = IPAddress.Parse(sr.ReadLine()); // Вторая строка - ip
                    this.Connect(); // Установить поток с сервером
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
                        Console.WriteLine(ex.Message + " Bad port or ip in file!");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Connect()
        {
            this.tcpClient = new TcpClient(this.ipAddress.ToString(), this.portNumber); // Инициализировать клиента как объект
            this.netStream = this.tcpClient.GetStream(); // Получить поток
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public bool Authenticate(string login, string password)
        {
            // Отправка данных на сервер
            byte[] dataToAuth = Encoding.Unicode.GetBytes(login + '\t' + password);
            this.netStream.Write(dataToAuth, 0, dataToAuth.Length);

            // Получение разрешения от сервера
            byte[] dataFromServer = new byte[1]; // Массив с ответом
            int bytesReceivedCount = 0;
            // Ответ от сервера
            do
            {
                bytesReceivedCount = netStream.Read(dataFromServer, 0, dataFromServer.Length);
            }
            while (netStream.DataAvailable);

            if (dataFromServer[0] == 1)
                return true;
            else
                return false;
        }
    }
}