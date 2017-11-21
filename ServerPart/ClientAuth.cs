using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.IO;

namespace ServerPart
{
    class ClientAuth:IAuth
    {
        private TcpClient tcpClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpClient"></param>
        public ClientAuth(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        private bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="networkStream"></param>
        /// <returns></returns>
        private StringBuilder ReadFromStream(NetworkStream networkStream)
        {
            byte[] buffer = new byte[1024];

            StringBuilder data = new StringBuilder();
            int bytesReceived = 0;

            do
            {
                bytesReceived = networkStream.Read(buffer, 0, buffer.Length); // Читаем сообщение в буффер
                data.Append(Encoding.Unicode.GetString(buffer, 0, bytesReceived)); // Превращаю в массив символов

            } while (networkStream.DataAvailable);

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetPasswordWithLogin(string login)
        {
            using (StreamReader sr = new StreamReader("hashedPasswords.txt"))
            {
                string[] fileData;
                while (sr.Peek() != -1)
                {
                    fileData = sr.ReadLine().ToString().Split('\t');
                    if (fileData[1].Equals(login))
                        return fileData[0]; // Вернуть пароль для пользователя, который хранится в сервере
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Authenticate()
        {
            NetworkStream networkStream = null; // Поток взаимодействия между сервером и клиентом
            try
            {
                networkStream = this.tcpClient.GetStream(); // Получаю поток
                // TODO: Fix empty network strings 
                StringBuilder clientData = this.ReadFromStream(networkStream); // Получить поток и прочитать из него
                string []logs = clientData.ToString().Split('\t'); // logs[0] - login, logs[1] - password

                Console.WriteLine("Client {0} with password {1} connected!", logs[0], logs[1]);

                using (MD5 md5Hash = MD5.Create())
                {
                    string userServerHash = GetPasswordWithLogin(logs[0]); // Пароль внутри сервера

                    // Отправляю клиенту массив из одного байта. 1 - удачная аутентификация, 2 - нет
                    byte[] message = new byte[1];
                    if (VerifyMd5Hash(md5Hash, logs[1], userServerHash)) // Сравниваю пороль из потока с внутренним
                        message[0] = 1;
                    else
                        message[0] = 0;
                    networkStream.Write(message, 0, message.Length);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                // Закрываю потоки
                if (networkStream != null)
                    networkStream.Close();
                if (this.tcpClient != null)
                    this.tcpClient.Close();
            }
        }
    }
}