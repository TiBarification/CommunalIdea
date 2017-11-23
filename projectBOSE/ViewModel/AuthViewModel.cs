using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using projectBOSE.ClientAuthentication;

namespace projectBOSE.ViewModel
{
    class AuthViewModel : ViewModelBase, IAuthViewModel
    {
        ClientAuthenticator authenticator;
        public AuthViewModel()
        {

        }
        RelayCommand _loginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(this.LoginCommandMethod));
            }
        }

        /// <summary>
        /// The <see cref="UsernameString" /> property's name.
        /// </summary>
        public const string UsernameStringPropertyName = "UsernameString";

        private string _usernameString = null;

        /// <summary>
        /// Для username
        /// </summary>
        public string UsernameString
        {
            get
            {
                return _usernameString;
            }

            set
            {
                if (_usernameString == value)
                    return;
                _usernameString = value;
                RaisePropertyChanged(UsernameStringPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="PasswordString" /> property's name.
        /// </summary>
        public const string PasswordStringPropertyName = "PasswordString";
        private string _passwordString = null;

        /// <summary>
        /// Для password
        /// </summary>
        public string PasswordString
        {
            get
            {
                return _passwordString;
            }

            set
            {
                if (_passwordString == value)
                    return;
                _passwordString = value;
                RaisePropertyChanged(PasswordStringPropertyName);
            }
        }

        public event EventHandler Identifying;

        private void LoginCommandMethod()
        {
            ///*--Проверка username и password--*/
            //if (this._passwordString == null || this._usernameString == null || this._passwordString == String.Empty || this._usernameString == String.Empty) // Решена проблема с фокусами
            //    return;

            //authenticator = new ClientAuthenticator(); // Коннект к серверу

            //if (!authenticator.Authenticate(this._usernameString, this._passwordString))
            //    return;
            
            if (Identifying == null) return;
            Identifying(this, new EventArgs());            
        }
    }
}
