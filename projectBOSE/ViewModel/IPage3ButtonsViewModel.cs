//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace projectBOSE.ViewModel
{
    public interface IPage3ButtonsViewModel
    {
        RelayCommand PaymentCommand { get; }
        RelayCommand PayersCommand { get; }
        RelayCommand SettingsCommand { get; }
    }
}
