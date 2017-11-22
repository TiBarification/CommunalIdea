using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace projectBOSE.ViewModel
{
    public interface IPaymentViewModel
    {
        RelayCommand PayCommand { get; }
        string AmountString { get; set; }
        string PersonalAccount { get; set; }
        string FullNamePayer { get; set; }
        string ReceiverAccount { get; set; }
        ObservableCollection<string> TypesOfServices { get; set; }
    }
}
