using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBOSE.ClientAuthentication
{
    interface ISender
    {
        void Connect();
        bool Authenticate(string login, string password);
    }
}
