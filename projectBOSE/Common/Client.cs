using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBOSE.Common
{
    /// <summary>
    /// Описывает клиента
    /// </summary>
    public class Client
    {
        public int id;
        public long accountid;
        public string firstname;
        public string lastname;
        public int age;

        public Client(int id, long accountid, string firstname, string lastname, int age)
        {
            this.id = id;
            this.accountid = accountid;
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
        }
    }
}
