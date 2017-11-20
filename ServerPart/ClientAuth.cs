using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Security.Cryptography;

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

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Authenticate()
        {
           


                NetworkStream networkStream = null; // Поток взаимодействия между сервером и клиентом
            try
            {
                networkStream = this.tcpClient.GetStream(); // Получить поток
                byte[] buffer = new byte[1024];

                StringBuilder sb = new StringBuilder();
                int bytesReceived = 0;

                do
                {
                    bytesReceived = networkStream.Read(buffer, 0, buffer.Length); // Читаем сообщение в буффер
                    sb.Append(Encoding.Unicode.GetString(buffer, 0, bytesReceived)); // Превращаю в массив символов

                } while (networkStream.DataAvailable);
            }
            catch
            {

            }
        }
    }
}
