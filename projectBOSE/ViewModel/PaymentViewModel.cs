using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace projectBOSE.ViewModel
{
    class PaymentViewModel : ViewModelBase, IPaymentViewModel
    {

        public PaymentViewModel()
        {
            try
            {
                this.TypesOfServices = ViewModelLocator.Instance.DatabaseService.GetAllNamesSevices();
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            //this.PropertyChanged += PRSDatabaseViewModel_PropertyChanged;
        }

        /// <summary>
        /// The <see cref="TypesOfServices" /> property's name.
        /// </summary>
        public const string TypesOfServicesPropertyName = "TypesOfServices";
        private ObservableCollection<string> _typesOfServices = new ObservableCollection<string>();
            //{
            //    "Централізоване постачання холодної води",
            //    "Централізоване водовідведення (Каналізація)",
            //    "Централізоване опаленн",
            //    "Централізоване постачання гарячої води",
            //    "Електропостачання",
            //    "Газопостачання",
            //    "Вивезення побутових відходів",
            //    "Прибирання прибудинкової території",
            //    "Прибирання сходових клітин",
            //    "Вивезення побутових відходів",
            //    "Прибирання підвалу, технічних поверхів та покрівлі",
            //    "Технічне обслуговування ліфтів",
            //    "Обслуговування систем диспетчеризації",
            //    "Поточний ремонт конструктивних елементів",
            //    "Поливання дворів, клумб і газонів",
            //    "Прибирання і вивезення снігу",
            //    "Енергопостачання ліфтів",
            //    "Інше технічне обслуговування внутрішньобудинкових систем"
            //};

        /// <summary>
        /// Сумма
        /// </summary>
        public ObservableCollection<string> TypesOfServices
        {
            get
            {
                return _typesOfServices;
            }

            set
            {
                if (_typesOfServices == value)
                    return;
                _typesOfServices = value;
                RaisePropertyChanged(TypesOfServicesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ReceiverAccount" /> property's name.
        /// </summary>
        public const string ReceiverAccountPropertyName = "ReceiverAccount";
        private string _receiverAccount = null;

        /// <summary>
        /// Сумма
        /// </summary>
        public string ReceiverAccount
        {
            get
            {
                return _receiverAccount;
            }

            set
            {
                if (_receiverAccount == value)
                    return;
                _receiverAccount = value;
                RaisePropertyChanged(ReceiverAccountPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FullNamePayer" /> property's name.
        /// </summary>
        public const string FullNamePayerPropertyName = "FullNamePayer";
        private string _fullNamePayer = null;

        /// <summary>
        /// Сумма
        /// </summary>
        public string FullNamePayer
        {
            get
            {
                return _fullNamePayer;
            }

            set
            {
                if (_fullNamePayer == value)
                    return;
                _fullNamePayer = value;
                RaisePropertyChanged(FullNamePayerPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PersonalAccount" /> property's name.
        /// </summary>
        public const string PersonalAccountPropertyName = "PersonalAccount";
        private string _personalAccount = null;

        /// <summary>
        /// Сумма
        /// </summary>
        public string PersonalAccount
        {
            get
            {
                return _personalAccount;
            }

            set
            {
                if (_personalAccount == value)
                    return;
                _personalAccount = value;
                RaisePropertyChanged(PersonalAccountPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="AmountString" /> property's name.
        /// </summary>
        public const string AmountStringPropertyName = "AmountString";
        private string _amountString = null;

        /// <summary>
        /// Сумма
        /// </summary>
        public string AmountString
        {
            get
            {
                return _amountString;
            }

            set
            {
                if (_amountString == value)
                    return;
                _amountString = value;
                RaisePropertyChanged(AmountStringPropertyName);
            }
        }



        RelayCommand _payCommand;
        public RelayCommand PayCommand
        {
            get
            {
                return _payCommand ?? (_payCommand = new RelayCommand(this.PayCommandMethod));
            }
        }

        private void PayCommandMethod()
        {
            /*--Проверка username и password--*/

            //if (Identifying == null) return;
            //Identifying(this, new EventArgs());
        }
    }
}
