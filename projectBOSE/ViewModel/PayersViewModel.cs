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
    class PayersViewModel : ViewModelBase, IPayersViewModel
    {
        RelayCommand _findPayerCommand;
        public RelayCommand FindPayerCommand
        {
            get
            {
                return _findPayerCommand ?? (_findPayerCommand = new RelayCommand(FindPayerCommandMethod));
            }
        }

        /// <summary>
        /// The <see cref="PayerField" /> property's name.
        /// </summary>
        public const string PayerFieldPropertyName = "PayerField";
        private string _fayerField = null;

        /// <summary>
        /// Сумма
        /// </summary>
        public string PayerField
        {
            get
            {
                return _fayerField;
            }

            set
            {
                if (_fayerField == value)
                    return;
                _fayerField = value;
                RaisePropertyChanged(PayerFieldPropertyName);
            }
        }




        public ObservableCollection<string> TypesOfServices
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private void FindPayerCommandMethod()
        {
            /*--Проверка username и password--*/

            //if (Identifying == null) return;
            //Identifying(this, new EventArgs());
        }
    }
}
