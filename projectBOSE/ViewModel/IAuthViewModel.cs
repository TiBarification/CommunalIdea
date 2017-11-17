using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace projectBOSE.ViewModel
{
    public interface IAuthViewModel
    {
        RelayCommand LoginCommand { get; }
        string UsernameString { get; set; }
        string PasswordString { get; set; }
        
        event EventHandler Identifying;
    }
}
