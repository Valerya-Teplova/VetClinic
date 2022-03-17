﻿using System;
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
using System.Windows.Shapes;

namespace VetClinic
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(LoginTxB.Text))
            {
                if (!String.IsNullOrEmpty(PasswordPsB.Password))
                {
                    IQueryable<User> users = Db.DBClass.GetContext().User.Where(p => p.Login == LoginTxB.Text && p.Password == PasswordPsB.Password);
                    if (users.Count() == 1)
                    {
                        UserWindow userWindow = new UserWindow(users.First());
                        userWindow.Owner = this;
                        userWindow.Show();
                        Hide();
                    }
                    else MessageBox.Show("Вы вели неправильный логин или пароль", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
    }
}
