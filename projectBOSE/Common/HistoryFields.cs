using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBOSE.Common
{
    /// <summary>
    /// Хранит в себе записи в базе данных и из неё. Будет использоваться в datagrid
    /// </summary>
    public class HistoryFields
    {
        public int service_id;
        public int client_id;
        public DateTime by_date;
        public uint? receiver;
        public double paid;
        public HistoryFields(int service_id, int client_id, DateTime by_date, uint? receiver, double paid)
        {
            this.service_id = service_id;
            this.client_id = client_id;
            this.by_date = by_date;
            this.receiver = receiver;
            this.paid = paid;
        }
    }
}
