using GalaSoft.MvvmLight.Command;
using projectBOSE.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBOSE.ViewModel
{
    public interface IPayViewModel
    {
        // Debug
        string DebugMessage { get; set; }

        //RelayCommand DebugMessage { get; }
        // Needed
        ObservableCollection<HistoryFields> HistoryProducts { get; }
    }
}
