using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerPart
{
    class SocketServer : ISocketServer
    {
        private int portNumber;
        private IPAddress ipAddress;
        private TcpListener tcpListener;

        /// <summary>
        /// 
        /// </summary>
        public SocketServer()
        {
            this.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            try
            {
                Console.WriteLine("Enter port number (e.g. 11000): ");
                this.portNumber = int.Parse(Console.ReadLine());
                ipAddress = IPAddress.Any; // Слушать на всех сетевых интерфейсах
                tcpListener = new TcpListener(this.ipAddress, this.portNumber);
                tcpListener.Start();
                Console.WriteLine("Server started recieving authorization requests!");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message + " Port must be between " + IPEndPoint.MinPort + " and " + IPEndPoint.MaxPort + '!');
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
                    Console.WriteLine(ex.Message + " Bad port input!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Listen()
        {
            try
            {
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient(); // Приём сокета клиента
                    Console.WriteLine("Connection with employee was established!");
                    ClientAuth clientAuth = new ClientAuth(tcpClient); // Создаю объект аутентификации клиента

                    // Создание потока и обработка
                    Thread myThread = new Thread(new ThreadStart(clientAuth.Authenticate));
                    myThread.Start();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                if (tcpListener != null)
                    tcpListener.Stop();
            }
        }
    }
}
