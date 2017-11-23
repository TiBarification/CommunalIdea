using projectBOSE.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBOSE.Model.Database
{
    public class MainDataBaseWork
    {
        public const int RECEIVER =2 ;
        private const string _dbpath = "../../Database/communal.db";
        public ObservableCollection<string> servicetypes { get; private set; }

        public List<Services> GetServicesCollection()
        {
            servicetypes = new ObservableCollection<string>();
            var m_dbConnection = new SQLiteConnection("Data Source=" + _dbpath + ";Version=3;");
            m_dbConnection.Open();

            string sql = "SELECT * FROM services LIMIT 100";

            var lOut = new List<Services>();

            var sqc = new SQLiteCommand(sql, m_dbConnection);

            SQLiteDataReader sqdr = sqc.ExecuteReader();

            while (sqdr.Read())
            {
                var service = new Services();
                service.id = sqdr.GetInt32(0);
                service.name = sqdr.GetString(1);
                service.tariff = sqdr.GetDouble(2);
                servicetypes.Add(service.name);
                lOut.Add(service);
            }

            m_dbConnection.Close();

            return lOut;
        }
    }
}
