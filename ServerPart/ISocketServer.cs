using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPart
{
    interface ISocketServer
    {
        void Start();
        void Listen();
    }
}
