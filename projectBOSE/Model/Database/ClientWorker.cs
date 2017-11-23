using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectBOSE.Common;
using System.Data.SQLite;

namespace projectBOSE.Model.Database
{
    class ClientWorker
    {
        private const string _dbpath = "../../Database/communal.db";
        public const ulong recieverAccount = 445512429418; // Константный счёт человека, который даёт услугу
        /// <summary>
        /// Вернуть объект клиента по его рассчётному счёту
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Client ReturnClientByAccount(ulong ? account)
        {
            if (account == null)
                return null;

            var m_dbConnection = new SQLiteConnection("Data Source=" + _dbpath + ";Version=3;");
            m_dbConnection.Open(); // open DB

            var command = new SQLiteCommand("SELECT * FROM clients WHERE accountid=" +  account, m_dbConnection);

            SQLiteDataReader sqdr = command.ExecuteReader();
            if (sqdr.HasRows == false)
            {
                m_dbConnection.Close();
                return null;
            }

            sqdr.Read(); 

            Client client = new Client(sqdr.GetInt32(0), sqdr.GetInt64(1), sqdr.GetString(2), sqdr.GetString(3), sqdr.GetInt32(4));

            m_dbConnection.Close(); // close DB
            
            return client;
        }
    }
}
