using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projectBOSE.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserControlAuthentication.xaml
    /// </summary>
    public partial class UserControlAuthentication : UserControl
    {
        public UserControlAuthentication()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //// Убрать фокус с полей ввода
            textBoxPassword.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            textBoxLogin.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
