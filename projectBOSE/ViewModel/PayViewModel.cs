using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using System.Data.SQLite;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using projectBOSE.Common;
using System.ComponentModel;

namespace projectBOSE.ViewModel
{
    class PayViewModel : ViewModelBase, IPayViewModel, INotifyPropertyChanged
    {
        public string debugMessage;
        /// <summary>
        /// The <see cref="UsernameString" /> property's name.
        /// </summary>
        public const string DebugMessagePropertyName = "UsernameString";
        public string DebugMessage
        {
            get
            {
                return debugMessage;
            }
            set
            {
                debugMessage = value;
                RaisePropertyChanged(DebugMessagePropertyName);
                //OnDebugMessageChanged("DebugMessage");
            }
        }

        public event PropertyChangedEventHandler OnDebugMessage;
        private void OnDebugMessageChanged(string v)
        {
            // Something TODO ?

            OnDebugMessage?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public ObservableCollection<HistoryFields> HistoryProducts
        {
            get
            {
                return _historyProducts;
            }
            set
            {
                _historyProducts = value;
                NotifyPropertyChanged("HistoryProducts");
            }
        }

        public event PropertyChangedEventHandler HistoryTableChanged;
        private void NotifyPropertyChanged(string v)
        {
            SetGridSQLite();
            HistoryTableChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        /*private void NotifyPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }*/

        ObservableCollection<HistoryFields> _historyProducts;

        private void HistoryProductsLoading()
        {
            
        }

        public void SetGridSQLite()
        {
            try
            {
                SQLiteConnection connection = new SQLiteConnection("Data Source=communal.db;Version=3;");
                SQLiteCommand command = new SQLiteCommand(connection);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return;
            }
        }
    }
}
