using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using projectBOSE.Model;

namespace projectBOSE.ViewModel
{
    class PayersViewModel : ViewModelBase, IPayersViewModel
    {
        RelayCommand _findPayerCommand;
        public RelayCommand FindPayerCommand
        {
            get
            {
                return _findPayerCommand ?? (_findPayerCommand = new RelayCommand(FindPayerCommandMethod,
                   () => PayerField.Length != 0
                ));
            }
        }

        public PayersViewModel()
        {
            ViewModelLocator.Instance.DatabaseService.dbHistoryChanged += DatabaseService_dbHistoryChanged;
        }

        void DatabaseService_dbHistoryChanged(object sender, EventArgs e)
        {
            PayersHistory = ViewModelLocator.Instance.DatabaseService.GetHistoryByClient(73881762489);
        }

        public override void Cleanup()
        {
            base.Cleanup();
            ViewModelLocator.Instance.DatabaseService.dbHistoryChanged -= DatabaseService_dbHistoryChanged;
        }


        /// <summary>
        /// The <see cref="PayerField" /> property's name.
        /// </summary>
        public const string PayerFieldPropertyName = "PayerField";
        private string _payerField = String.Empty;

        /// <summary>
        /// Сумма
        /// </summary>
        public string PayerField
        {
            get
            {
                return _payerField;
            }

            set
            {
                if (_payerField == value)
                    return;
                _payerField = value;
                this.FindPayerCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(PayerFieldPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PayersHistory" /> property's name.
        /// </summary>
        public const string PayersHistoryPropertyName = "PayersHistory";
        private ObservableCollection<historyres> _payersHistory = ViewModelLocator.Instance.DatabaseService.GetHistoryByClient(73881762489);
        public ObservableCollection<historyres> PayersHistory
        {
            get
            {
                return _payersHistory;
            }

            set
            {
                if (_payersHistory == value)
                    return;
                _payersHistory = value;
                RaisePropertyChanged(PayersHistoryPropertyName);
            }
        }

        private void FindPayerCommandMethod()
        {
            try
            {
                decimal value = Convert.ToDecimal(_payerField);
                ObservableCollection<historyres> tempObj = ViewModelLocator.Instance.DatabaseService.GetHistoryByClient(value);
                if (tempObj == null)
                    System.Windows.MessageBox.Show("Client with id " + value + " is not found in system", "Warning", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                else
                {
                    _payersHistory = tempObj;
                    RaisePropertyChanged(PayersHistoryPropertyName);
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            /*--Проверка username и password--*/

            //if (Identifying == null) return;
            //Identifying(this, new EventArgs());
        }
    }
}
